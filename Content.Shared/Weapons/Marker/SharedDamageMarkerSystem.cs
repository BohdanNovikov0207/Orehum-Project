using Content.Shared.Damage;
using Content.Shared.Projectiles;
using Content.Shared.Weapons.Melee.Events;
using Content.Shared.Whitelist;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Network;
using Robust.Shared.Physics.Events;
using Robust.Shared.Timing;
// Lavaland Change
using Content.Shared._Lavaland.Weapons.Marker;
using Content.Shared._Lavaland.Mobs;

using Content.Shared.Containers.ItemSlots;
using Content.Shared.Weapons.Melee.Components;
using Content.Shared._Lavaland.Weapons.Crusher.Upgrades.Components;
using Content.Shared._Lavaland.Weapons.Crusher;

//using Content.Shared.Actions;

using Content.Shared._Lavaland.Damage;

using Robust.Shared.Prototypes;

using Robust.Shared.Map;

using Content.Shared.Coordinates.Helpers;

//using Content.Shared.Weapons.Marker.Chaser;

namespace Content.Shared.Weapons.Marker;

public abstract class SharedDamageMarkerSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly INetManager _netManager = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly DamageableSystem _damageable = default!;
    [Dependency] private readonly EntityWhitelistSystem _whitelistSystem = default!;

    //[Dependency] private readonly ActionsSystem _actions = default!;

    [Dependency] private readonly IMapManager _mapMan = default!;

    private readonly EntProtoId _chaserPrototype = "LavalandHierophantChaser";

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<DamageMarkerOnCollideComponent, StartCollideEvent>(OnMarkerCollide);
        SubscribeLocalEvent<DamageMarkerComponent, AttackedEvent>(OnMarkerAttacked);
    }

    private void OnMarkerAttacked(EntityUid uid, DamageMarkerComponent component, AttackedEvent args)
    {
        if (component.Marker != args.Used)
            return;

        args.BonusDamage += component.Damage;
        _audio.PlayPredicted(component.Sound, uid, args.User);

        if (TryComp<LeechOnMarkerComponent>(args.Used, out var leech))
            _damageable.TryChangeDamage(args.User, leech.Leech, true, false, origin: args.Used);

        if (HasComp<DamageBoostOnMarkerComponent>(args.Used))
        {
            RaiseLocalEvent(uid, new ApplyMarkerBonusEvent(args.Used, args.User)); // For effects on the target
            RaiseLocalEvent(args.Used, new ApplyMarkerBonusEvent(args.Used, args.User)); // For effects on the weapon
        }

        RemCompDeferred<DamageMarkerComponent>(uid);

        if (TryComp<ItemSlotsComponent>(component.Marker, out var slots))
        {
            foreach (var slot in slots.Slots.Values)
            {
                if (slot.Whitelist?.Tags?.Contains("CrusherCrest") != true)
                    continue;

                if (slot.Item is not EntityUid crestEntity)
                    continue;

                if (!TryComp<ItemSlotsComponent>(crestEntity, out var crestSlots))
                    continue;

                foreach (var innerSlot in crestSlots.Slots.Values)
                {
                    if (innerSlot.Item is not EntityUid upgradeEntity)
                        continue;

                    if (TryComp<CrusherUpgradeHierophantComponent>(upgradeEntity, out var hierophant))
                    {
                        // var damage = (int) Math.Round(damageable.TotalDamage.Float() / 10.0);
                        var finalMaxSteps = 7; // club.ChaserMaxSteps; // + damage;

                        AddImmunity(args.User, 70f);

                        var xform = Transform(uid);
                        var targetCoords = xform.Coordinates.SnapToGrid(EntityManager, _mapMan);

                        var dummy = Spawn(null, targetCoords);


                        var chaser = Spawn(_chaserPrototype, Transform(args.User).Coordinates);

                        if (TryComp<HierophantChaserSharedComponent>(chaser, out var chasercomp))
                        {
                            chasercomp.Target = dummy;
                            chasercomp.MaxSteps *= finalMaxSteps;
                            chasercomp.Speed += 0.5f;
                        }

                        Timer.Spawn(TimeSpan.FromSeconds(finalMaxSteps + 100000), () =>
                        {
                            QueueDel(dummy);
                        });
                    }
                }
            }
        }
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<DamageMarkerComponent>();

        while (query.MoveNext(out var uid, out var comp))
        {
            if (comp.EndTime > _timing.CurTime)
                continue;

            RemCompDeferred<DamageMarkerComponent>(uid);

            //if (comp.Marker != EntityUid.Invalid)
                //RemComp<MeleeThrowOnHitComponent>(comp.Marker);
        }
    }

    private void OnMarkerCollide(EntityUid uid, DamageMarkerOnCollideComponent component, ref StartCollideEvent args)
    {
        if (!args.OtherFixture.Hard ||
            args.OurFixtureId != SharedProjectileSystem.ProjectileFixture ||
            component.Amount <= 0 ||
            _whitelistSystem.IsWhitelistFail(component.Whitelist, args.OtherEntity) ||
            !TryComp<ProjectileComponent>(uid, out var projectile) ||
            projectile.Weapon == null ||
            component.OnlyWorkOnFauna && // Lavaland Change
            !HasComp<FaunaComponent>(args.OtherEntity))
        {
            return;
        }

        // Markers are exclusive, deal with it.
        var marker = EnsureComp<DamageMarkerComponent>(args.OtherEntity);
        marker.Damage = new DamageSpecifier(component.Damage);
        marker.Marker = projectile.Weapon.Value;
        marker.EndTime = _timing.CurTime + component.Duration;
        component.Amount--;
        Dirty(args.OtherEntity, marker);

        if (_netManager.IsServer)
        {
            if (component.Amount <= 0)
            {
                QueueDel(uid);
            }
            else
            {
                Dirty(uid, component);
            }
        }

        if (projectile.Weapon is { } weapon)
        {
            if (TryComp<ItemSlotsComponent>(weapon, out var slots))
            {
                foreach (var slot in slots.Slots.Values)
                {
                    if (slot.Whitelist?.Tags?.Contains("CrusherCrest") != true)
                        continue;

                    if (slot.Item is not EntityUid crestEntity)
                        continue;

                    if (!TryComp<ItemSlotsComponent>(crestEntity, out var crestSlots))
                        continue;

                    foreach (var innerSlot in crestSlots.Slots.Values)
                    {
                        if (innerSlot.Item is not EntityUid upgradeEntity)
                            continue;

                        if (TryComp<CrusherUpgradeDrakeComponent>(upgradeEntity, out var drake))
                        {
                            EnsureComp<MeleeThrowOnHitComponent>(weapon);
                            /*
                            var throwComp = EnsureComp<MeleeThrowOnHitComponent>(weapon);

                            throwComp.Speed = drake.Speed;
                            throwComp.Lifetime = drake.Lifetime;

                            Dirty(weapon, throwComp);
                            */
                            // мы делаем прикольчики

                            Timer.Spawn(marker.EndTime - _timing.CurTime,
                                () =>
                                {
                                    //Deferred
                                    RemComp<MeleeThrowOnHitComponent>(weapon);
                                });
                        }

                        if (TryComp<CrusherUpgradeHivelordComponent>(upgradeEntity, out var hivelord))
                        {
                            marker.EndTime += TimeSpan.FromSeconds(5);
                        }

                        if (TryComp<CrusherUpgradeWatcherComponent>(upgradeEntity, out var watcher))
                        {
                            var target = args.OtherEntity; 
                            var lifetime = watcher.Lifetime + ((int) marker.EndTime.TotalSeconds - (int) _timing.CurTime.TotalSeconds);

                            EnsureComp<IcyLookComponent>(target);

                            Timer.Spawn(TimeSpan.FromSeconds(lifetime), () =>
                            {
                                RemComp<IcyLookComponent>(target);
                            });
                        }
                    }
                }
            }
        }
    }

    private void AddImmunity(EntityUid uid, float time = 3f)
    {
        EnsureComp<DamageSquareImmunityComponent>(uid).HasImmunityUntil = _timing.CurTime + TimeSpan.FromSeconds(time);
    }
}
