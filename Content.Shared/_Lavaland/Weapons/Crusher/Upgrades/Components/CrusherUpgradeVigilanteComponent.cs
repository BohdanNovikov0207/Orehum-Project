using Content.Shared.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Content.Shared.Damage;

namespace Content.Shared._Lavaland.Weapons.Crusher.Upgrades.Components;

[RegisterComponent]
public sealed partial class CrusherUpgradeVigilanteComponent : Component
{
    [DataField("lifetime")]
    public float Lifetime = 1f;
}
