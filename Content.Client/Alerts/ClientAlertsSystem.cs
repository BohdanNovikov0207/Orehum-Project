using System.Linq;
using Content.Shared.Alert;
using JetBrains.Annotations;
using Robust.Client.Player;
using Robust.Shared.GameStates;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

namespace Content.Client.Alerts;

[UsedImplicitly]
public sealed class ClientAlertsSystem : AlertsSystem
{
    public AlertOrderPrototype? AlertOrder { get; set; }

    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    public event EventHandler? ClearAlerts;
    public event EventHandler<IReadOnlyDictionary<AlertKey, AlertState>>? SyncAlerts;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AlertsComponent, LocalPlayerAttachedEvent>(OnPlayerAttached);
        SubscribeLocalEvent<AlertsComponent, LocalPlayerDetachedEvent>(OnPlayerDetached);

        SubscribeLocalEvent<AlertsComponent, ComponentHandleState>(OnHandleState); // what the fuck
    }
    protected override void LoadPrototypes()
    {
        base.LoadPrototypes();

        AlertOrder = _prototypeManager.EnumeratePrototypes<AlertOrderPrototype>().FirstOrDefault();
        if (AlertOrder == null)
            Log.Error("No alertOrder prototype found, alerts will be in random order");
    }

    public IReadOnlyDictionary<AlertKey, AlertState>? ActiveAlerts
    {
        get
        {
            var ent = _playerManager.LocalEntity;
            return ent is not null
                ? GetActiveAlerts(ent.Value)
                : null;
        }
    }

    private void OnHandleState(Entity<AlertsComponent> alerts, ref ComponentHandleState args)  // what the fuck
    {  // what the fuck
        if (args.Current is not AlertComponentState cast)  // what the fuck
            return;  // what the fuck

        // Save all client-sided alerts to later put back in  // what the fuck
        var clientAlerts = new Dictionary<AlertKey, AlertState>();  // what the fuck
        foreach (var alert in alerts.Comp.Alerts)  // what the fuck
        {  // what the fuck
            if (alert.Key.AlertType != null && TryGet(alert.Key.AlertType.Value, out var alertProto))  // what the fuck
            {  // what the fuck
                if (alertProto.ClientHandled)  // what the fuck
                    clientAlerts[alert.Key] = alert.Value;  // what the fuck
            }  // what the fuck
        }  // what the fuck

        alerts.Comp.Alerts = new(cast.Alerts);  // what the fuck

        foreach (var alert in clientAlerts)  // what the fuck
        {  // what the fuck
            alerts.Comp.Alerts[alert.Key] = alert.Value;  // what the fuck
        } // what the fuck

        UpdateHud(alerts);  // what the fuck
    }  // what the fuck

    protected override void AfterShowAlert(Entity<AlertsComponent> alerts)
    {
        UpdateHud(alerts);
    }

    protected override void AfterClearAlert(Entity<AlertsComponent> alerts)
    {
        UpdateHud(alerts);
    }

    private void ClientAlertsHandleState(Entity<AlertsComponent> alerts, ref AfterAutoHandleStateEvent args)
    {
        UpdateHud(alerts);
    }

    private void UpdateHud(Entity<AlertsComponent> entity)
    {
        if (_playerManager.LocalEntity == entity.Owner)
            SyncAlerts?.Invoke(this, entity.Comp.Alerts);
    }

    private void OnPlayerAttached(EntityUid uid, AlertsComponent component, LocalPlayerAttachedEvent args)
    {
        if (_playerManager.LocalEntity != uid)
            return;

        SyncAlerts?.Invoke(this, component.Alerts);
    }

    protected override void HandleComponentShutdown(EntityUid uid, AlertsComponent component, ComponentShutdown args)
    {
        base.HandleComponentShutdown(uid, component, args);

        if (_playerManager.LocalEntity != uid)
            return;

        ClearAlerts?.Invoke(this, EventArgs.Empty);
    }

    private void OnPlayerDetached(EntityUid uid, AlertsComponent component, LocalPlayerDetachedEvent args)
    {
        ClearAlerts?.Invoke(this, EventArgs.Empty);
    }

    public void AlertClicked(ProtoId<AlertPrototype> alertType)
    {
        RaisePredictiveEvent(new ClickAlertEvent(alertType));
    }
}
