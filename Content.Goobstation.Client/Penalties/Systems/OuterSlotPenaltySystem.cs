// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2025 SX-7 <92227810+SX-7@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 coderabbitai[bot] <136622811+coderabbitai[bot]@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Shared.Penalties.Components;
using Content.Shared.Clothing;
using Content.Shared.Inventory;
using Content.Shared.Movement.Systems;

namespace Content.Goobstation.Client.Penalties.Systems;

public sealed class OuterSlotPenaltySystem : EntitySystem
{
    [Dependency] private readonly MovementSpeedModifierSystem _movementSpeedModifierSystem = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<OuterSlotPenaltyComponent, ClothingDidEquippedEvent>(OnEquip);
        SubscribeLocalEvent<OuterSlotPenaltyComponent, ClothingDidUnequippedEvent>(OnUnequip);
    }

    private void OnEquip(EntityUid uid, OuterSlotPenaltyComponent comp, ref ClothingDidEquippedEvent args)
    {
        if (args.Clothing.Comp.Slots == SlotFlags.OUTERCLOTHING)
            comp.OuterLayerEquipped = true;
        _movementSpeedModifierSystem.RefreshMovementSpeedModifiers(uid);
    }

    private void OnUnequip(EntityUid uid, OuterSlotPenaltyComponent comp, ref ClothingDidUnequippedEvent args)
    {
        if (args.Clothing.Comp.Slots == SlotFlags.OUTERCLOTHING)
            comp.OuterLayerEquipped = false;
        _movementSpeedModifierSystem.RefreshMovementSpeedModifiers(uid);
    }
}
