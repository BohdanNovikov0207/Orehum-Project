// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Prototypes;

namespace Content.Shared.Procedural;

[Prototype]
public sealed class DungeonRoomPackPrototype : IPrototype
{
    [DataField("rooms", required: true)] public List<Box2i> Rooms = new();

    /// <summary>
    /// Used to associate the room pack with other room packs with the same dimensions.
    /// </summary>
    [DataField("size", required: true)] public Vector2i Size;

    [IdDataField]
    public string ID { get; } = string.Empty;
}
