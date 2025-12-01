using Content.Shared.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Lavaland.Weapons.Crusher.Upgrades.Components;

[RegisterComponent]
public sealed partial class CrusherUpgradeWatcherComponent : Component
{
   [DataField("lifetime")]
    public float Lifetime = 2f;
}
