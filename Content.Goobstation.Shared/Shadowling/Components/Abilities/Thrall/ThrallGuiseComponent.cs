using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Shadowling.Components.Abilities.Thrall;

/// <summary>
/// This is used for the Guise Ability.
/// </summary>
[RegisterComponent] [NetworkedComponent]
public sealed partial class ThrallGuiseComponent : Component
{
    [DataField]
    public EntityUid? ActionEnt;

    [DataField]
    public EntProtoId ActionId = "ActionGuise";

    /// <summary>
    /// Indicates whether the ability is active, or not.
    /// </summary>
    public bool Active;

    /// <summary>
    /// How long the effect lasts.
    /// </summary>
    [DataField]
    public TimeSpan GuiseDuration = TimeSpan.FromSeconds(10);

    [DataField]
    public TimeSpan NextUpdate = TimeSpan.Zero;
}
