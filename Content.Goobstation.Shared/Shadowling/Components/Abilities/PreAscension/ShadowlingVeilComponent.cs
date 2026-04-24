using Content.Shared.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Shadowling.Components.Abilities.PreAscension;

/// <summary>
/// This is used for Veil Ability
/// </summary>
[RegisterComponent] [NetworkedComponent]
public sealed partial class ShadowlingVeilComponent : Component
{
    [DataField]
    public EntityUid? ActionEnt;

    [DataField]
    public EntProtoId ActionId = "ActionVeil";

    /// <summary>
    /// Indicates the range radius which the ability will search for, once used.
    /// </summary>
    [DataField]
    public float Range = 9f;

    [DataField]
    public ProtoId<TagPrototype> TorchTag = "Torch";
}
