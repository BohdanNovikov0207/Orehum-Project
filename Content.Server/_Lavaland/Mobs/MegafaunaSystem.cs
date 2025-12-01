using Content.Shared._Lavaland.Aggression;
using Content.Shared._Lavaland.Audio;
using Content.Shared.Mobs;
using Content.Shared.Weapons.Melee.Events;
using Robust.Shared.Player;

using Content.Shared.Projectiles;
using Robust.Shared.Physics.Events;

namespace Content.Server._Lavaland.Mobs;

public sealed class MegafaunaSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MegafaunaComponent, AttackedEvent>(OnAttacked);
        SubscribeLocalEvent<MegafaunaComponent, MobStateChangedEvent>(OnDeath);

        SubscribeLocalEvent<MegafaunaComponent, StartCollideEvent>(OnCollide);
    }

    public void OnAttacked<T>(EntityUid uid, T comp, ref AttackedEvent args) where T : MegafaunaComponent
    {
        // это полный пиздец
        // просто
        // как они до такого дожили
        if (!HasComp<MegafaunaWeaponLooterComponent>(args.Used))
            comp.CrusherOnly = false; // it's over...
    }

    private void OnCollide(EntityUid uid, MegafaunaComponent comp, ref StartCollideEvent args)
    {
        var other = args.OtherEntity;

        if (!HasComp<MegafaunaWeaponLooterComponent>(other))
            comp.CrusherOnly = false;
    }

    public void OnDeath<T>(EntityUid uid, T comp, ref MobStateChangedEvent args) where T : MegafaunaComponent
    {
        var coords = Transform(uid).Coordinates;

        comp.CancelToken.Cancel();

        RaiseLocalEvent(uid, new MegafaunaKilledEvent());

        if (TryComp<BossMusicComponent>(uid, out var boss) &&
            TryComp<AggressiveComponent>(uid, out var aggresive))
        {
            var msg = new BossMusicStopEvent();
            foreach (var aggressor in aggresive.Aggressors)
            {
                if (!TryComp<ActorComponent>(aggressor, out var actor))
                    return;

                RaiseNetworkEvent(msg, actor.PlayerSession.Channel);
            }
        }

        if (comp.CrusherOnly && comp.CrusherLoot != null)
        {
            Spawn(comp.CrusherLoot, coords);
        }
        else if (comp.Loot != null)
        {
            Spawn(comp.Loot, coords);
        }

        QueueDel(uid);
    }
}
