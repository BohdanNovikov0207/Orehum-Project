using Content.Shared._Lavaland.Weapons.Crusher;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared._Lavaland.Weapons.Crusher.Crests.Components;
// 100% мучу какой-то всратый велосипед, который уже изобрели до меня
namespace Content.Shared._Lavaland.Weapons.Crusher.Crests;

public sealed class CrusherUpgradeReloadSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CrusherUpgradeReloadComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<CrusherUpgradeReloadComponent, ComponentRemove>(OnRemove);
    }

    private void OnStartup(EntityUid uid, CrusherUpgradeReloadComponent component, ref ComponentStartup args)
    {
        if (TryComp<GunComponent>(uid, out var gunComponent))
        {
            component.OldFireRate = gunComponent.FireRate;
            gunComponent.FireRate *= component.ReloadCoefficient;
        }
    }

    private void OnRemove(EntityUid uid, CrusherUpgradeReloadComponent component, ref ComponentRemove args)
    {
        if (TerminatingOrDeleted(uid))
            return;
        if (TryComp<GunComponent>(uid, out var gunComponent))
        {
            if (component.OldFireRate != null)
            {
                gunComponent.FireRate = (float) component.OldFireRate;
            }
        }
        
    }
}
