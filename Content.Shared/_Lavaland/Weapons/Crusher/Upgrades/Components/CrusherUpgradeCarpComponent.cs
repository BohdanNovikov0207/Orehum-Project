using Content.Shared.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Content.Shared.Damage;

namespace Content.Shared._Lavaland.Weapons.Crusher.Upgrades.Components;

[RegisterComponent]
public sealed partial class CrusherUpgradeCarpComponent : Component
{
    [ViewVariables(VVAccess.ReadWrite), DataField("damage")]
    public DamageSpecifier Damage = new();
}
