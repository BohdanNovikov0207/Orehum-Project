using Content.Goobstation.Maths.FixedPoint;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Shared._Shitmed.Medical.Surgery.Traumas.Components;

[RegisterComponent] [AutoGenerateComponentState] [NetworkedComponent]
public sealed partial class BoneComponent : Component
{
    [DataField]
    public SoundSpecifier BoneBreakSound = new SoundCollectionSpecifier("BoneGone");

    [DataField] [AutoNetworkedField] [ViewVariables]
    public FixedPoint2 BoneIntegrity = 60f;

    [AutoNetworkedField] [ViewVariables]
    public BoneSeverity BoneSeverity = BoneSeverity.Normal;

    [AutoNetworkedField] [ViewVariables]
    public EntityUid? BoneWoundable;

    [DataField] [AutoNetworkedField] [ViewVariables]
    public FixedPoint2 IntegrityCap = 60f;
}
