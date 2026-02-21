using Content.Shared.Actions;
using Content.Goobstation.Shared.Overlays;
using Robust.Shared.GameStates;

namespace Content.Goobstation.Shared._Orehum.MoreGoggles;

[RegisterComponent, NetworkedComponent]
public sealed partial class MesonVisionComponent : SwitchableOverlayComponent
{
    public override string? ToggleAction { get; set; } = "ToggleMesonVision";
}

public sealed partial class ToggleMesonVisionEvent : InstantActionEvent;

public sealed class SharedMesonVisionSystem : SwitchableOverlaySystem<MesonVisionComponent, ToggleMesonVisionEvent>;
