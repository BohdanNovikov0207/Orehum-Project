using Content.Server.Actions;
using Content.Server.Chat.Systems;
using Content.Shared._Orehum.Callouts;

namespace Content.Server._Orehum.Callouts;

public sealed class MCMarineOrdersSystem : CalloutsSystem
{
    [Dependency] private readonly ActionsSystem _actions = default!;
    [Dependency] private readonly ChatSystem _chat = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<CalloutsComponent, MapInitEvent>(OnOrdersMapInit);
        SubscribeLocalEvent<CalloutsComponent, ComponentShutdown>(OnOrdersShutdown);
    }

    private void OnOrdersMapInit(Entity<CalloutsComponent> entity, ref MapInitEvent ev)
    {
        var comp = entity.Comp;
        _actions.AddAction(entity, ref comp.AttackActionEntity, comp.AttackAction);
        _actions.SetUseDelay(comp.AttackActionEntity, comp.Cooldown);

        _actions.AddAction(entity, ref comp.DefendActionEntity, comp.DefendAction);
        _actions.SetUseDelay(comp.DefendActionEntity, comp.Cooldown);

        _actions.AddAction(entity, ref comp.RetreatActionEntity, comp.RetreatAction);
        _actions.SetUseDelay(comp.RetreatActionEntity, comp.Cooldown);

        _actions.AddAction(entity, ref comp.RallyActionEntity, comp.RallyAction);
        _actions.SetUseDelay(comp.RallyActionEntity, comp.Cooldown);
    }

    private void OnOrdersShutdown(Entity<CalloutsComponent> entity, ref ComponentShutdown ev)
    {
        _actions.RemoveAction(entity.Owner, entity.Comp.AttackActionEntity);
        _actions.RemoveAction(entity.Owner, entity.Comp.DefendActionEntity);
        _actions.RemoveAction(entity.Owner, entity.Comp.RetreatActionEntity);
        _actions.RemoveAction(entity.Owner, entity.Comp.RallyActionEntity);
    }
}
