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
    /// Anti-EORG measure. Will add pacified to all players upon round end.
    /// Its not perfect, but gets the job done.
    /// </summary>
    public static readonly CVarDef<bool> RoundEndPacifist =
        CVarDef.Create("game.round_end_pacifist", false, CVar.SERVERONLY);

    /*
     * No EORG
     */

    /// <summary>
    /// Whether the no EORG popup is enabled.
    /// </summary>
    public static readonly CVarDef<bool> RoundEndNoEorgPopup =
        CVarDef.Create("game.round_end_eorg_popup_enabled", true, CVar.SERVER | CVar.REPLICATED);

    /// <summary>
    /// Skip the no EORG popup.
    /// </summary>
    public static readonly CVarDef<bool> SkipRoundEndNoEorgPopup =
        CVarDef.Create("game.skip_round_end_eorg_popup", false, CVar.CLIENTONLY | CVar.ARCHIVE);

    /// <summary>
    /// How long to display the EORG popup for.
    /// </summary>
    public static readonly CVarDef<float> RoundEndNoEorgPopupTime =
        CVarDef.Create("game.round_end_eorg_popup_time", 5f, CVar.SERVER | CVar.REPLICATED);

    /*
     * Auto ACO
     */

    /// <summary>
    /// How long with no captain before requesting an ACO be elected.
    /// </summary>
    public static readonly CVarDef<TimeSpan> RequestAcoDelay =
        CVarDef.Create("game.request_aco_delay", TimeSpan.FromMinutes(15), CVar.SERVERONLY | CVar.ARCHIVE);

    /// <summary>
    /// Determines whether an ACO should be requested when the captain leaves during the round,
    /// in addition to cases where there are no captains at round start.
    /// </summary>
    public static readonly CVarDef<bool> RequestAcoOnCaptainDeparture =
        CVarDef.Create("game.request_aco_on_captain_departure", true, CVar.SERVERONLY | CVar.ARCHIVE);

    /// <summary>
    /// Determines whether All Access (AA) should be automatically unlocked if no captain is present.
    /// </summary>
    public static readonly CVarDef<bool> AutoUnlockAllAccessEnabled =
        CVarDef.Create("game.auto_unlock_aa_enabled", true, CVar.SERVERONLY | CVar.ARCHIVE);

    /// <summary>
    /// How long after an ACO request announcement is made before All Access (AA) should be unlocked.
    /// </summary>
    public static readonly CVarDef<TimeSpan> AutoUnlockAllAccessDelay =
        CVarDef.Create("game.auto_unlock_aa_delay", TimeSpan.FromMinutes(5), CVar.SERVERONLY | CVar.ARCHIVE);

    /*
     * Misc.
     */

    /// <summary>
    /// Whether the Shipyard is enabled.
    /// </summary>
    public static readonly CVarDef<bool> Shipyard =
        CVarDef.Create("shuttle.shipyard", true, CVar.SERVERONLY);

    /// <summary>
    ///    Maximum number of characters in objective summaries.
    /// </summary>
    public static readonly CVarDef<int> MaxObjectiveSummaryLength =
        CVarDef.Create("game.max_objective_summary_length", 512, CVar.SERVER | CVar.REPLICATED);

    /// <summary>
    /// Disables all vision filters for species like Vulpkanin or Harpies. There are good reasons someone might want to disable these.
    /// </summary>
    public static readonly CVarDef<bool> NoVisionFilters =
        CVarDef.Create("accessibility.no_vision_filters", false, CVar.CLIENTONLY | CVar.ARCHIVE);

    /*
     * Cosmic Cult
     */

    /// <summary>
    /// The multiplier for the difficulty of the monument.
    /// </summary>
    public static readonly CVarDef<float> CosmicCultistDifficultyMultiplier =
        CVarDef.Create("cosmiccult.difficulty_multiplier", 1.5f, CVar.SERVER);

    /// <summary>
    /// How much entropy a convert is worth towards the next monument tier.
    /// </summary>
    public static readonly CVarDef<int> CosmicCultistEntropyValue =
        CVarDef.Create("cosmiccult.cultist_entropy_value", 5, CVar.SERVER);

    /// <summary>
    /// How much of the crew the cult is aiming to convert for a tier 3 monument.
    /// </summary>
    public static readonly CVarDef<int> CosmicCultTargetConversionPercent =
        CVarDef.Create("cosmiccult.target_conversion_percent", 40, CVar.SERVER);

    /// <summary>
    /// How long the timer for the cult's stewardship vote lasts.
    /// </summary>
    public static readonly CVarDef<int> CosmicCultStewardVoteTimer =
        CVarDef.Create("cosmiccult.steward_vote_timer", 120, CVar.SERVER);

    /// <summary>
    /// The delay between the monument getting upgraded to tier 2 and the crew learning of that fact. the monument cannot be upgraded again in this time.
    /// </summary>
    public static readonly CVarDef<int> CosmicCultT2RevealDelaySeconds =
        CVarDef.Create("cosmiccult.t2_reveal_delay_seconds", 120, CVar.SERVER);

    /// <summary>
    /// The delay between the monument getting upgraded to tier 3 and the crew learning of that fact. the monument cannot be upgraded again in this time.
    /// </summary>
    public static readonly CVarDef<int> CosmicCultT3RevealDelaySeconds =
        CVarDef.Create("cosmiccult.t3_reveal_delay_seconds", 60, CVar.SERVER);

    /// <summary>
    /// The delay between the monument getting upgraded to tier 3 and the finale starting.
    /// </summary>
    public static readonly CVarDef<int> CosmicCultFinaleDelaySeconds =
        CVarDef.Create("cosmiccult.extra_entropy_for_finale", 150, CVar.SERVER);
}
