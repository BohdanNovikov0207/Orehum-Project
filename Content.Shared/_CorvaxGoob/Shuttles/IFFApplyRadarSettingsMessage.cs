// SPDX-FileCopyrightText: 2026 Space Station 14 Contributors
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Shared.Shuttles.Events;

[Serializable] [NetSerializable]
public sealed class IFFApplyRadarSettingsMessage : BoundUserInterfaceMessage
{
    public Color Color;
    public string? Name;
}
