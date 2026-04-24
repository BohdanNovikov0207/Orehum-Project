using Robust.Shared.Prototypes;

namespace Content.Shared._White.Xenomorphs.Caste;

[Prototype("xenomorphCaste")]
public sealed class XenomorphCastePrototype : IPrototype
{
    [DataField]
    public int MaxCount;

    [DataField]
    public string Name = string.Empty;

    [DataField]
    public ProtoId<XenomorphCastePrototype>? NeedCasteDeath;

    [IdDataField]
    public string ID { get; } = default!;
}
