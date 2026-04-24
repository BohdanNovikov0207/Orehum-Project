using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Goobstation.Shared.Disease;

[DataDefinition] [Serializable] [NetSerializable]
public sealed partial class DiseaseSpreadModifier
{
    /// <summary>
    /// By how much to multiply spread attempts' chance.
    /// </summary>
    [DataField]
    public Dictionary<ProtoId<DiseaseSpreadPrototype>, float> ChanceMultipliers = new();

    /// <summary>
    /// How much to modify spread attempts' power.
    /// </summary>
    [DataField]
    public Dictionary<ProtoId<DiseaseSpreadPrototype>, float> PowerModifiers = new();

    public float PowerMod(ProtoId<DiseaseSpreadPrototype> proto) => PowerModifiers.GetValueOrDefault(proto, 0f);

    public float ChanceMult(ProtoId<DiseaseSpreadPrototype> proto) => ChanceMultipliers.GetValueOrDefault(proto, 1f);
}
