using Content.Shared.Chat.Prototypes;
using Content.Shared.Whitelist;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.MobCall;

/// <summary>
/// Lets this entity use MobCallAction actions to cause nearby specified mobs with the proper HTN to go to it.
/// </summary>
[RegisterComponent]
public sealed partial class MobCallSourceComponent : Component
{
    [DataField]
    public ProtoId<EmotePrototype> Emote = "Scream";

    [DataField]
    public string Key = "CallTarget";

    [DataField]
    public float Range = 20f;

    /// <summary>
    /// Whitelist of entities to work on.
    /// </summary>
    [DataField(required: true)]
    public EntityWhitelist Whitelist = new();
}
