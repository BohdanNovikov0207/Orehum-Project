// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Ygg01 <y.laughing.man.y@gmail.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 SX_7 <sn1.test.preria.2002@gmail.com>
// SPDX-FileCopyrightText: 2025 Tayrtahn <tayrtahn@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Maps;
using Content.Shared.Tag;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared.Procedural;

[Prototype]
public sealed class DungeonRoomPrototype : IPrototype
{
    /// <summary>
    /// Path to the file to use for the room.
    /// </summary>
    [DataField("atlas", required: true)]
    public ResPath AtlasPath;

    /// <summary>
    /// These tiles will be ignored when copying from the atlas into the actual game,
    /// allowing you to make rooms of irregular shapes that blend seamlessly into their surroundings
    /// </summary>
    [DataField]
    public ProtoId<ContentTileDefinition>? IgnoreTile;

    /// <summary>
    /// Tile offset into the atlas to use for the room.
    /// </summary>
    [DataField(required: true)]
    public Vector2i Offset;

    [DataField(required: true)]
    public Vector2i Size;

    [ViewVariables(VVAccess.ReadWrite)] [DataField]
    public List<ProtoId<TagPrototype>> Tags = new();

    [IdDataField] public string ID { get; } = string.Empty;
}
