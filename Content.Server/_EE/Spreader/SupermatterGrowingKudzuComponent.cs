using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Server._EE.Spreader;

[RegisterComponent, Access(typeof(SupermatterKudzuSystem)), AutoGenerateComponentPause]
public sealed partial class SupermatterGrowingKudzuComponent : Component
{
    [DataField("nextTick", customTypeSerializer: typeof(TimeOffsetSerializer))]
    [AutoPausedField]
    public TimeSpan NextTick = TimeSpan.Zero;
}
