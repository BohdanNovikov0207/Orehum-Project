using Content.Shared._Lavaland.Weapons.Crusher.Upgrades.Components;
using Content.Shared.Containers.ItemSlots;
using Content.Shared.Damage;
using Content.Shared.Weapons.Melee.Events;
using Robust.Shared.GameObjects;
using Content.Shared.FixedPoint;
using Content.Shared.Damage.Prototypes;

using System.Linq;

using Content.Shared.Weapons.Marker;
using Robust.Shared.Physics.Events;

using Content.Shared.Weapons.Melee.Components;
using Robust.Shared.Timing;

using Content.Shared.Projectiles;

using Content.Shared.Weapons.Ranged.Components;

using Content.Shared._Lavaland.Weapons.Crusher.Crests.Components;
using Content.Shared.Weapons.Ranged.Events;

namespace Content.Shared._Lavaland.Weapons.Crusher;

public sealed class CrusherUpgradeableSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _timing = default!;

    public bool magmaUpgradeFlag = false;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<CrusherUpgradeableComponent, MeleeHitEvent>(OnMeleeHit);

        SubscribeLocalEvent<CrusherUpgradeOriginalProtoComponent, ComponentRemove>(OnMagmaUpgradeRemoved);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<ItemSlotsComponent>();
        while (query.MoveNext(out var weapon, out var slots)) 
        {
            foreach (var slot in slots.Slots.Values)
            {
                var hasCrestTag = slot.Whitelist?.Tags?.Contains("CrusherCrest") == true;
                if (!hasCrestTag)
                    continue;

                var crestEntity = slot.Item;
                if (crestEntity is null)
                    continue;

                if (!TryComp(crestEntity.Value, out ItemSlotsComponent? crestSlots))
                    continue;

                foreach (var innerSlot in crestSlots.Slots.Values)
                {
                    var upgradeEntity = innerSlot.Item;
                    if (upgradeEntity is null)
                        continue;
                    

                    if (TryComp<CrusherUpgradeMagmaWatcherComponent>(upgradeEntity.Value, out var magma))
                    {
                        if (TryComp<BasicEntityAmmoProviderComponent>(weapon, out var ammoProvider))
                        {
                            var orig = EnsureComp<CrusherUpgradeOriginalProtoComponent>(weapon);
                            orig.OriginalProto ??= ammoProvider.Proto;

                            ammoProvider.Proto = magma.Proto;
                            Dirty(weapon, ammoProvider);
                            magmaUpgradeFlag = true;
                        }
                    }
                }
            }

            if (!magmaUpgradeFlag)
            {
                if (TryComp<CrusherUpgradeOriginalProtoComponent>(weapon, out var orig2) && orig2.OriginalProto != null)
                {
                    // костыль на костыле, говнокод на говнокоде
                    if (TryComp<BasicEntityAmmoProviderComponent>(weapon, out var ammoProvider2))
                    {
                        ammoProvider2.Proto = orig2.OriginalProto;
                        Dirty(weapon, ammoProvider2);
                        RemComp<CrusherUpgradeOriginalProtoComponent>(weapon);
                    }
                }
            }

            magmaUpgradeFlag = false;
        }
    }

    private void OnMagmaUpgradeRemoved(EntityUid uid, CrusherUpgradeOriginalProtoComponent comp, ComponentRemove args)
    {
        if (TryComp<BasicEntityAmmoProviderComponent>(uid, out var ammoProvider))
        {
            if (comp.OriginalProto != null)
            {
                ammoProvider.Proto = comp.OriginalProto;
                Dirty(uid, ammoProvider);
            }
        }
    }


    private void OnMeleeHit(EntityUid uid, CrusherUpgradeableComponent comp, MeleeHitEvent args)
    {
        if (!TryComp(uid, out ItemSlotsComponent? slots))
            return;

        foreach (var slot in slots.Slots.Values)
        {
            var hasCrestTag = slot.Whitelist?.Tags?.Contains("CrusherCrest") == true;


            if (!hasCrestTag) continue;

            var crestEntity = slot.Item;
            if (crestEntity is null)
                continue;

            if (!TryComp(crestEntity.Value, out ItemSlotsComponent? crestSlots))
                continue;

            foreach (var innerSlot in crestSlots.Slots.Values)
            {
                var upgradeEntity = innerSlot.Item;
                if (upgradeEntity is null)
                    continue;

                if (TryComp<CrusherUpgradeGoliathComponent>(upgradeEntity.Value, out var goliath))
                {
                    if (!TryComp<DamageableComponent>(args.User, out var damageable))
                    {
                        Logger.Warning($"Owner {args.User} missing damage or threshold components.");
                        return;
                    }

                    var extraDamageInt = (int) Math.Round(damageable.TotalDamage.Float() / 5.0);

                    if (goliath.Damage.DamageDict.Count > 0)
                    {
                        var firstType = goliath.Damage.DamageDict.Keys.First();
                        var extra = new DamageSpecifier();
                        extra.DamageDict[firstType] =
                            extra.DamageDict.GetValueOrDefault(firstType) + FixedPoint2.New(extraDamageInt);

                        args.BonusDamage += goliath.Damage;
                        args.BonusDamage += extra;
                    }
                    else
                    {
                        args.BonusDamage += goliath.Damage;
                    }
                }

                foreach (var target in args.HitEntities)
                {
                    if (HasComp<CarpBloodComponent>(target) && TryComp<CrusherUpgradeCarpComponent>(upgradeEntity.Value, out var carp))
                    {
                        args.BonusDamage += carp.Damage;
                    }
                }
            }
        }
    }
}
