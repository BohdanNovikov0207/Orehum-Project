//using Content.Shared._Lavaland.Mobs.Components;
using Content.Shared.Alert;
using Content.Shared.Movement.Systems;
using Content.Shared._Lavaland.Weapons.Crusher;

namespace Content.Shared._Lavaland.Weapons.Crusher;

public sealed class IcyLookSystem : EntitySystem
{
    [Dependency] private readonly AlertsSystem _alertsSystem = default!;
    [Dependency] private readonly MovementSpeedModifierSystem _movementSpeed = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<IcyLookComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<IcyLookComponent, ComponentRemove>(OnRemove);
        SubscribeLocalEvent<IcyLookComponent, RefreshMovementSpeedModifiersEvent>(OnRefreshSpeed);
    }

    private void OnStartup(EntityUid uid, IcyLookComponent component, ref ComponentStartup args)
    {
        _alertsSystem.ShowAlert(uid, component.IcyLookAlertKey);
        _movementSpeed.RefreshMovementSpeedModifiers(uid);
    }

    private void OnRemove(EntityUid uid, IcyLookComponent component, ref ComponentRemove args)
    {
        if (TerminatingOrDeleted(uid))
            return;

        _alertsSystem.ClearAlert(uid, component.IcyLookAlertKey);
    }

    private void OnRefreshSpeed(EntityUid uid, IcyLookComponent component, ref RefreshMovementSpeedModifiersEvent args)
    {
        args.ModifySpeed(component.MovementSpeedBuff, component.MovementSpeedBuff);
    }
}
