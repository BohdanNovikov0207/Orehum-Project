using System.Linq;
using System.Threading.Tasks;
using Content.Server._Lavaland.Mobs.AshDrake.Components;
using Robust.Shared.Map.Components;
using Timer = Robust.Shared.Timing.Timer;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

namespace Content.Server._Lavaland.Mobs.AshDrake;

public sealed class AshDrakeTelepadSystem : EntitySystem
{
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly SharedMapSystem _map = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AshDrakeTelepadComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<AshDrakeTelepadComponent, EntityTerminatingEvent>(OnTerminating);
    }

    private void OnMapInit(Entity<AshDrakeTelepadComponent> ent, ref MapInitEvent args)
    {
        var xform = Transform(ent).Coordinates;
        var drake = Spawn(ent.Comp.AshDrakePrototype, xform);

        if (!TryComp<AshDrakeBossComponent>(drake, out var drakeComp))
            return;

        ent.Comp.ConnectedAshDrake = drake;
        drakeComp.ConnectedTelepad = ent;
    }

    private void OnTerminating(Entity<AshDrakeTelepadComponent> ent, ref EntityTerminatingEvent args)
    {
        if (ent.Comp.ConnectedAshDrake != null &&
            TryComp<AshDrakeBossComponent>(ent.Comp.ConnectedAshDrake.Value, out var drakeComp))
            drakeComp.ConnectedTelepad = null;

        DeleteAshDrakeFieldImmediatly(ent);
    }

    public void ActivateField(Entity<AshDrakeTelepadComponent> ent)
    {
        if (ent.Comp.Enabled)
            return; // how?

        SpawnDrakeField(ent);
        ent.Comp.Enabled = true;
    }

    public void DeactivateField(Entity<AshDrakeTelepadComponent> ent)
    {
        if (!ent.Comp.Enabled)
            return; // how?

        DeleteDrakeField(ent);
        ent.Comp.Enabled = false;
    }

    public void DeleteAshDrakeFieldImmediatly(Entity<AshDrakeTelepadComponent> ent)
    {
        var walls = ent.Comp.Walls.Where(x => !TerminatingOrDeleted(x));
        foreach (var wall in walls)
        {
            QueueDel(wall);
        }
    }

    private async Task SpawnDrakeField(Entity<AshDrakeTelepadComponent> ent)
    {
        var xform = Transform(ent);

        if (!TryComp<MapGridComponent>(xform.GridUid, out var grid))
            return;

        var gridEnt = (xform.GridUid.Value, grid);
        var range = ent.Comp.Radius;
        var center = xform.Coordinates.Position;

        // get tile position of our entity
        if (!_transform.TryGetGridTilePosition((ent, xform), out var tilePos))
            return;

        var pos = _map.TileCenterToVector(gridEnt, tilePos);
        var confines = new Box2(center, center).Enlarged(ent.Comp.Radius);
        var box = _map.GetLocalTilesIntersecting(ent, grid, confines).ToList();

        var confinesS = new Box2(pos, pos).Enlarged(Math.Max(range - 1, 0));
        var boxS = _map.GetLocalTilesIntersecting(ent, grid, confinesS).ToList();
        box = box.Where(b => !boxS.Contains(b)).ToList();

        // fill the box
        Timer.Spawn(5000, () =>
        {
            foreach (var tile in box)
            {
                var wall = Spawn(ent.Comp.WallPrototype, _map.GridTileToWorld(xform.GridUid.Value, grid, tile.GridIndices));
                ent.Comp.Walls.Add(wall);
            }
        });
    }

    private async Task DeleteDrakeField(Entity<AshDrakeTelepadComponent> ent)
    {
        var walls = ent.Comp.Walls.Where(x => !TerminatingOrDeleted(x));
        foreach (var wall in walls)
        {
            QueueDel(wall);
        }
    }
}
