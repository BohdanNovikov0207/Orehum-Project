using Content.Shared.Containers.ItemSlots;
using Content.Shared.Guidebook;
using Robust.Shared.GameStates;

namespace Content.Shared.Atmos.Piping.Unary.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState(true)]
public sealed partial class GasCanisterComponent : Component, IGasMixtureHolder
{
    [DataField]
    public ItemSlot GasTankSlot = new();

    /// <summary>
    /// Last recorded pressure, for appearance-updating purposes.
    /// </summary>
    public float LastPressure = 0f;

    /// <summary>
    /// Maximum release pressure possible for the release valve.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public float MaxReleasePressure = Atmospherics.OneAtmosphere * 10;

    /// <summary>
    /// Minimum release pressure possible for the release valve.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public float MinReleasePressure = Atmospherics.OneAtmosphere / 10;

    /// <summary>
    /// Valve release pressure.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public float ReleasePressure = Atmospherics.OneAtmosphere;

    /// <summary>
    /// Whether the release valve is open on the canister.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public bool ReleaseValve = false;

    [DataField("port")]
    public string PortName { get; set; } = "port";

    /// <summary>
    /// Container name for the gas tank holder.
    /// </summary>
    [DataField("container")]
    public string ContainerName { get; set; } = "tank_slot";

    [GuidebookData]
    public float Volume => Air.Volume;

    [DataField("gasMixture")]
    public GasMixture Air { get; set; } = new();
}
