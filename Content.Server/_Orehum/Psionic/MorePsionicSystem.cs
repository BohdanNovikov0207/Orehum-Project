using Content.Server.Abilities.Psionics;
using Content.Server.Psionics;
using Content.Shared._Orehum.Psionic;
using Content.Shared.Abilities.Psionics;
using Content.Shared.Chemistry.EntitySystems;
using Content.Shared.Chemistry.Reagent;
using Content.Shared.Damage;
using Content.Shared.FixedPoint;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Systems;
using Content.Shared.Nutrition.Components;
using Content.Shared.Popups;
using Content.Shared.Psionics.Glimmer;
using Robust.Shared.Prototypes;


namespace Content.Server._Orehum.Psionic;

public sealed class MorePsionicSystem : EntitySystem
{
    private static readonly FixedPoint2 MaxPoison = FixedPoint2.New(10f);

    [Dependency] private readonly SharedPsionicAbilitiesSystem _sharedPsionicsAbilities = default!;
    [Dependency] private readonly PsionicAbilitiesSystem _psionicAbilities = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly PsionicsSystem _psionics = default!;
    [Dependency] private readonly MobThresholdSystem _threshold = default!;
    [Dependency] private readonly DamageableSystem _damageable = default!;
    [Dependency] private readonly GlimmerSystem _glimmer = default!;
    [Dependency] private readonly SharedSolutionContainerSystem _solutionContainer = default!;
    //[Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PoisonFoodActionEvent>(OnPoisonFoodActionEvent);
        SubscribeLocalEvent<EatPsionicActionEvent>(OnEatPsionicActionEvent);
    }

    private void OnPoisonFoodActionEvent(PoisonFoodActionEvent args)
    {
        if (_sharedPsionicsAbilities.OnAttemptPowerUse(args.Performer, "PoisonFood", true))
        {
            if (!TryComp<PsionicComponent>(args.Performer, out var performerPsionic)
                || !TryComp<FoodComponent>(args.Target, out var food)
                || !_solutionContainer.TryGetSolution((args.Target, null), food.Solution, out var reagentsEnt))
                return;

            var remain = MaxPoison;
            var add = 0;
            var solution = reagentsEnt.Value.Comp.Solution;
            for (var i = 0; i < solution.Contents.Count; i++)
            {
                if (remain <= FixedPoint2.Zero)
                    break;

                var content = solution.Contents[i];
                if (content.Quantity >= remain)
                {
                    remain -= content.Quantity;
                    add += content.Quantity.Int();
                    solution.RemoveReagent(content);
                    i--;
                }
                else
                {
                    add += remain.Int();
                    solution.RemoveReagent(new(content.Reagent, remain));
                    break;
                }
            }

            solution.AddReagent("Toxin", add);

            _glimmer.DeltaGlimmerOutput(5);

            _popup.PopupCoordinates("Вас наполняет удовольствие за содеянное!", Comp<TransformComponent>(args.Performer).Coordinates, args.Performer);

            _sharedPsionicsAbilities.LogPowerUsed(args.Performer, "PoisonFood");
            args.Handled = true;
        }
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
