using System.Numerics;
using System.Runtime.CompilerServices;
using Content.Shared.Damage;
using Content.Shared.Damage.Prototypes;
using Content.Shared.Mobs.Components;
using Content.Shared.Popups;
using Content.Shared.Standing;
using Content.Shared.Weapons.Melee.Events;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Network;
using Robust.Shared.Prototypes;

namespace Content.Shared._White.BackStab;

public sealed class BackStabSystem : EntitySystem
{
    [Robust.Shared.IoC.Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Robust.Shared.IoC.Dependency] private readonly INetManager _net = default!;
    [Robust.Shared.IoC.Dependency] private readonly SharedTransformSystem _transform = default!;
    [Robust.Shared.IoC.Dependency] private readonly SharedPopupSystem _popup = default!;
    [Robust.Shared.IoC.Dependency] private readonly SharedAudioSystem _audio = default!;
    [Robust.Shared.IoC.Dependency] private readonly StandingStateSystem _standing = default!;

    public static readonly SoundSpecifier BackstabSound =
        new SoundPathSpecifier("/Audio/_Goobstation/Weapons/Effects/guillotine.ogg");

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<BackStabComponent, MeleeHitEvent>(HandleHit);
    }

    private void HandleHit(Entity<BackStabComponent> ent, ref MeleeHitEvent args)
    {
        if (ent.Comp.DamageMultiplier < 1f || !args.IsHit || args.HitEntities.Count != 1)
            return;

        var target = args.HitEntities[0];

        if (!TryBackstab(target, args.User, ent.Comp.Tolerance))
            return;

        var total = args.BaseDamage.GetTotal();

        var damage = total * ent.Comp.DamageMultiplier;

        args.BonusDamage += new DamageSpecifier(_prototypeManager.Index<DamageTypePrototype>("Slash"), damage - total);
    }

    public bool TryBackstab(EntityUid target,
        EntityUid user,
        Angle tolerance,
        bool showPopup = true,
        bool playSound = true,
        bool alwaysBackstabLaying = true)
    {
        if (target == user || !HasComp<MobStateComponent>(target))
            return false;

        if (alwaysBackstabLaying && _standing.IsDown(target))
        {
            BackstabEffects(target, showPopup, playSound);
            return true;
        }

        var xform = Transform(target);
        var userXform = Transform(user);
        var v1 = -_transform.GetWorldRotation(xform).ToWorldVec();
        var v2 = _transform.GetWorldPosition(userXform) - _transform.GetWorldPosition(xform);
        var angle = CalculateAngle(v1, v2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float CalculateAngle(Vector2 first, Vector2 second)
        {
            return MathF.Acos(Dot(first, second) / (first.Length() * second.Length()));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float Dot(Vector2 left, Vector2 right)
        {
            return left.X * right.X + left.Y * right.Y;
        }

        if (angle > tolerance.Theta)
            return false;

        BackstabEffects(target, showPopup, playSound);
        return true;
    }

    private void BackstabEffects(EntityUid target, bool showPopup = true, bool playSound = true)
    {
        if (_net.IsClient)
            return;

        if (showPopup)
            _popup.PopupEntity(Loc.GetString("backstab-message"), target, PopupType.LargeCaution);

        if (playSound)
            _audio.PlayPvs(BackstabSound, target);
    }
}
