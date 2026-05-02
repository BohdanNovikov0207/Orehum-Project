using Robust.Shared.Audio;
using Content.Shared.Radio;
using Robust.Shared.Prototypes;

namespace Content.Shared._Starlight.Shipyard.Components;

[RegisterComponent]
public sealed partial class ShipyardConsoleComponent : Component
{
    /// <summary>
    /// Sound played when shuttle purchase validation fails.
    /// </summary>
    [DataField]
    public SoundSpecifier ErrorSound =
        new SoundPathSpecifier("/Audio/Effects/Cargo/buzz_sigh.ogg");

    /// <summary>
    /// Sound played when shuttle purchase succeeds.
    /// </summary>
    [DataField]
    public SoundSpecifier ConfirmSound =
        new SoundPathSpecifier("/Audio/Effects/Cargo/ping.ogg");

    /// <summary>
    /// Radio channel used for purchase announcements.
    /// </summary>
    [DataField]
    public ProtoId<RadioChannelPrototype> AnnouncementChannel = "Command";
}
