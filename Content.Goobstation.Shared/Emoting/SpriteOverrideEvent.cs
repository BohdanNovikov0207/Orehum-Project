// SPDX-FileCopyrightText: 2026 Goob Station Contributors
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Goobstation.Shared.Emoting;

[Serializable] [NetSerializable] [ByRefEvent]
public sealed class SpriteOverrideEvent : EntityEventArgs
{
}
