using Robust.Shared.GameStates;

namespace Content.Shared.Atmos.Piping.Binary.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState(true)]
public sealed partial class GasVolumePumpComponent : Component
{
    public static readonly float DefaultHigherThreshold = 2 * Atmospherics.MaxOutputPressure;

    [DataField]
    public bool Blocked = false;

    [DataField] [AutoNetworkedField]
    public bool Enabled = true;

    [DataField]
    public float HigherThreshold = DefaultHigherThreshold;

    [DataField("inlet")]
    public string InletName = "inlet";

    [DataField]
    public float LastMolesTransferred;

    [DataField]
    public float LeakRatio = 0.1f;

    [DataField]
    public float LowerThreshold = 0.01f;

    [DataField]
    public float MaxTransferRate = Atmospherics.MaxTransferRate;

    [DataField("outlet")]
    public string OutletName = "outlet";

    [ViewVariables(VVAccess.ReadWrite)]
    public bool Overclocked = false;

    [DataField]
    public float OverclockThreshold = 1000;

    [DataField] [AutoNetworkedField]
    public float TransferRate = Atmospherics.MaxTransferRate;
}
