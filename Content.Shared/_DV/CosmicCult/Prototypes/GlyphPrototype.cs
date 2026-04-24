using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._DV.CosmicCult.Prototypes;

[Prototype]
public sealed class GlyphPrototype : IPrototype
{
    [DataField(required: true)]
    public EntProtoId Entity;

    [DataField(required: true)]
    public SpriteSpecifier Icon = SpriteSpecifier.Invalid;

    [DataField(required: true)]
    public LocId Name;

    [DataField(required: true)]
    public int Tier;

    [DataField]
    public LocId Tooltip;

    [IdDataField]
    public string ID { get; } = default!;
}
