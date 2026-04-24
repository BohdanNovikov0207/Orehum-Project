// SPDX-FileCopyrightText: 2023 Kevin Zheng <kevinz5000@gmail.com>
// SPDX-FileCopyrightText: 2023 Morb <14136326+Morb0@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 AJCM <AJCM@tutanota.com>
// SPDX-FileCopyrightText: 2024 Rainfall <rainfey0+git@gmail.com>
// SPDX-FileCopyrightText: 2024 Rainfey <rainfey0+github@gmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Serialization;

namespace Content.Shared.NukeOps;

[Serializable] [NetSerializable]
public enum WarDeclaratorUiKey
{
    Key,
}

public enum WarConditionStatus : byte
{
    WarReady,
    YesWar,
    NoWarUnknown,
    NoWarTimeout,
    NoWarSmallCrew,
    NoWarShuttleDeparted,
}

[Serializable] [NetSerializable]
public sealed class WarDeclaratorBoundUserInterfaceState : BoundUserInterfaceState
{
    public TimeSpan EndTime;
    public TimeSpan ShuttleDisabledTime;
    public WarConditionStatus? Status;

    public WarDeclaratorBoundUserInterfaceState(WarConditionStatus? status,
        TimeSpan endTime,
        TimeSpan shuttleDisabledTime)
    {
        Status = status;
        EndTime = endTime;
        ShuttleDisabledTime = shuttleDisabledTime;
    }
}

[Serializable] [NetSerializable]
public sealed class WarDeclaratorActivateMessage : BoundUserInterfaceMessage
{
    public WarDeclaratorActivateMessage(string msg)
    {
        Message = msg;
    }

    public string Message { get; }
}
