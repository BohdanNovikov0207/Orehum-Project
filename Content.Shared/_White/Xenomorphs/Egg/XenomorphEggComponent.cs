using Robust.Shared.Audio;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._White.Xenomorphs.Egg;

[RegisterComponent]
public sealed partial class XenomorphEggComponent : Component
{
    [ViewVariables]
    public TimeSpan BurstAt = TimeSpan.Zero;

    [DataField]
    public TimeSpan BurstingDelay = TimeSpan.FromSeconds(1.5f);

    [DataField]
    public float BurstRange = 1f;

    [ViewVariables]
    public TimeSpan CheckInRangeAt = TimeSpan.Zero;

    [DataField]
    public TimeSpan CheckInRangeDelay = TimeSpan.FromSeconds(1);

    [DataField]
    public SoundSpecifier? CleaningSound = new SoundPathSpecifier("/Audio/Animals/Blob/blobattack.ogg");

    [DataField]
    public EntProtoId? FaceHuggerPrototype = "MobXenomorphFaceHugger";

    [ViewVariables]
    public TimeSpan GrownAt = TimeSpan.Zero;

    [DataField]
    public TimeSpan MaxGrowthTime = TimeSpan.FromSeconds(150);

    [DataField]
    public TimeSpan MinGrowthTime = TimeSpan.FromSeconds(90);

    [DataField]
    public XenomorphEggStatus Status = XenomorphEggStatus.Growning;
}

public enum XenomorphEggStatus : byte
{
    Burst,
    Bursting,
    Grown,
    Growning,
}

[Serializable] [NetSerializable]
public enum XenomorphEggVisualsStatus : byte
{
    Burst,
    Bursting,
    Grown,
    Growning,
}

[Serializable] [NetSerializable]
public enum XenomorphEggKey
{
    Key,
}
