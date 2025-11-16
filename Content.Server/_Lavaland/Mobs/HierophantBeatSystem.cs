using Content.Shared._Lavaland.Mobs.Components;
using Content.Shared.Alert;
using Content.Shared.Movement.Systems;

namespace Content.Server._Lavaland.Mobs;

public sealed class HierophantBeatSystem : EntitySystem
{
    [Dependency] private readonly AlertsSystem _alertsSystem = default!;
    [Dependency] private readonly MovementSpeedModifierSystem _movementSpeed = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<HierophantBeatComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<HierophantBeatComponent, ComponentRemove>(OnRemove);
        SubscribeLocalEvent<HierophantBeatComponent, RefreshMovementSpeedModifiersEvent>(OnRefreshSpeed);
    }

    private void OnStartup(EntityUid uid, HierophantBeatComponent component, ref ComponentStartup args)
    {
        _alertsSystem.ShowAlert(uid, component.HierophantBeatAlertKey);
        _movementSpeed.RefreshMovementSpeedModifiers(uid);
    }

    private void OnRemove(EntityUid uid, HierophantBeatComponent component, ref ComponentRemove args)
    {
        if (TerminatingOrDeleted(uid))
            return;
        
        _alertsSystem.ClearAlert(uid, component.HierophantBeatAlertKey);
    }

    private void OnRefreshSpeed(EntityUid uid, HierophantBeatComponent component, ref RefreshMovementSpeedModifiersEvent args)
    {
        args.ModifySpeed(component.MovementSpeedBuff, component.MovementSpeedBuff);
    }
}
