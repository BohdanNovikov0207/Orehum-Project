using Robust.Shared.GameStates;

namespace Content.Goobstation.Shared.GPS.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState(true, true)]
public sealed partial class GPSComponent : Component
{
    [DataField] [AutoNetworkedField]
    public bool Enabled;

    [DataField] [AutoNetworkedField]
    public List<GpsEntry> GpsEntries = new();

    [DataField] [AutoNetworkedField]
    public string GpsName = "";

    [DataField] [AutoNetworkedField]
    public bool InDistress;

    [DataField] [AutoNetworkedField]
    public NetEntity? TrackedEntity;
}
