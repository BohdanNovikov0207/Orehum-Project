using Robust.Shared.Prototypes;

namespace Content.Shared.EntityEffects.Effects.PlantMetabolism;

public sealed partial class PlantChangeStat : EventEntityEffect<PlantChangeStat>
{
    [DataField]
    public float MaxValue;

    [DataField]
    public float MinValue;

    [DataField]
    public int Steps;

    [DataField]
    public string TargetValue;

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys) =>
        throw new NotImplementedException();
}
