using Content.Goobstation.Common.Grab;
using Content.Shared.Inventory;

namespace Content.Goobstation.Shared.Grab;

[ByRefEvent]
public record struct GrabModifierEvent(EntityUid User, GrabStage Stage) : IInventoryRelayEvent
{
    public float Modifier = 0f;

    public float Multiplier = 1f;

    public GrabStage? NewStage = null;

    public float SpeedMultiplier = 1f;
    public SlotFlags TargetSlots => SlotFlags.GLOVES;
}
