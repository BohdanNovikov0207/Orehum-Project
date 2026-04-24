using Robust.Shared.Audio;

namespace Content.Goobstation.Shared.Vehicles;

[RegisterComponent]
public sealed partial class ForkliftComponent : Component
{
    [DataField]
    public int ForkliftCapacity = 4;

    [ViewVariables(VVAccess.ReadOnly)]
    public EntityUid? LiftAction;

    [DataField]
    public SoundSpecifier LiftSound;

    [ViewVariables(VVAccess.ReadOnly)]
    public TimeSpan? LiftSoundEndTime;

    [ViewVariables(VVAccess.ReadOnly)]
    public EntityUid? LiftSoundUid;


    [ViewVariables(VVAccess.ReadOnly)]
    public EntityUid? UnliftAction;
}
