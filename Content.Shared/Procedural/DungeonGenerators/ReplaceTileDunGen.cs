// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tayrtahn <tayrtahn@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Maps;
using Robust.Shared.Noise;
using Robust.Shared.Prototypes;

namespace Content.Shared.Procedural.DungeonGenerators;

/// <summary>
/// Replaces existing tiles if they're not empty.
/// </summary>
public sealed partial class ReplaceTileDunGen : IDunGenLayer
{
    [DataField(required: true)]
    public List<ReplaceTileLayer> Layers = new();

    /// <summary>
    /// Chance for a non-variant tile to be used, in case they're too noisy.
    /// </summary>
    [DataField]
    public float VariantWeight = 0.1f;
}

[DataRecord]
public record struct ReplaceTileLayer
{
    public FastNoiseLite Noise;

    public float Threshold;
    public ProtoId<ContentTileDefinition> Tile;
}
