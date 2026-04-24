using System.Numerics;

namespace Content.Shared.DeviceNetwork.Events;

/// <summary>
/// Event raised before a device network packet is send.
/// Subscribed to by other systems to prevent the packet from being sent.
/// </summary>
public sealed class BeforePacketSentEvent : CancellableEntityEventArgs
{
    /// <summary>
    /// The network the packet will be sent to.
    /// </summary>
    public readonly string NetworkId;

    /// <summary>
    /// The EntityUid of the entity the packet was sent from.
    /// </summary>
    public readonly EntityUid Sender;

    /// <summary>
    /// The senders current position in world coordinates.
    /// </summary>
    public readonly Vector2 SenderPosition;

    public readonly TransformComponent SenderTransform;

    public BeforePacketSentEvent(EntityUid sender, TransformComponent xform, Vector2 senderPosition, string networkId)
    {
        Sender = sender;
        SenderTransform = xform;
        SenderPosition = senderPosition;
        NetworkId = networkId;
    }
}
