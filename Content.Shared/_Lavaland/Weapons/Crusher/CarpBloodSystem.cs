using Content.Shared.Alert;
using Content.Shared.Movement.Systems;
using Content.Shared._Lavaland.Weapons.Crusher;

namespace Content.Shared._Lavaland.Weapons.Crusher;

public sealed class CarpBloodSystem : EntitySystem
{
    [Dependency] private readonly AlertsSystem _alertsSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CarpBloodComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<CarpBloodComponent, ComponentRemove>(OnRemove);
    }

    private void OnStartup(EntityUid uid, CarpBloodComponent component, ref ComponentStartup args)
    {
        _alertsSystem.ShowAlert(uid, component.CarpBloodAlertKey);
    }

    private void OnRemove(EntityUid uid, CarpBloodComponent component, ref ComponentRemove args)
    {
        if (TerminatingOrDeleted(uid))
            return;

        _alertsSystem.ClearAlert(uid, component.CarpBloodAlertKey);
    }
}
