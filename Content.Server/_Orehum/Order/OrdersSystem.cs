using Content.Server.Actions;
using Content.Server.Chat.Systems;
using Content.Shared.Chat;
using Content.Shared._Orehum.Orders;

namespace Content.Server._Orehum.Orders;

public sealed class OrdersSystem : SharedOrdersSystem
{
    [Dependency] private readonly ActionsSystem _actions = default!;
    [Dependency] private readonly ChatSystem _chat = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<OrdersComponent, MapInitEvent>(OnOrdersMapInit);
        SubscribeLocalEvent<OrdersComponent, ComponentShutdown>(OnOrdersShutdown);
    }

    private void OnOrdersMapInit(EntityUid uid, OrdersComponent comp, MapInitEvent ev)
    {
        // All the SetUseDelay calls are required because even tho we set the cooldown on all of them once an order
        // is issued for some reason the order that was pressed uses its delays and does not care about its cooldown
        // being set.
        _actions.AddAction(uid, ref comp.FocusActionEntity, comp.FocusAction);
        _actions.SetUseDelay(comp.FocusActionEntity, comp.Cooldown);
        _actions.AddAction(uid, ref comp.HoldActionEntity, comp.HoldAction);
        _actions.SetUseDelay(comp.HoldActionEntity, comp.Cooldown);
        _actions.AddAction(uid, ref comp.MoveActionEntity, comp.MoveAction);
        _actions.SetUseDelay(comp.MoveActionEntity, comp.Cooldown);
    }

    private void OnOrdersShutdown(EntityUid uid, OrdersComponent comp, ComponentShutdown ev)
    {
        _actions.RemoveAction(uid, comp.FocusActionEntity);
        _actions.RemoveAction(uid, comp.HoldActionEntity);
        _actions.RemoveAction(uid, comp.MoveActionEntity);
    }

    protected override void OnAction(EntityUid uid, OrdersComponent comp, MoveActionEvent ev)
    {
        base.OnAction(uid, comp, ev);
        OnAction(uid, comp.MoveCallouts);
    }

    protected override void OnAction(EntityUid uid, OrdersComponent comp, HoldActionEvent ev)
    {
        base.OnAction(uid, comp, ev);
        OnAction(uid, comp.HoldCallouts);
    }

    protected override void OnAction(EntityUid uid, OrdersComponent comp, FocusActionEvent ev)
    {
        base.OnAction(uid, comp, ev);
        OnAction(uid, comp.FocusCallouts);
    }

    private void OnAction(EntityUid uid, List<string> callouts)
    {
        var random = new Random();
        var callout = random.Next(0, callouts.Count);
        _chat.TrySendInGameICMessage(uid, Loc.GetString(callouts[callout]), InGameICChatType.Speak, false);
    }

}
