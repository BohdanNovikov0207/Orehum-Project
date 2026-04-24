using Robust.Shared.Prototypes;

namespace Content.Shared.EntityEffects.Effects.PlantMetabolism;

public sealed partial class RobustHarvest : EventEntityEffect<RobustHarvest>
{
    [DataField]
    public int PotencyIncrease = 3;

    [DataField]
    public int PotencyLimit = 50;

    [DataField]
    public int PotencySeedlessThreshold = 30;

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys) =>
        Loc.GetString("reagent-effect-guidebook-plant-robust-harvest",
            ("seedlesstreshold", PotencySeedlessThreshold),
            ("limit", PotencyLimit),
            ("increase", PotencyIncrease),
            ("chance", Probability));
}
