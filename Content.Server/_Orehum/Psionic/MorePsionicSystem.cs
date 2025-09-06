using Content.Server.Abilities.Psionics;
using Content.Server.Psionics;
using Content.Shared._Orehum.Psionic;
using Content.Shared.Abilities.Psionics;
using Content.Shared.Damage;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Systems;
using Content.Shared.Popups;
using Content.Shared.Psionics.Glimmer;

namespace Content.Server._Orehum.Psionic;

public sealed class MorePsionicSystem : EntitySystem
{
    [Dependency] private readonly SharedPsionicAbilitiesSystem _sharedPsionicsAbilities = default!;
    [Dependency] private readonly PsionicAbilitiesSystem _psionicAbilities = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly PsionicsSystem _psionics = default!;
    [Dependency] private readonly MobThresholdSystem _threshold = default!;
    [Dependency] private readonly DamageableSystem _damageable = default!;
    [Dependency] private readonly GlimmerSystem _glimmer = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<EatPsionicActionEvent>(OnEatPsionicActionEvent);
    }

    private void OnEatPsionicActionEvent(EatPsionicActionEvent args)
    {
        if (_sharedPsionicsAbilities.OnAttemptPowerUse(args.Performer, "EatPsionic", true))
        {
            if (!TryComp<PsionicComponent>(args.Performer, out var performerPsionic)
                || !TryComp<PsionicComponent>(args.Target, out var targetPsionic))
                return;

            foreach (var targetPsionicActivePower in targetPsionic.ActivePowers)
                _psionicAbilities.InitializePsionicPower(args.Performer, targetPsionicActivePower, performerPsionic, true);

            _psionicAbilities.MindBreak(args.Target);

            var half = _threshold.GetThresholdForState(args.Target, MobState.Critical).Int() / 2;
            _damageable.TryChangeDamage(
                args.Target,
                new()
                {
                    DamageDict =
                    {
                        {"Cellular", half }
                    }
                },
                ignoreResistances: true);

            _glimmer.DeltaGlimmerOutput(50);

            _popup.PopupCoordinates("Вы чувствуете как теряете свои силы!", Comp<TransformComponent>(args.Target).Coordinates, args.Target);
            _popup.PopupCoordinates("Новые силы наполняют вас!", Comp<TransformComponent>(args.Performer).Coordinates, args.Performer);

            _sharedPsionicsAbilities.LogPowerUsed(args.Performer, "EatPsionic");
            args.Handled = true;
        }
    }
}
