using Robust.Shared.GameStates;

namespace Content.Shared._Lavaland.Mobs.Components;

[RegisterComponent, NetworkedComponent]
public sealed partial class HierophantBeatComponent : Component
{
    [DataField]
    public float MovementSpeedBuff = 1.05f;

    [DataField]
    public string HierophantBeatAlertKey = "HierophantBeat";
}
