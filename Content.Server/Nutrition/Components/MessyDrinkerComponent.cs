using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.Nutrition.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Server.Nutrition.Components;

/// <summary>
/// Entities with this component occasionally spill some of the solution they're ingesting.
/// </summary>
[RegisterComponent]
public sealed partial class MessyDrinkerComponent : Component
{
    /// <summary>
    /// The types of food prototypes we can spill
    /// </summary>
    [DataField]
    public List<ProtoId<EdiblePrototype>> SpillableTypes = new() { "Drink" };

    /// <summary>
    /// The amount of solution that is spilled when <see cref="SpillChance" /> procs.
    /// </summary>
    [DataField]
    public FixedPoint2 SpillAmount = 1.0;

    [DataField]
    public float SpillChance = 0.2f;

    [DataField]
    public LocId? SpillMessagePopup;
}
