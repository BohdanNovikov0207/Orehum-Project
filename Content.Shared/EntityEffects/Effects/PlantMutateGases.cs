using Robust.Shared.Prototypes;

namespace Content.Shared.EntityEffects.Effects;

/// <summary>
/// changes the gases that a plant or produce create.
/// </summary>
public sealed partial class PlantMutateExudeGasses : EventEntityEffect<PlantMutateExudeGasses>
{
    [DataField]
    public float MaxValue = 0.5f;

    [DataField]
    public float MinValue = 0.01f;

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys) =>
        "TODO";
}

/// <summary>
/// changes the gases that a plant or produce consumes.
/// </summary>
public sealed partial class PlantMutateConsumeGasses : EventEntityEffect<PlantMutateConsumeGasses>
{
    [DataField]
    public float MaxValue = 0.5f;

    [DataField]
    public float MinValue = 0.01f;

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys) =>
        "TODO";
}
