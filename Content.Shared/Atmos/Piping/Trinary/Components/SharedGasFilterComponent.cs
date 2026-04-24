// SPDX-FileCopyrightText: 2021 Vera Aguilera Puerto <6766154+Zumorica@users.noreply.github.com>
// SPDX-FileCopyrightText: 2021 Vera Aguilera Puerto <gradientvera@outlook.com>
// SPDX-FileCopyrightText: 2021 ike709 <ike709@github.com>
// SPDX-FileCopyrightText: 2021 ike709 <ike709@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 Vordenburg <114301317+Vordenburg@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Serialization;

namespace Content.Shared.Atmos.Piping.Trinary.Components;

[Serializable] [NetSerializable]
public enum GasFilterUiKey
{
    Key,
}

[Serializable] [NetSerializable]
public sealed class GasFilterBoundUserInterfaceState : BoundUserInterfaceState
{
    public GasFilterBoundUserInterfaceState(string filterLabel, float transferRate, bool enabled, Gas? filteredGas)
    {
        FilterLabel = filterLabel;
        TransferRate = transferRate;
        Enabled = enabled;
        FilteredGas = filteredGas;
    }

    public string FilterLabel { get; }
    public float TransferRate { get; }
    public bool Enabled { get; }
    public Gas? FilteredGas { get; }
}

[Serializable] [NetSerializable]
public sealed class GasFilterToggleStatusMessage : BoundUserInterfaceMessage
{
    public GasFilterToggleStatusMessage(bool enabled)
    {
        Enabled = enabled;
    }

    public bool Enabled { get; }
}

[Serializable] [NetSerializable]
public sealed class GasFilterChangeRateMessage : BoundUserInterfaceMessage
{
    public GasFilterChangeRateMessage(float rate)
    {
        Rate = rate;
    }

    public float Rate { get; }
}

[Serializable] [NetSerializable]
public sealed class GasFilterSelectGasMessage : BoundUserInterfaceMessage
{
    public GasFilterSelectGasMessage(int? id)
    {
        ID = id;
    }

    public int? ID { get; }
}
