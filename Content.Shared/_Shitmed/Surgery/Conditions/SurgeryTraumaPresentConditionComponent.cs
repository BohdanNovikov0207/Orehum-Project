using Content.Shared._Shitmed.Medical.Surgery.Traumas;
using Robust.Shared.GameStates;

namespace Content.Shared._Shitmed.Medical.Surgery.Conditions;

[RegisterComponent] [NetworkedComponent]
public sealed partial class SurgeryTraumaPresentConditionComponent : Component
{
    [DataField]
    public bool Inverted = false;

    [DataField("trauma")]
    public TraumaType TraumaType = TraumaType.BoneDamage;
}
