// SPDX-FileCopyrightText: 2024 SlamBamActionman <83650252+SlamBamActionman@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Inventory;

namespace Content.Shared.Slippery;

[ByRefEvent]
public record struct GetSlowedOverSlipperyModifierEvent() : IInventoryRelayEvent
{
    public float SlowdownModifier = 1f;
    SlotFlags IInventoryRelayEvent.TargetSlots => ~SlotFlags.POCKET;
}
