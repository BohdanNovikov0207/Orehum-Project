using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.Actions;
using Content.Shared.DoAfter;
using Robust.Shared.Audio;
using Robust.Shared.Map;
using Robust.Shared.Physics.Dynamics;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

// Goobstation


namespace Content.Shared._White.Actions.Events;

/// <summary>
/// Event for placing a tile and/or spawning an entity in under the object that triggers it with a delay.
/// </summary>
public sealed partial class SpawnTileEntityActionEvent : InstantActionEvent
{
    /// <summary>
    /// The sound that will be played when the action is performed
    /// </summary>
    [DataField]
    public SoundSpecifier? Audio;

    [DataField(customTypeSerializer: typeof(FlagSerializer<CollisionLayer>))]
    public int BlockedCollisionLayer;

    [DataField(customTypeSerializer: typeof(FlagSerializer<CollisionMask>))]
    public int BlockedCollisionMask;

    /// <summary>
    /// The prototype of the entity to be created
    /// </summary>
    [DataField]
    public EntProtoId? Entity;

    /// <summary>
    /// The identifier of the tile to be placed
    /// </summary>
    [DataField]
    public string? TileId;
}

/// <summary>
/// Event for placing a tile and/or spawning an entity at a specified position on the map with a delay.
/// </summary>
public sealed partial class PlaceTileEntityEvent : WorldTargetActionEvent
{
    /// <summary>
    /// The sound that will be played when the action is performed
    /// </summary>
    [DataField]
    public SoundSpecifier? Audio;

    [DataField(customTypeSerializer: typeof(FlagSerializer<CollisionLayer>))]
    public int BlockedCollisionLayer;

    [DataField(customTypeSerializer: typeof(FlagSerializer<CollisionMask>))]
    public int BlockedCollisionMask;

    /// <summary>
    /// The prototype of the entity to be created
    /// </summary>
    [DataField]
    public EntProtoId? Entity;

    /// <summary>
    /// The duration of the action in seconds
    /// </summary>
    [DataField]
    public float Length;

    /// <summary>
    /// The identifier of the tile to be placed
    /// </summary>
    [DataField]
    public string? TileId;
}

[Serializable] [NetSerializable]
public sealed partial class PlaceTileEntityDoAfterEvent : DoAfterEvent
{
    public NetEntity Action; // Goobstation
    public SoundSpecifier? Audio;
    public int BlockedCollisionLayer;
    public int BlockedCollisionMask;
    public EntProtoId? Entity;
    public FixedPoint2 PlasmaCost; // Goobstation
    public NetCoordinates Target;
    public string? TileId;

    public override DoAfterEvent Clone() => this;
}
