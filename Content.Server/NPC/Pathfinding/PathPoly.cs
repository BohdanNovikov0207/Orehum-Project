// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.NPC;
using Robust.Shared.Map;

namespace Content.Server.NPC.Pathfinding;

public sealed class PathPoly : IEquatable<PathPoly>
{
    [ViewVariables]
    public readonly Box2 Box;

    [ViewVariables]
    public readonly Vector2i ChunkOrigin;

    [ViewVariables]
    public readonly EntityUid GraphUid;

    [ViewVariables]
    public readonly HashSet<PathPoly> Neighbors;

    [ViewVariables]
    public readonly byte TileIndex;

    [ViewVariables]
    public PathfindingData Data;

    public PathPoly(EntityUid graphUid,
        Vector2i chunkOrigin,
        byte tileIndex,
        Box2 vertices,
        PathfindingData data,
        HashSet<PathPoly> neighbors)
    {
        GraphUid = graphUid;
        ChunkOrigin = chunkOrigin;
        TileIndex = tileIndex;
        Box = vertices;
        Data = data;
        Neighbors = neighbors;
    }

    [ViewVariables]
    public EntityCoordinates Coordinates => new(GraphUid, Box.Center);

    public bool Equals(PathPoly? other) =>
        other != null &&
        GraphUid.Equals(other.GraphUid) &&
        ChunkOrigin.Equals(other.ChunkOrigin) &&
        TileIndex == other.TileIndex &&
        Data.Equals(other.Data) &&
        Box.Equals(other.Box);

    public bool IsValid() => (Data.Flags & PathfindingBreadcrumbFlag.Invalid) == 0x0;

    // Explicitly don't check neighbors.

    public bool IsEquivalent(PathPoly other) =>
        GraphUid.Equals(other.GraphUid) &&
        ChunkOrigin.Equals(other.ChunkOrigin) &&
        TileIndex == other.TileIndex &&
        Data.IsEquivalent(other.Data) &&
        Box.Equals(other.Box);

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is PathPoly other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(GraphUid, ChunkOrigin, TileIndex, Box);
}
