using Content.Goobstation.Shared.InternalResources.Data;
using Content.Shared.Alert;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Changeling.Components;

/// <summary>
/// Marks a changeling that has evolved Void Adaption.
/// </summary>
[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState(fieldDeltas: true)]
public sealed partial class VoidAdaptionComponent : Component
{
    [DataField] [AutoNetworkedField]
    public bool AdaptingLowPressure;

    [DataField] [AutoNetworkedField]
    public bool AdaptingLowTemp;

    [DataField]
    public ProtoId<AlertPrototype> Alert = "VoidAdaption";

    [DataField] [AutoNetworkedField]
    public float ChemModifierValue = 1f;

    [DataField]
    public LocId EnterLowPressurePopup = "changeling-voidadapt-lowpressure-start";

    [DataField]
    public LocId EnterLowTempPopup = "changeling-voidadapt-lowtemperature-start";

    [DataField]
    public LocId FirePopup = "changeling-voidadapt-onfire";

    [DataField] [AutoNetworkedField]
    public bool FirePopupSent;

    [DataField]
    public LocId LeaveLowPressurePopup = "changeling-voidadapt-lowpressure-end";

    [DataField]
    public LocId LeaveLowTempPopup = "changeling-voidadapt-lowtemperature-end";

    [DataField]
    public ProtoId<InternalResourcesPrototype> ResourceType = "ChangelingChemicals";
}
