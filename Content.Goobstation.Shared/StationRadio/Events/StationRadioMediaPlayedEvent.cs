using Robust.Shared.Audio;
using Robust.Shared.Serialization;

namespace Content.Goobstation.Shared.StationRadio.Events;

[Serializable] [NetSerializable]
public sealed class StationRadioMediaPlayedEvent : EntityEventArgs
{
    public StationRadioMediaPlayedEvent(SoundPathSpecifier Media)
    {
        MediaPlayed = Media;
    }

    public SoundPathSpecifier MediaPlayed { get; }
}
