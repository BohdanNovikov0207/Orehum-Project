using Content.Shared.Actions;
using Content.Goobstation.Shared.Overlays;
using Robust.Shared.GameStates;

namespace Content.Goobstation.Shared._Orehum.MoreGoggles;

[RegisterComponent, NetworkedComponent]
public sealed partial class MinerVisionComponent : SwitchableOverlayComponent
{
    public override string? ToggleAction { get; set; } = "ToggleMinerVision";

    [DataField]
    public override float PulseTime { get; set; } = 2f;
}

public sealed partial class ToggleMinerVisionEvent : InstantActionEvent;

public sealed class SharedMinerVisionSystem : SwitchableOverlaySystem<MinerVisionComponent, ToggleMinerVisionEvent>;
