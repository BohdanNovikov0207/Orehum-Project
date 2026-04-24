using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Shitcode.Heretic.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState(fieldDeltas: true)] [AutoGenerateComponentPause]
public sealed partial class HereticCosmicRuneComponent : Component
{
    [DataField]
    public TimeSpan Delay = TimeSpan.FromSeconds(2);

    [DataField]
    public EntProtoId Effect = "HereticRuneCosmosLight";

    [DataField] [AutoNetworkedField]
    public EntityUid? LinkedRune;

    [DataField] [AutoPausedField] [AutoNetworkedField]
    public TimeSpan NextUse = TimeSpan.Zero;

    [DataField]
    public float Range = 0.75f;

    [DataField]
    public SoundSpecifier? Sound = new SoundPathSpecifier("/Audio/_Goobstation/Heretic/cosmic_energy.ogg");
}
