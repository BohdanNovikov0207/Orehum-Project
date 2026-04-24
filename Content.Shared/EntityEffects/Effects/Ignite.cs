using Content.Shared.Database;
using Robust.Shared.Prototypes;

namespace Content.Shared.EntityEffects.Effects;

/// <summary>
/// Ignites a mob.
/// </summary>
public sealed partial class Ignite : EventEntityEffect<Ignite>
{
    public override bool ShouldLog => true;

    public override LogImpact LogImpact => LogImpact.Medium;

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => Loc.GetString("reagent-effect-guidebook-ignite", ("chance", Probability));
}
