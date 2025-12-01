using Robust.Shared.GameStates;

namespace Content.Shared._Lavaland.Weapons.Crusher;

[RegisterComponent, NetworkedComponent]
public sealed partial class CarpBloodComponent : Component
{
    [DataField]
    public string CarpBloodAlertKey = "CarpBlood";
}
