using Robust.Shared.Serialization;

namespace Content.Shared._Orehum.Interaction;

[Serializable] [NetSerializable]
public sealed class InteractionParticleEvent(NetEntity performer, NetEntity? used, NetEntity target, bool isClientEvent)
    : EntityEventArgs
{
    /// <summary>
    /// Workaround for event subscription not working w/ the session overload
    /// </summary>
    public bool IsClientEvent = isClientEvent;

    public NetEntity Performer = performer;

    public NetEntity Target = target;

    public NetEntity? Used = used;
}
