using Robust.Shared.Map;
using Robust.Shared.Serialization;

namespace Content.Goobstation.Common.Heretic;

[Serializable] [NetSerializable]
public sealed class ButtonTagPressedEvent(string id, NetEntity user, NetCoordinates coords) : EntityEventArgs
{
    public NetCoordinates Coords = coords;

    public string Id = id;
    public NetEntity User = user;
}

[ByRefEvent]
public record struct HereticCheckEvent(EntityUid Uid, bool Result = false);
