// грело, когда ерп
using Content.Shared.Weapons.Melee.Events;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Map;
using Robust.Shared.Map.Components;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Content.Server._Lavaland.Mobs.AshDrake.Components;
using Content.Shared._Lavaland.Aggression;
using Content.Shared.Damage;
using Content.Shared.FixedPoint;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Systems;
using Content.Shared.Mobs.Components;
using Robust.Shared.Timing;
using Content.Shared.Movement.Components;
using Content.Shared.Movement.Systems;

using Timer = Robust.Shared.Timing.Timer;
using Robust.Server.GameObjects;
using System.Threading;
using Robust.Shared.Physics.Components;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

namespace Content.Server._Lavaland.Mobs.AshDrake;

public sealed class AshDrakeSystem : EntitySystem
{
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedTransformSystem _xform = default!;
    //[Dependency] private readonly SharedMapSystem _map = default!;
    [Dependency] private readonly MapSystem _map = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly MegafaunaSystem _megafauna = default!;
    [Dependency] private readonly AshDrakeTelepadSystem _ashDrakeTelepad = default!;
    [Dependency] private readonly DamageableSystem _damage = default!;
    [Dependency] private readonly MobThresholdSystem _threshold = default!;
    [Dependency] private readonly MovementSpeedModifierSystem _movement = default!;

    [Dependency] private readonly EntityLookupSystem _lookup = default!;

    private readonly EntProtoId _firePrototype = "LavalandAshDrakeFire";
    private readonly EntProtoId _markPrototype = "LavalandSupportMarkAshDrake";
    private readonly EntProtoId _lavaPrototype = "LavalandSupportLavaMarkAshDrake";
    private readonly EntProtoId _wallPrototype = "LavalandAshDrakeWallBasaltCobblebrick";
    private readonly EntProtoId _safeMarkPrototype = "LavalandSupportSafeMarkAshDrake";
    private readonly EntProtoId _lavaArenaPrototype = "LavalandSupportLavaArenaMarkAshDrake";

    private readonly EntProtoId _drakeStatuePrototype = "LavalandAshDrakeStatue";
    private readonly EntProtoId _cratePrototype = "LavalandCrateNecropolisAshDrake";

    private const float HealthScalingFactor = 1.25f;
    private const float AngerScalingFactor = 1.15f;
    private readonly FixedPoint2 _baseAshDrakeHp = 4000;

    private record RemovedEntity(string PrototypeId, MapCoordinates Position);

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AshDrakeBossComponent, AttackedEvent>(OnAttacked);
        SubscribeLocalEvent<AshDrakeBossComponent, MobStateChangedEvent>(_megafauna.OnDeath);

        SubscribeLocalEvent<AshDrakeBossComponent, MegafaunaStartupEvent>(OnAshDrakeInit);
        SubscribeLocalEvent<AshDrakeBossComponent, MegafaunaDeinitEvent>(OnAshDrakeDeinit);
        SubscribeLocalEvent<AshDrakeBossComponent, MegafaunaKilledEvent>(OnAshDrakeKilled);
        SubscribeLocalEvent<AshDrakeBossComponent, AggressorAddedEvent>(OnAggressorAdded);
    }

    #region Event Handling

    private void OnAshDrakeInit(Entity<AshDrakeBossComponent> ent, ref MegafaunaStartupEvent args)
    {
        if (ent.Comp.ConnectedTelepad != null &&
            TryComp<AshDrakeTelepadComponent>(ent.Comp.ConnectedTelepad.Value, out var fieldComp))
            _ashDrakeTelepad.ActivateField((ent.Comp.ConnectedTelepad.Value, fieldComp));

        _movement.ChangeBaseSpeed(ent, 4f, 2f, 20f);
    }

    private void OnAshDrakeDeinit(Entity<AshDrakeBossComponent> ent, ref MegafaunaDeinitEvent args)
    {
        if (ent.Comp.ConnectedTelepad == null ||
            !TryComp<DamageableComponent>(ent, out var damageable) ||
            !TryComp<AshDrakeTelepadComponent>(ent.Comp.ConnectedTelepad.Value, out var fieldComp) ||
            !TryComp<MobThresholdsComponent>(ent, out var thresholds))
            return;

        _movement.ChangeBaseSpeed(ent, 0f, 0f, 1f);

        var telepad = ent.Comp.ConnectedTelepad.Value;
        _ashDrakeTelepad.DeactivateField((telepad, fieldComp));
        var position = _xform.GetMapCoordinates(telepad);
        _damage.SetAllDamage(ent, damageable, 0);
        _threshold.SetMobStateThreshold(ent, _baseAshDrakeHp, MobState.Dead, thresholds);
        Timer.Spawn(TimeSpan.FromSeconds(10), () => _xform.SetMapCoordinates(ent, position));
    }

    private void OnAshDrakeKilled(Entity<AshDrakeBossComponent> ent, ref MegafaunaKilledEvent args)
    {
        if (ent.Comp.ConnectedTelepad != null &&
            TryComp<AshDrakeTelepadComponent>(ent.Comp.ConnectedTelepad.Value, out var fieldComp))
        {
            var telepad = ent.Comp.ConnectedTelepad.Value;
            _ashDrakeTelepad.DeactivateField((telepad, fieldComp));
            var position = _xform.GetMapCoordinates(telepad);

            QueueDel(telepad);
            var statue = EntityManager.SpawnEntity(_drakeStatuePrototype, position);
            EntityManager.SpawnEntity(_cratePrototype, position);
            EntityManager.GetComponent<TransformComponent>(statue).LocalRotation = Angle.FromDegrees(0);
        }
    }

    private void OnAttacked(Entity<AshDrakeBossComponent> ent, ref AttackedEvent args)
    {
        _movement.ChangeBaseSpeed(ent, 4f, 2f, 20f);
        _megafauna.OnAttacked(ent, ent.Comp, ref args);
    }

    private void OnAggressorAdded(Entity<AshDrakeBossComponent> ent, ref AggressorAddedEvent args)
    {
        if (!TryComp<AggressiveComponent>(ent, out var aggressive)
            || !TryComp<MobThresholdsComponent>(ent, out var thresholds))
            return;

        UpdateScaledThresholds(ent, aggressive, thresholds);
    }

    #endregion

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var eqe = EntityQueryEnumerator<AshDrakeBossComponent, DamageableComponent>();
        while (eqe.MoveNext(out var uid, out var comp, out var damage))
        {
            Entity<AshDrakeBossComponent> ent = (uid, comp);

            if (TryComp<AggressiveComponent>(uid, out var aggressors))
            {
                if (aggressors.Aggressors.Count > 0 && !comp.Aggressive)
                    InitBoss(ent, aggressors);
                else if (aggressors.Aggressors.Count == 0 && comp.Aggressive)
                    DeinitBoss(ent);
            }

            if (!comp.Aggressive)
                continue;

            if (!comp.IsBreathingFireCircle && !comp.IsFireJump && !comp.IsFireArena)
            {
                TickTimer(ref comp.AttackTimer, frameTime, () =>
                {
                    DoRandomAttack(ent);
                    comp.AttackTimer = Math.Max(comp.AttackCooldown / 2, comp.MinAttackCooldown);
                });
            }
        }
    }

    private void TickTimer(ref float timer, float frameTime, Action onFired)
    {
        timer -= frameTime;

        if (timer <= 0)
        {
            onFired.Invoke();
        }
    }

    #region Boss Initializing

    private void InitBoss(Entity<AshDrakeBossComponent> ent, AggressiveComponent aggressors)
    {
        ent.Comp.Aggressive = true;
        RaiseLocalEvent(ent, new MegafaunaStartupEvent());
    }

    private void DeinitBoss(Entity<AshDrakeBossComponent> ent)
    {
        Logger.GetSawmill("drake").Info($"[AshDrake] DeinitBoss triggered for {ent}");
        ent.Comp.Aggressive = false;
        RaiseLocalEvent(ent, new MegafaunaDeinitEvent());
    }

    #endregion

    #region Attack Calculation

    private async Task DoAttack(Entity<AshDrakeBossComponent> ent, EntityUid? target, AshDrakeAttackType attackType)
    {
        var damage = new FixedPoint2();
        if (EntityManager.TryGetComponent<DamageableComponent>(ent, out var comp))
        {
            damage = comp.TotalDamage;
            //Logger.GetSawmill("debug").Info($"HP дракона: {hp}");
        }
        if (damage < 3600)
        {
            switch (attackType)
            {
                case AshDrakeAttackType.Invalid:
                    return;

                case AshDrakeAttackType.BreathingFire:
                    if (damage > 1500)
                    {
                        BreathingFire(ent, target);
                        if (target != null)
                            SpawnMarksAround(target.Value, _markPrototype, 9, 0.30f);
                    } else
                    {
                        BreathingFire(ent, target);
                    }
                    break;

                case AshDrakeAttackType.BreathingFireCircle:
                    if (damage > 1500)
                    //{
                        BreathingFireCircle(ent, 6);
                    //}
                    else
                    //{
                        DoRandomAttack(ent);
                    //}
                    break;

                case AshDrakeAttackType.FireJump:
                    if (damage > 1500)
                    //{
                        FireArena(ent, target);
                    //}
                    else
                    //{
                        FireJump(ent, target);
                    //}
                    break;

                case AshDrakeAttackType.FireArea:
                    if (damage > 1500)
                        FireArena(ent, target);
                    else
                        DoRandomAttack(ent);
                    break;
            }

            ent.Comp.PreviousAttack = attackType;
        }
    }

    private void DoRandomAttack(Entity<AshDrakeBossComponent> ent)
    {
        if (TerminatingOrDeleted(ent))
            return;

        var target = PickTarget(ent);

        var rounding = _random.Next(0, 1) == 1 ? MidpointRounding.AwayFromZero : MidpointRounding.ToZero;

        var attacks = ent.Comp.Attacks.Keys.Where(x => x != ent.Comp.PreviousAttack).ToList();
        var attackType = _random.Pick(attacks);

        DoAttack(ent, target, attackType);
    }

    #endregion

    #region Attacks

    public void BreathingFire(Entity<AshDrakeBossComponent> ent, EntityUid? target)
    {
        var xform = Transform(ent);
        var rotation = xform.LocalRotation;
        var dir = GetDirection(rotation);

        if (xform.GridUid == null ||
            !EntityManager.TryGetComponent<MapGridComponent>(xform.GridUid.Value, out var grid))
            return;

        var gridUid = xform.GridUid.Value;
        var start = grid.TileIndicesFor(xform.Coordinates);
        var lines = GetFireLines(dir);
        var maxLen = lines.Max(line => line.Count);

        if (target != null && _random.Next(0, 2) > 0)
            SpawnMarksAround(target.Value, _markPrototype, 9, 0.11f);

        for (int step = 0; step < maxLen; step++)
        {
            var delay = (int) GetDelay(ent, ent.Comp.InterActionDelay / 3f) * step + 5;
            var stepCopy = step; 

            Timer.Spawn(delay, () =>
            {
                foreach (var line in lines)
                {
                    if (stepCopy < line.Count)
                    {
                        var offset = line[stepCopy];
                        var tile = start + offset;
                        var world = _map.GridTileToWorld(gridUid, grid, tile);
                        EntityManager.SpawnEntity(_firePrototype, world);
                    }
                }
            });
        }
    }

    public void BreathingFireCircle(Entity<AshDrakeBossComponent> ent, int count = 3)
    {
        var xform = Transform(ent);

        if (xform.GridUid == null ||
            !EntityManager.TryGetComponent<MapGridComponent>(xform.GridUid.Value, out var grid))
            return;

        var gridUid = xform.GridUid.Value;
        var start = grid.TileIndicesFor(xform.Coordinates);

        var directions = new[] { Direction.North, Direction.East, Direction.South, Direction.West };

        var allLines = new List<List<Vector2i>>();
        foreach (var dir in directions)
        {
            var lines = GetFireLines(dir);
            allLines.AddRange(lines);
        }

        var maxLen = allLines.Max(line => line.Count);

        ent.Comp.IsBreathingFireCircle = true;

        for (int breath = 0; breath < count; breath++)
        {
            var breathCopy = breath;

            var breathDelay = breathCopy * 2000;

            Timer.Spawn(breathDelay, () =>
            {
                EntityUid target = (EntityUid?) ent.Owner ?? (EntityUid?) PickTarget(ent) ?? ent;
                if (_random.Next(0, 2) > 0)
                    SpawnMarksAround(target, _markPrototype, 9, 0.11f);

                start = grid.TileIndicesFor(xform.Coordinates);

                for (int step = 0; step < maxLen; step++)
                {
                    var delay = (int) GetDelay(ent, ent.Comp.InterActionDelay / 3f) * step + 5;
                    var stepCopy = step;

                    Timer.Spawn(delay, () =>
                    {
                        foreach (var line in allLines)
                        {
                            if (stepCopy < line.Count)
                            {
                                var offset = line[stepCopy];
                                var tile = start + offset;
                                var world = _map.GridTileToWorld(gridUid, grid, tile);
                                EntityManager.SpawnEntity(_firePrototype, world);
                            }
                        }
                    });
                    if (breathCopy == count - 1)
                        ent.Comp.IsBreathingFireCircle = false;
                }
            });
        }
    }

    public void FireJump(Entity<AshDrakeBossComponent> ent, EntityUid? target)
    {
        ent.Comp.IsFireJump = true;
        if (target == null)
            target = PickTarget(ent);

        if (target == null)
            return;

        var xform = Transform(ent);

        _movement.ChangeBaseSpeed(ent, 0f, 0f, 1f);

        if (ent.Comp.ConnectedTelepad != null)
        {
            var telepad = ent.Comp.ConnectedTelepad.Value;
            var position = _xform.GetMapCoordinates(telepad);
            _xform.SetMapCoordinates(ent, position);
        }

        SpawnMarksAround(target.Value, _lavaPrototype, 1, 1.0f);

        var duration = TimeSpan.FromSeconds(2);
        var interval = TimeSpan.FromMilliseconds(500); 
        var elapsed = 0;

        var cts = new CancellationTokenSource();
        Timer.SpawnRepeating(TimeSpan.FromMilliseconds(500), () =>
        {
            SpawnMarksAround(target.Value, _lavaPrototype, 1, 0.55f);
        }, cts.Token);

        Timer.Spawn((int) duration.TotalMilliseconds, () =>
        {
            cts.Cancel();
            if (!TryComp<MapGridComponent>(Transform(target.Value).GridUid, out var grid))
                return;

            var gridUid = Transform(target.Value).GridUid!.Value;
            var start = grid.TileIndicesFor(Transform(target.Value).Coordinates);

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    var tile = start + new Vector2i(dx, dy);
                    var pos = _map.GridTileToWorld(gridUid, grid, tile);
                    EntityManager.SpawnEntity(_lavaPrototype, pos);
                }
            }

            Timer.Spawn(TimeSpan.FromSeconds(3), () =>
            {
                var world = _map.GridTileToWorld(gridUid, grid, start);
                _xform.SetMapCoordinates(ent, world);
                _movement.ChangeBaseSpeed(ent, 4f, 2f, 20f);
                BreathingFireCircle(ent, 1);
                ent.Comp.IsFireJump = false;
            });

            
        });
    }

    public void FireArena(Entity<AshDrakeBossComponent> ent, EntityUid? target)
    {
        ent.Comp.IsFireArena = true;
        if (target == null)
            target = PickTarget(ent);
        if (target == null)
            return;

        var tXform = Transform(target.Value);
        if (tXform.GridUid == null ||
            !EntityManager.TryGetComponent<MapGridComponent>(tXform.GridUid.Value, out var grid))
            return;

        var gridUid = tXform.GridUid.Value;

        if (ent.Comp.ConnectedTelepad != null &&
            EntityManager.TryGetComponent<AshDrakeTelepadComponent>(ent.Comp.ConnectedTelepad.Value, out var telepadComp))
        {
            var originalCenter = grid.TileIndicesFor(tXform.Coordinates);
            var center = AdjustArenaCenter(originalCenter, gridUid, grid, telepadComp.Walls);

            /*
            if (ent.Comp.ConnectedTelepad != null)
            {*/
            var telepad = ent.Comp.ConnectedTelepad.Value;
            var tpPos = _xform.GetMapCoordinates(telepad);
            _movement.ChangeBaseSpeed(ent, 0f, 0f, 1f);
            _xform.SetMapCoordinates(ent, tpPos);
            //}

            var spawnedWalls = new List<EntityUid>();
            var removedEntities = new List<RemovedEntity>();

            SaveAndClearInsideArena(gridUid, grid, center, 2, removedEntities);

            for (int dx = -3; dx <= 3; dx++)
            {
                for (int dy = -3; dy <= 3; dy++)
                {
                    if (Math.Abs(dx) == 3 || Math.Abs(dy) == 3)
                    {
                        var tile = center + new Vector2i(dx, dy);
                        var pos = _map.GridTileToWorld(gridUid, grid, tile);
                        var wall = EntityManager.SpawnEntity(_wallPrototype, pos);
                        spawnedWalls.Add(wall);
                    }
                }
            }

            for (int round = 0; round < 3; round++)
            {
                var roundCopy = round;
                var delay = TimeSpan.FromSeconds(4.5 * roundCopy);

                Timer.Spawn(delay, () =>
                {
                    var innerTiles = new List<Vector2i>();
                    for (int dx = -2; dx <= 2; dx++)
                    {
                        for (int dy = -2; dy <= 2; dy++)
                        {
                            innerTiles.Add(center + new Vector2i(dx, dy));
                        }
                    }

                    var safeTile = _random.Pick(innerTiles);
                    var safePos = _map.GridTileToWorld(gridUid, grid, safeTile);
                    EntityManager.SpawnEntity(_safeMarkPrototype, safePos);

                    foreach (var tile in innerTiles)
                    {
                        if (tile == safeTile)
                            continue;

                        var pos = _map.GridTileToWorld(gridUid, grid, tile);
                        EntityManager.SpawnEntity(_lavaArenaPrototype, pos);
                    }

                    Timer.Spawn(TimeSpan.FromSeconds(4.5), () =>
                    {
                        if (roundCopy == 2)
                        {
                            var worldCenter = _map.GridTileToWorld(gridUid, grid, center);
                            _movement.ChangeBaseSpeed(ent, 4f, 2f, 20f);
                            _xform.SetMapCoordinates(ent, worldCenter);

                            CleanupWalls(spawnedWalls);
                            RestoreEntities(removedEntities);
                            ent.Comp.IsFireArena = false;
                        }
                    });
                });
            }
        }
    }

    #endregion

    #region Patterns

    public void SpawnMarksAround(EntityUid relative, string prototypeId, int range = 9, float chance = 0.11f)
    {
        var xform = Transform(relative);

        if (!TryComp<MapGridComponent>(xform.GridUid, out var grid))
            return;

        var gridEnt = ((EntityUid) xform.GridUid, grid);

        if (!_xform.TryGetGridTilePosition(relative, out var tilePos))
            return;

        var pos = _map.TileCenterToVector(gridEnt, tilePos);

        var tiles = _map.GetLocalTilesIntersecting(relative, grid,
            new Box2(pos, pos).Enlarged(range)).ToList();

        var rand = new Random();

        foreach (var tile in tiles)
        {
            if (rand.NextDouble() <= chance)
            {
                var world = _map.GridTileToWorld((EntityUid) xform.GridUid, grid, tile.GridIndices);
                EntityManager.SpawnEntity(prototypeId, world);
            }
        }
    }



    
    // навайбкодил. Пожалуй, оставлю эти подсказки
    /// <summary>
    /// Возвращает массив линий для выбранного направления.
    /// Каждая линия — список смещений тайлов.
    /// </summary>
    private List<List<Vector2i>> GetFireLines(Direction dir)
    {
        var lines = new List<List<Vector2i>>();

        switch (dir)
        {
            case Direction.North:
                lines.Add(MakeLine(0, -1, 10));
                lines.Add(MakeBentLine(-1, -1, 0, -1));
                lines.Add(MakeBentLine(1, -1, 0, -1));
                break;

            case Direction.South:
                lines.Add(MakeLine(0, 1, 10));
                lines.Add(MakeBentLine(1, 1, 0, 1));
                lines.Add(MakeBentLine(-1, 1, 0, 1));
                break;

            case Direction.East:
                lines.Add(MakeLine(1, 0, 10));
                lines.Add(MakeBentLine(1, -1, 1, 0));
                lines.Add(MakeBentLine(1, 1, 1, 0));
                break;

            case Direction.West:
                lines.Add(MakeLine(-1, 0, 10));
                lines.Add(MakeBentLine(-1, 1, -1, 0));
                lines.Add(MakeBentLine(-1, -1, -1, 0));
                break;
        }

        return lines;
    }

    /// <summary>
    /// Прямая линия.
    /// </summary>
    private List<Vector2i> MakeLine(int dx, int dy, int length)
    {
        var line = new List<Vector2i>();
        var pos = Vector2i.Zero;
        for (int i = 0; i < length; i++)
        {
            pos += new Vector2i(dx, dy);
            line.Add(pos);
        }
        return line;
    }

    /// <summary>
    /// Линия с изгибом.
    /// </summary>
    private List<Vector2i> MakeBentLine(int dx1, int dy1, int dx2, int dy2)
    {
        var line = new List<Vector2i>();
        var pos = Vector2i.Zero;

        for (int i = 0; i < 5; i++)
        {
            pos += new Vector2i(dx1, dy1);
            line.Add(pos);
        }
        for (int i = 0; i < 5; i++)
        {
            pos += new Vector2i(dx2, dy2);
            line.Add(pos);
        }
        return line;
    }

    private void CleanupWalls(List<EntityUid> walls)
    {
        foreach (var uid in walls)
        {
            if (EntityManager.EntityExists(uid))
                EntityManager.DeleteEntity(uid);
        }
        walls.Clear();
    }

    /// <summary>
    /// Сохраняет и удаляет все твёрдые объекты внутри арены.
    /// </summary>
    private void SaveAndClearInsideArena(EntityUid gridUid, MapGridComponent grid, Vector2i center, int radius, List<RemovedEntity> removed)
    {
        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = -radius; dy <= radius; dy++)
            {
                var tile = center + new Vector2i(dx, dy);
                var worldPos = _map.GridTileToWorld(gridUid, grid, tile);

                foreach (var ent in _lookup.GetEntitiesIntersecting(worldPos))
                {
                    if (EntityManager.HasComponent<MobStateComponent>(ent))
                    {
                        continue;
                    }

                    if (EntityManager.TryGetComponent<PhysicsComponent>(ent, out var physics) && physics.CanCollide)
                    {
                        if (EntityManager.TryGetComponent<MetaDataComponent>(ent, out var meta) && meta.EntityPrototype != null)
                        {
                            var protoId = meta.EntityPrototype.ID;
                            var pos = _xform.GetMapCoordinates(ent);
                            removed.Add(new RemovedEntity(protoId, pos));
                        }
                        EntityManager.DeleteEntity(ent);
                    }
                }
            }
        }
    }




    /// <summary>
    /// Восстанавливает все ранее удалённые объекты.
    /// </summary>
    private void RestoreEntities(List<RemovedEntity> removed)
    {
        foreach (var r in removed)
        {
            EntityManager.SpawnEntity(r.PrototypeId, r.Position);
        }
        removed.Clear();
    }


    private Vector2i AdjustArenaCenter(Vector2i originalCenter, EntityUid gridUid, MapGridComponent grid, List<EntityUid> telepadWalls)
    {
        var offsets = BuildOffsetsRadius3();
        foreach (var offset in offsets)
        {
            var candidate = originalCenter + offset;
            if (!IntersectsTelepadWalls(candidate, gridUid, grid, telepadWalls))
                return candidate;
        }

        return originalCenter;
    }

    private List<Vector2i> BuildOffsetsRadius3()
    {
        var offsets = new List<Vector2i>();

        offsets.Add(new Vector2i(0, 0));

        offsets.AddRange(new[]
        {
        new Vector2i(1,0), new Vector2i(-1,0),
        new Vector2i(0,1), new Vector2i(0,-1),
        new Vector2i(1,1), new Vector2i(-1,1),
        new Vector2i(1,-1), new Vector2i(-1,-1),
    });

        offsets.AddRange(new[]
        {
        new Vector2i(2,0), new Vector2i(-2,0),
        new Vector2i(0,2), new Vector2i(0,-2),
        new Vector2i(2,1), new Vector2i(2,-1),
        new Vector2i(-2,1), new Vector2i(-2,-1),
        new Vector2i(1,2), new Vector2i(-1,2),
        new Vector2i(1,-2), new Vector2i(-1,-2),
        new Vector2i(2,2), new Vector2i(-2,2),
        new Vector2i(2,-2), new Vector2i(-2,-2),
    });

        offsets.AddRange(new[]
        {
        new Vector2i(3,0), new Vector2i(-3,0),
        new Vector2i(0,3), new Vector2i(0,-3),
        new Vector2i(3,1), new Vector2i(3,-1),
        new Vector2i(-3,1), new Vector2i(-3,-1),
        new Vector2i(1,3), new Vector2i(-1,3),
        new Vector2i(1,-3), new Vector2i(-1,-3),
        new Vector2i(2,3), new Vector2i(-2,3),
        new Vector2i(2,-3), new Vector2i(-2,-3),
        new Vector2i(3,2), new Vector2i(-3,2),
        new Vector2i(3,-2), new Vector2i(-3,-2),
        new Vector2i(3,3), new Vector2i(-3,3),
        new Vector2i(3,-3), new Vector2i(-3,-3),
    });

        return offsets;
    }

    private bool IntersectsTelepadWalls(Vector2i center, EntityUid gridUid, MapGridComponent grid, List<EntityUid> telepadWalls)
    {
        for (int dx = -3; dx <= 3; dx++)
        {
            for (int dy = -3; dy <= 3; dy++)
            {
                var tile = center + new Vector2i(dx, dy);
                var worldPos = _map.GridTileToWorld(gridUid, grid, tile);

                foreach (var ent in _lookup.GetEntitiesIntersecting(worldPos))
                {
                    if (telepadWalls.Contains(ent))
                        return true;
                }
            }
        }
        return false;
    }



    #endregion

    #region Helper methods

    private void UpdateScaledThresholds(EntityUid uid,
        AggressiveComponent aggressors,
        MobThresholdsComponent thresholds)
    {
        var playerCount = Math.Max(1, aggressors.Aggressors.Count);
        var scalingMultiplier = 1f;

        for (var i = 1; i < playerCount; i++)
            scalingMultiplier *= HealthScalingFactor;

        Logger.GetSawmill("drake").Info($"Setting threshold for {uid} to {_baseAshDrakeHp * scalingMultiplier}");
        if (_threshold.TryGetDeadThreshold(uid, out var deadThreshold, thresholds)
            && deadThreshold < _baseAshDrakeHp * scalingMultiplier)
            _threshold.SetMobStateThreshold(uid, _baseAshDrakeHp * scalingMultiplier, MobState.Dead, thresholds);
    }

    private EntityUid? PickTarget(Entity<AshDrakeBossComponent> ent)
    {
        if (!ent.Comp.Aggressive
        || !TryComp<AggressiveComponent>(ent, out var aggressive)
        || aggressive.Aggressors.Count == 0
        || TerminatingOrDeleted(ent))
            return null;

        return _random.Pick(aggressive.Aggressors);
    }

    private float GetDelay(Entity<AshDrakeBossComponent> ent, float baseDelay)
    {
        var minDelay = Math.Max(baseDelay / 2.5f, AshDrakeBossComponent.TileDamageDelay);

        return Math.Max(baseDelay - (baseDelay * 2), minDelay);
    }

    private Direction GetDirection(Angle rotation)
    {
        var deg = rotation.Degrees;
        if (deg < 0)
            deg += 360;

        if (deg >= 45 && deg < 135) return Direction.East;
        if (deg >= 135 && deg < 225) return Direction.South;
        if (deg >= -125 && deg < 315) return Direction.West;
        return Direction.North;
    }
    #endregion
}
