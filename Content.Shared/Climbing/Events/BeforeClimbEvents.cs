// SPDX-FileCopyrightText: 2024 beck-thompson <107373427+beck-thompson@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Climbing.Components;
using Content.Shared.Inventory;

namespace Content.Shared.Climbing.Events;

public abstract class BeforeClimbEvent : CancellableEntityEventArgs
{
    public readonly Entity<ClimbableComponent> BeingClimbedOn;
    public readonly EntityUid GettingPutOnTable;
    public readonly EntityUid PuttingOnTable;

    public BeforeClimbEvent(EntityUid gettingPutOntable,
        EntityUid puttingOnTable,
        Entity<ClimbableComponent> beingClimbedOn)
    {
        GettingPutOnTable = gettingPutOntable;
        PuttingOnTable = puttingOnTable;
        BeingClimbedOn = beingClimbedOn;
    }
}

/// <summary>
/// This event is raised on the the person either getting put on or going on the table.
/// The event is also called on their clothing as well.
/// </summary>
public sealed class SelfBeforeClimbEvent : BeforeClimbEvent, IInventoryRelayEvent
{
    public SelfBeforeClimbEvent(EntityUid gettingPutOntable,
        EntityUid puttingOnTable,
        Entity<ClimbableComponent> beingClimbedOn) : base(gettingPutOntable, puttingOnTable, beingClimbedOn)
    {
    }

    public SlotFlags TargetSlots { get; } = SlotFlags.WITHOUT_POCKET;
}

/// <summary>
/// This event is raised on the thing being climbed on.
/// </summary>
public sealed class TargetBeforeClimbEvent : BeforeClimbEvent
{
    public TargetBeforeClimbEvent(EntityUid gettingPutOntable,
        EntityUid puttingOnTable,
        Entity<ClimbableComponent> beingClimbedOn) : base(gettingPutOntable, puttingOnTable, beingClimbedOn)
    {
    }
}
