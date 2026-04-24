// SPDX-FileCopyrightText: 2024 BombasterDS <115770678+BombasterDS@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Eui;
using Robust.Shared.Serialization;

namespace Content.Goobstation.Shared.Administration;

[Serializable] [NetSerializable]
public sealed class TimeTransferPanelEuiState : EuiStateBase
{
    public TimeTransferPanelEuiState(bool hasFlag)
    {
        HasFlag = hasFlag;
    }

    public bool HasFlag { get; }
}

[Serializable] [NetSerializable]
public sealed class TimeTransferEuiMessage : EuiMessageBase
{
    public TimeTransferEuiMessage(string playerId, List<TimeTransferData> timeData, bool overwrite)
    {
        PlayerId = playerId;
        TimeData = timeData;
        Overwrite = overwrite;
    }

    public string PlayerId { get; }
    public List<TimeTransferData> TimeData { get; }

    public bool Overwrite { get; }
}

[Serializable] [NetSerializable]
public sealed class TimeTransferWarningEuiMessage : EuiMessageBase
{
    public TimeTransferWarningEuiMessage(string message, Color color)
    {
        Message = message;
        WarningColor = color;
    }

    public string Message { get; }
    public Color WarningColor { get; }
}

[DataDefinition]
[Serializable] [NetSerializable]
public partial record struct TimeTransferData
{
    public TimeTransferData(string tracker, string timeString)
    {
        PlaytimeTracker = tracker;
        TimeString = timeString;
    }

    [DataField]
    public string TimeString { get; init; }

    [DataField]
    public string PlaytimeTracker { get; init; }
}
