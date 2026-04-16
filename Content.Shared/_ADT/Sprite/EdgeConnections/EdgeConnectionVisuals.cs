// SPDX-FileCopyrightText: 2026 Space Station 14 Contributors
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Shared._ADT.Sprite.EdgeConnections;

[Serializable, NetSerializable]
public enum EdgeConnectionVisuals : byte
{
    ConnectionMask,
}

[Flags]
[Serializable, NetSerializable]
public enum EdgeConnectionDirections : byte
{
    None = 0,
    North = 1 << 0,
    South = 1 << 1,
    East = 1 << 2,
    West = 1 << 3,
}
