using Robust.Shared.GameStates;

namespace Content.Shared._Lavaland.Weapons.Crusher;

[RegisterComponent, NetworkedComponent]
public sealed partial class IcyLookComponent : Component
{
    [DataField]
    public float MovementSpeedBuff = 0.70f;

    [DataField]
    public string IcyLookAlertKey = "IcyLook";
}
