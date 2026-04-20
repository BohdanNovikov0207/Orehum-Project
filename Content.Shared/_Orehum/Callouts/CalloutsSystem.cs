using Content.Shared.Radio;
using Content.Shared.Inventory;
using Content.Shared._RMC14.Marines.Announce;
using Content.Shared._RMC14.Marines.Squads;
using Content.Shared.Radio.Components;
using Robust.Shared.Prototypes;

namespace Content.Shared._Orehum.Callouts;

public abstract class CalloutsSystem : EntitySystem
{
    [Dependency] private readonly SharedMarineAnnounceSystem _marineAnnounce = default!;
    [Dependency] private readonly InventorySystem _inventory = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CalloutsComponent, AttackCalloutActionEvent>(OnAction);
        SubscribeLocalEvent<CalloutsComponent, DefendCalloutActionEvent>(OnAction);
        SubscribeLocalEvent<CalloutsComponent, RetreatCalloutActionEvent>(OnAction);
        SubscribeLocalEvent<CalloutsComponent, RallyCalloutActionEvent>(OnAction);
    }

    private void OnAction(Entity<CalloutsComponent> entity, ref AttackCalloutActionEvent args)
    {
        if (args.Handled)
            return;

        args.Handled = true;

        TrySendOrders(entity, entity.Comp.AttackOrderSays, entity.Comp.AttackEffectOnAction);
    }

    private void OnAction(Entity<CalloutsComponent> entity, ref DefendCalloutActionEvent args)
    {
        if (args.Handled)
            return;

        args.Handled = true;

        TrySendOrders(entity, entity.Comp.DefendOrderSays, entity.Comp.DefendEffectOnAction);
    }

    private void OnAction(Entity<CalloutsComponent> entity, ref RetreatCalloutActionEvent args)
    {
        if (args.Handled)
            return;

        args.Handled = true;

        TrySendOrders(entity, entity.Comp.RetreatOrderSays, entity.Comp.RetreatEffectOnAction);
    }

    private void OnAction(Entity<CalloutsComponent> entity, ref RallyCalloutActionEvent args)
    {
        if (args.Handled)
            return;

        args.Handled = true;

        TrySendOrders(entity, entity.Comp.RallyOrderSays, entity.Comp.RallyEffectOnAction);
    }

    private void TrySendOrders(EntityUid entity, List<LocId> listOrdersSays, EntProtoId effectOnAction)
    {
[Dependency] private readonly IRobustRandom _random = default!;
[Dependency] private readonly IGameTiming _timing = default!;

private void TrySendOrders(EntityUid entity, List<LocId> listOrdersSays, EntProtoId effectOnAction)
{
    if (!_timing.IsFirstTimePredicted)
        return;

    var selectedMessage = _random.Pick(listOrdersSays);

private void TrySendOrders(EntityUid entity, List<LocId> listOrdersSays, EntProtoId effectOnAction)
{
    if (!_timing.IsFirstTimePredicted)
        return;

    var selectedMessage = _random.Pick(listOrdersSays);

    if (_net.IsServer)
        Spawn(effectOnAction, Transform(entity).Coordinates);
}

        if (!TryGetHeadset(entity, out var headsetChannel))
        {
            return;
        }

        _marineAnnounce.AnnounceRadio(entity, selectedMessage, headsetChannel);
    }
    private ProtoId<RadioChannelPrototype>? TryGetSquadRadioChannel(EntityUid entity)
    {
        if (!TryComp<SquadMemberComponent>(entity, out var squad))
            return null;

        if (!TryComp<SquadTeamComponent>(squad.Squad, out var team))
            return null;

        return team.Radio;
    }

    private bool TryGetHeadset(EntityUid entity, out ProtoId<RadioChannelPrototype> channel)
    {
        channel = default;

        if (!_inventory.TryGetSlotEntity(entity, "ears", out var headsetEntity))
            return false;

        var squadChannel = TryGetSquadRadioChannel(entity);
        if (squadChannel.HasValue)
        {
            if (HasChannelInHeadset(entity, squadChannel.Value))
            {
                channel = squadChannel.Value;
                return true;
            }
        }

        if (TryComp<CalloutsComponent>(entity, out var ordersComp))
        {
            channel = ordersComp.DefaultFallbackChannel;
            return true;
        }

        return false;
    }

    private bool HasChannelInHeadset(EntityUid entity, ProtoId<RadioChannelPrototype> channel)
    {
        var slots = _inventory.GetSlotEnumerator(entity);
        while (slots.MoveNext(out var slot))
        {
            if (slot.ContainedEntity is not { } contained)
                continue;

            if (slot.ID != "ears")
                continue;

            if (TryComp<EncryptionKeyHolderComponent>(contained, out var keyHolder))
            {
                if (keyHolder.Channels.Contains(channel))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
