using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Disease;

/// <summary>
/// A type of disease spread.
/// </summary>
[Prototype]
public sealed class DiseaseSpreadPrototype : IPrototype
{
    [DataField]
    public bool BlockedByInternals; // TODO: not implemented in the system

    [DataField(required: true)]
    private string Name { get; } = default!;

    [ViewVariables(VVAccess.ReadOnly)]
    public string LocalizedName => Loc.GetString("disease-spread-" + Name.ToLower());

    [IdDataField]
    public string ID { get; } = default!;
}
