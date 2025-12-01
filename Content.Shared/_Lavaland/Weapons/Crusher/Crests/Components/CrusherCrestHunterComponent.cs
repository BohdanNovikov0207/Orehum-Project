using Content.Shared.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Content.Shared.Damage;

namespace Content.Shared._Lavaland.Weapons.Crusher.Crests.Components;

[RegisterComponent]
public sealed partial class CrusherCrestHunterComponent : Component
{

}

[RegisterComponent]
public sealed partial class CrusherUpgradeReloadComponent : Component
{
    [DataField]
    public float ReloadCoefficient = 0.1f;
    [DataField]
    public float? OldFireRate;
}
