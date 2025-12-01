using Robust.Shared.GameStates;

using Content.Shared.Damage.Prototypes;
using Robust.Shared.Prototypes;

namespace Content.Shared._Lavaland.Weapons.Crusher;

[RegisterComponent, NetworkedComponent]
public sealed partial class VigilanteEyeComponent : Component
{
    [DataField]
    public string VigilanteEyeAlertKey = "VigilanteEye";
}
