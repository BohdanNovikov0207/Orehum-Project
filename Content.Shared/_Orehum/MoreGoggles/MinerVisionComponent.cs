using Content.Shared.Actions;
using Content.Shared.Overlays.Switchable;
using Robust.Shared.GameStates;

namespace Content.Shared._Orehum.MoreGoggles;

[RegisterComponent, NetworkedComponent]
public sealed partial class MinerVisionComponent : SwitchableOverlayComponent
{
    public override string? ToggleAction { get; set; } = "ToggleMinerVision";

    [DataField]
    public override float PulseTime { get; set; } = 2f;
}

public sealed partial class ToggleMinerVisionEvent : InstantActionEvent;

public sealed class SharedMinerVisionSystem : SwitchableOverlaySystem<MinerVisionComponent, ToggleMinerVisionEvent>;
