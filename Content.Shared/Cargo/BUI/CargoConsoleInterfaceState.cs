// SPDX-FileCopyrightText: 2022 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Cargo.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Cargo.BUI;

[NetSerializable] [Serializable]
public sealed class CargoConsoleInterfaceState : BoundUserInterfaceState
{
    public int Capacity;
    public int Count;
    public string Name;
    public List<CargoOrderData> Orders;
    public List<ProtoId<CargoProductPrototype>> Products;
    public NetEntity Station;

    public CargoConsoleInterfaceState(string name,
        int count,
        int capacity,
        NetEntity station,
        List<CargoOrderData> orders,
        List<ProtoId<CargoProductPrototype>> products)
    {
        Name = name;
        Count = count;
        Capacity = capacity;
        Station = station;
        Orders = orders;
        Products = products;
    }
}
