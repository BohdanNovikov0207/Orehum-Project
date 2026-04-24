using Robust.Shared.Audio;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Goobstation.Shared.Sound.Components;

/// <summary>
/// Plays a sound (or random entry from a sound collection) at random intervals.
/// </summary>
[RegisterComponent]
[AutoGenerateComponentPause]
public sealed partial class RandomIntervalSoundComponent : Component
{
    [DataField]
    public TimeSpan MaxInterval = TimeSpan.FromSeconds(18);

    [DataField]
    public TimeSpan MinInterval = TimeSpan.FromSeconds(8);

    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer))] [AutoPausedField]
    public TimeSpan NextSound;

    /// <summary>
    /// If true, the sound will only play if the owner/wearer is alive.
    /// </summary>
    [DataField("requireAlive")]
    public bool RequireAlive = false;

    /// <summary>
    /// If true, the sound will only play while this entity is equipped as clothing.
    /// </summary>
    [DataField("requireEquipped")]
    public bool RequireEquipped = false;

    [DataField("sound")]
    public SoundSpecifier Sound = new SoundPathSpecifier("/Audio/Items/bikehorn.ogg");
}
