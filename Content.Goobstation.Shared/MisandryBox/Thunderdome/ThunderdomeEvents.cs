using Robust.Shared.Serialization;

namespace Content.Goobstation.Shared.MisandryBox.Thunderdome;

/// <summary>
/// Ghost requests to join the thunderdome.
/// </summary>
[Serializable] [NetSerializable]
public sealed class ThunderdomeJoinRequestEvent : EntityEventArgs;

/// <summary>
/// Player requests to leave the thunderdome.
/// </summary>
[Serializable] [NetSerializable]
public sealed class ThunderdomeLeaveRequestEvent : EntityEventArgs;

/// <summary>
/// Server broadcasts the current thunderdome player count to ghosts.
/// </summary>
[Serializable] [NetSerializable]
public sealed class ThunderdomePlayerCountEvent : EntityEventArgs
{
    public ThunderdomePlayerCountEvent(int count)
    {
        Count = count;
    }

    public int Count { get; }
}

/// <summary>
/// Server announces kill streaks / events to thunderdome participants.
/// </summary>
[Serializable] [NetSerializable]
public sealed class ThunderdomeAnnouncementEvent : EntityEventArgs
{
    public ThunderdomeAnnouncementEvent(string message)
    {
        Message = message;
    }

    public string Message { get; }
}

/// <summary>
/// Server notifies a thunderdome player that their original station body has been revived.
/// </summary>
[Serializable] [NetSerializable]
public sealed class ThunderdomeRevivalOfferEvent : EntityEventArgs;

/// <summary>
/// Client accepts the revival offer and wants to return to their original body.
/// </summary>
[Serializable] [NetSerializable]
public sealed class ThunderdomeRevivalAcceptEvent : EntityEventArgs;
