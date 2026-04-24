using Content.Goobstation.Common.Grab;
using Robust.Shared.GameStates;

namespace Content.Goobstation.Shared.Grab;

[RegisterComponent] [NetworkedComponent]
public sealed partial class GrabModifierComponent : Component
{
    [DataField]
    public float GrabEscapeModifier;

    [DataField]
    public float GrabEscapeMultiplier = 1f;

    [DataField]
    public float GrabMoveSpeedMultiplier = 1f;

    [DataField]
    public GrabStage StartingGrabStage = GrabStage.Soft;
}
