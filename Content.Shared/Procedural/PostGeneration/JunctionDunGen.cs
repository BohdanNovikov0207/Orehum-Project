using Content.Shared.EntityTable;
using Content.Shared.Maps;
using Robust.Shared.Prototypes;

namespace Content.Shared.Procedural.PostGeneration;

/// <summary>
/// Places the specified entities at junction areas.
/// </summary>
public sealed partial class JunctionDunGen : IDunGenLayer
{
    [DataField(required: true)]
    public ProtoId<EntityTablePrototype> Contents;

    [DataField(required: true)]
    public ProtoId<ContentTileDefinition> Tile;

    /// <summary>
    /// Width to check for junctions.
    /// </summary>
    [DataField]
    public int Width = 3;
}
