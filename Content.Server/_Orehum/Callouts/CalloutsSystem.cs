using Content.Server.Actions;
using Content.Server.Chat.Systems;
using Content.Shared.Inventory;
using Content.Shared.Whitelist;
using Content.Shared._RMC14.Marines.Announce;
using Content.Shared._Orehum.Callouts;
using Robust.Shared.Random;

namespace Content.Server._Orehum.Callouts;

public sealed class MCMarineOrdersSystem : MCSharedSendOrdersSystem
{
    [Dependency] private readonly ActionsSystem _actions = default!;
    [Dependency] private readonly ChatSystem _chat = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly SharedMarineAnnounceSystem _marineAnnounce = default!;
    [Dependency] private readonly InventorySystem _inventory = default!;
    [Dependency] private readonly EntityWhitelistSystem _whitelist = default!;

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
