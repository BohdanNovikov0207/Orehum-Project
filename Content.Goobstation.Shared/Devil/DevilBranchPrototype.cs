using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Devil;

[Prototype("devilBranchPrototype")]
public sealed class DevilBranchPrototype : IPrototype
{
    [DataField("powerActions", required: true)]
    public Dictionary<DevilPowerLevel, List<EntProtoId>> PowerActions = new();

    [IdDataField]
    public string ID { get; set; } = default!;
}

public enum DevilPowerLevel : byte
{
    None,
    Weak,
    Moderate,
    Powerful,
}
