// SPDX-FileCopyrightText: 2022 Andreas Kämper <andreas@kaemper.tech>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Shared.VendingMachines;

[Serializable] [NetSerializable]
public sealed class VendingMachineEjectMessage : BoundUserInterfaceMessage
{
    public readonly string ID;
    public readonly InventoryType Type;

    public VendingMachineEjectMessage(InventoryType type, string id)
    {
        Type = type;
        ID = id;
    }
}

[Serializable] [NetSerializable]
public enum VendingMachineUiKey
{
    Key,
}
