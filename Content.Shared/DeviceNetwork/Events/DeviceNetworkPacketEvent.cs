namespace Content.Shared.DeviceNetwork.Events;

/// <summary>
/// Event raised when a device network packet gets sent.
/// </summary>
public sealed class DeviceNetworkPacketEvent : EntityEventArgs
{
    /// <summary>
    /// The data that is being sent.
    /// </summary>
    public readonly NetworkPayload Data;

    /// <summary>
    /// The frequency the packet is sent on.
    /// </summary>
    public readonly uint Frequency;

    /// <summary>
    /// The device network address of the sending entity.
    /// </summary>
    public readonly string SenderAddress;

    /// <summary>
    /// Address of the intended recipient. Null if the message was broadcast.
    /// </summary>
    public string? Address;

    /// <summary>
    /// The id of the network that this packet is being sent on.
    /// </summary>
    public int NetId;

    /// <summary>
    /// The entity that sent the packet.
    /// </summary>
    public EntityUid Sender;

    public DeviceNetworkPacketEvent(int netId,
        string? address,
        uint frequency,
        string senderAddress,
        EntityUid sender,
        NetworkPayload data)
    {
        NetId = netId;
        Address = address;
        Frequency = frequency;
        SenderAddress = senderAddress;
        Sender = sender;
        Data = data;
    }
}
