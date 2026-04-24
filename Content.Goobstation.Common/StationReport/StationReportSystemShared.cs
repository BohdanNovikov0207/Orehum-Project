using Robust.Shared.Serialization;

namespace Content.Goobstation.Common.StationReport;

[Serializable] [NetSerializable]
public sealed class StationReportEvent : EntityEventArgs
{
    public StationReportEvent(string? text)
    {
        StationReportText = text;
    }

    //This is where the stationreport is stored so the client can access it
    public string? StationReportText { get; }
}
