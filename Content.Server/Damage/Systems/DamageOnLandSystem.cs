// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Adventure.Bartender.Systems; // Adventure
using Content.Server.Damage.Components;
using Content.Shared.Damage;
using Content.Shared.Nutrition.Components;
using Content.Shared.Nutrition.EntitySystems; // Adventure
using Content.Shared.Throwing;

namespace Content.Server.Damage.Systems
{
    /// <summary>
    /// Damages the thrown item when it lands.
    /// </summary>
    public sealed class DamageOnLandSystem : EntitySystem
    {
        [Dependency] private readonly DamageableSystem _damageableSystem = default!;
        [Dependency] private readonly SpillProofThrowerSystem _nonspillthrower = default!; // Adventure

        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<DamageOnLandComponent, LandEvent>(DamageOnLand);
        }

        private void DamageOnLand(EntityUid uid, DamageOnLandComponent component, ref LandEvent args)
        {
            // Adventure start
            if (args.User is { } user
                && TryComp<EdibleComponent>(uid, out var edible)
                && edible.Edible == IngestionSystem.Drink
                && _nonspillthrower.GetSpillProofThrow(user))
            {
                return;
            }
            // Adventure end
            _damageableSystem.TryChangeDamage(uid, component.Damage, component.IgnoreResistances);
        }
    }
}
