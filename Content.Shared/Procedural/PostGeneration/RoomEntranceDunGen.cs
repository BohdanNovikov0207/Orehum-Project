using Content.Shared.EntityTable;
using Content.Shared.Maps;
using Robust.Shared.Prototypes;

namespace Content.Shared.Procedural.PostGeneration;

/// <summary>
/// Places tiles / entities onto room entrances.
/// </summary>
public sealed partial class RoomEntranceDunGen : IDunGenLayer
{
    [DataField]
    public ProtoId<EntityTablePrototype> Contents;

    [DataField(required: true)]
    public ProtoId<ContentTileDefinition> Tile;
}
