// SPDX-FileCopyrightText: 2024 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.EntityTable.EntitySelectors;

namespace Content.Shared.Procedural.DungeonLayers;

/// <summary>
/// Spawns entities inside of the dungeon randomly.
/// </summary>
public sealed partial class EntityTableDunGen : IDunGenLayer
{
    [DataField]
    public int MaxCount = 1;
    // Counts separate to config to avoid some duplication.

    [DataField]
    public int MinCount = 1;

    /// <summary>
    /// Should the count be per dungeon or across all dungeons.
    /// </summary>
    [DataField]
    public bool PerDungeon;

    [DataField(required: true)]
    public EntityTableSelector Table;
}
