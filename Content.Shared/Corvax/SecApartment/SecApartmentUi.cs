// SPDX-FileCopyrightText: 2026 Space Station 14 Contributors
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Medical.SuitSensor;
using Robust.Shared.Serialization;

namespace Content.Shared.SecApartment;

[Serializable] [NetSerializable]
public enum SecApartmentUiKey : byte
{
    Key,
}

[Serializable] [NetSerializable]
public sealed class SecApartmentUpdateState : BoundUserInterfaceState
{
    public SecApartmentUpdateState(
        string stationName,
        List<CrewMemberInfo> securityCrew,
        List<CrewMemberInfo> unassignedSecurity,
        List<Squad> squads)
    {
        StationName = stationName;
        SecurityCrew = securityCrew;
        UnassignedSecurity = unassignedSecurity;
        Squads = squads;
    }

    public string StationName { get; }
    public List<CrewMemberInfo> SecurityCrew { get; }
    public List<CrewMemberInfo> UnassignedSecurity { get; }
    public List<Squad> Squads { get; }
}

[Serializable] [NetSerializable]
public sealed class SensorStatusUpdateState : BoundUserInterfaceState
{
    public SensorStatusUpdateState(
        Dictionary<string, SuitSensorStatus?> memberStatuses,
        Dictionary<string, (string Location, bool HasLocation)> squadLocations)
    {
        MemberStatuses = memberStatuses;
        SquadLocations = squadLocations;
    }

    public Dictionary<string, SuitSensorStatus?> MemberStatuses { get; }
    public Dictionary<string, (string Location, bool HasLocation)> SquadLocations { get; }
}

[Serializable] [NetSerializable]
public sealed class CreateSquadMessage : BoundUserInterfaceMessage
{
    public CreateSquadMessage(string squadName)
    {
        SquadName = squadName;
    }

    public string SquadName { get; }
}

[Serializable] [NetSerializable]
public sealed class DeleteSquadMessage : BoundUserInterfaceMessage
{
    public DeleteSquadMessage(string squadId)
    {
        SquadId = squadId;
    }

    public string SquadId { get; }
}

[Serializable] [NetSerializable]
public sealed class RenameSquadMessage : BoundUserInterfaceMessage
{
    public RenameSquadMessage(string squadId, string newName)
    {
        SquadId = squadId;
        NewName = newName;
    }

    public string SquadId { get; }
    public string NewName { get; }
}

[Serializable] [NetSerializable]
public sealed class UpdateSquadDescriptionMessage : BoundUserInterfaceMessage
{
    public UpdateSquadDescriptionMessage(string squadId, string description)
    {
        SquadId = squadId;
        Description = description;
    }

    public string SquadId { get; }
    public string Description { get; }
}

[Serializable] [NetSerializable]
public sealed class AddMemberToSquadMessage : BoundUserInterfaceMessage
{
    public AddMemberToSquadMessage(string squadId, string memberId)
    {
        SquadId = squadId;
        MemberId = memberId;
    }

    public string SquadId { get; }
    public string MemberId { get; }
}

[Serializable] [NetSerializable]
public sealed class RemoveMemberFromSquadMessage : BoundUserInterfaceMessage
{
    public RemoveMemberFromSquadMessage(string squadId, string memberId)
    {
        SquadId = squadId;
        MemberId = memberId;
    }

    public string SquadId { get; }
    public string MemberId { get; }
}

[Serializable] [NetSerializable]
public sealed class ChangeSquadIconMessage : BoundUserInterfaceMessage
{
    public ChangeSquadIconMessage(string squadId, SquadIconNum iconId)
    {
        SquadId = squadId;
        IconId = iconId;
    }

    public string SquadId { get; }
    public SquadIconNum IconId { get; }
}

[Serializable] [NetSerializable]
public sealed class ChangeSquadStatusMessage : BoundUserInterfaceMessage
{
    public ChangeSquadStatusMessage(string squadId, SquadStatus status)
    {
        SquadId = squadId;
        Status = status;
    }

    public string SquadId { get; }
    public SquadStatus Status { get; }
}

[Serializable] [NetSerializable]
public sealed class TimerUpdateState : BoundUserInterfaceState
{
    public TimerUpdateState(List<TimerEntry> timers)
    {
        Timers = timers;
    }

    public List<TimerEntry> Timers { get; }
}

[Serializable] [NetSerializable]
public sealed class RemoveTimerMessage : BoundUserInterfaceMessage
{
    public RemoveTimerMessage(NetEntity timerUid)
    {
        TimerUid = timerUid;
    }

    public NetEntity TimerUid { get; }
}
