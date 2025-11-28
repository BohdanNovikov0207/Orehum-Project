using Content.Shared.Alert;
using Content.Shared.Damage;
using Content.Shared.Damage.Components;

namespace Content.Shared._Lavaland.Weapons.Crusher;

public sealed class VigilanteEyeSystem : EntitySystem
{
    [Dependency] private readonly AlertsSystem _alertsSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<VigilanteEyeComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<VigilanteEyeComponent, ComponentRemove>(OnRemove);
    }

    private void OnStartup(EntityUid uid, VigilanteEyeComponent component, ref ComponentStartup args)
    {
        _alertsSystem.ShowAlert(uid, component.VigilanteEyeAlertKey);
        EnsureComp<GodmodeComponent>(uid);
    }

    private void OnRemove(EntityUid uid, VigilanteEyeComponent component, ref ComponentRemove args)
    {
        if (TerminatingOrDeleted(uid))
            return;

        _alertsSystem.ClearAlert(uid, component.VigilanteEyeAlertKey);
        if (HasComp<GodmodeComponent>(uid))
            RemComp<GodmodeComponent>(uid);
    }
}
