using Content.Shared.Radio;
using Content.Shared.Inventory;
using Content.Shared.Radio.Components;
using Robust.Shared.Prototypes;

namespace Content.Shared._Orehum.Callouts;

public abstract class CalloutsSystem : EntitySystem
{
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
        var random = new System.Random();
        var selectedMessage = listOrdersSays[random.Next(0, listOrdersSays.Count)];

        Spawn(effectOnAction, Transform(entity).Coordinates);

        if (!TryGetHeadset(entity, out var headsetChannel))
            return;

        AnnounceOnRadio(entity, selectedMessage, headsetChannel);
    }

    protected virtual void AnnounceOnRadio(EntityUid entity, LocId message, ProtoId<RadioChannelPrototype> channel) { }

    private bool TryGetHeadset(EntityUid entity, out ProtoId<RadioChannelPrototype> channel)
    {
        channel = default;

        if (!_inventory.TryGetSlotEntity(entity, "ears", out _))
            return false;

        if (TryComp<CalloutsComponent>(entity, out var ordersComp))
        {
            channel = ordersComp.DefaultFallbackChannel;
            return true;
        }

        return false;
    }
}
