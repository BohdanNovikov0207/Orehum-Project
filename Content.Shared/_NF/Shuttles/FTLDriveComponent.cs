using Content.Shared.Shuttles.Systems;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Shared._NF.Shuttles;

/// <summary>
/// Assigned to shuttles that are able to FTL.
/// </summary>
[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class FTLDriveComponent : Component
{
    [DataField] [AutoNetworkedField]
    public FTLDriveData Data = new(SharedShuttleSystem.FTLRange, false);
}

/// <summary>
/// Contains data for the FTL drive.
/// </summary>
[DataDefinition]
[Serializable] [NetSerializable]
public partial record struct FTLDriveData
{
    [DataField]
    public float? ArrivalTime;

    [DataField]
    public float? CooldownTime;

    [DataField("ftlToSameMap")]
    public bool FTLToSameMap;

    [DataField]
    public float? KnockdownTime;

    [DataField]
    public float Range;

    [DataField]
    public float? StartupTime;

    [DataField]
    public float? TravelTime;

    public FTLDriveData(float range, bool ftlToSameMap)
    {
        Range = range;
        FTLToSameMap = ftlToSameMap;
    }
}
