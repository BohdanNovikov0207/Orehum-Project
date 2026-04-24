using Content.Shared._Lavaland.Megafauna.Selectors;
using JetBrains.Annotations;

namespace Content.Shared._Lavaland.Megafauna.Conditions.Targeting;

/// <summary>
/// Universal parent for all megafauna conditions that
/// check something on a target entity.
/// Used in selectors like <see cref="AggressivePickTargetSelector" /> to check
/// all possible variants and return just one best target out of all possibilities.
/// </summary>
[ImplicitDataDefinitionForInheritors]
[MeansImplicitUse]
public abstract partial class MegafaunaEntityCondition
{
    /// <summary>
    /// How much weight this condition has when it had failed.
    /// </summary>
    [DataField]
    public float FailWeight;

    /// <summary>
    /// Can be used to make some conditions have more influence than others.
    /// </summary>
    [DataField]
    public float Weight = 1f;

    public float Evaluate(MegafaunaCalculationBaseArgs args, EntityUid target) =>
        EvaluateImplementation(args, target) ? Weight : FailWeight;

    public abstract bool EvaluateImplementation(MegafaunaCalculationBaseArgs args, EntityUid target);
}
