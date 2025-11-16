using Robust.Shared.Configuration;

namespace Content.Shared._DV.CCVars;

/// <summary>
/// DeltaV specific cvars.
/// </summary>
[CVarDefs]
// ReSharper disable once InconsistentNaming - Shush you
public sealed class DCCVars
{
    /// <summary>
    ///    Maximum number of characters in objective summaries.
    /// </summary>
    public static readonly CVarDef<int> MaxObjectiveSummaryLength =
        CVarDef.Create("game.max_objective_summary_length", 512, CVar.SERVER | CVar.REPLICATED);
}
