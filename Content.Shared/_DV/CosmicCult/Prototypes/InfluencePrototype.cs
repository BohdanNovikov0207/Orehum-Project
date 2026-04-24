using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._DV.CosmicCult.Prototypes;

/// <summary>
/// An influence that can be purchased from the monument
/// </summary>
[Prototype]
public sealed class InfluencePrototype : IPrototype
{
    [DataField]
    public EntProtoId? Action;

    [DataField]
    public ComponentRegistry? Add;

    [DataField(required: true)]
    public int Cost;

    [DataField(required: true)]
    public LocId Description;

    [DataField(required: true)]
    public SpriteSpecifier Icon = SpriteSpecifier.Invalid;

    [DataField(required: true)]
    public LocId InfluenceType;

    [DataField(required: true)]
    public LocId Name;

    [DataField]
    public ComponentRegistry? Remove;

    [DataField(required: true)]
    public int Tier;

    [IdDataField]
    public string ID { get; } = default!;
}
