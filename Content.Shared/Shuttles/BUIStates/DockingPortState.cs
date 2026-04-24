// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Map;
using Robust.Shared.Serialization;

namespace Content.Shared.Shuttles.BUIStates;

/// <summary>
/// State of each individual docking port for interface purposes
/// </summary>
[Serializable] [NetSerializable]
public sealed class DockingPortState
{
    public Angle Angle;

    public NetCoordinates Coordinates;
    public NetEntity Entity;

    public NetEntity? GridDockedWith;
    public string Name = string.Empty;
    public bool Connected => GridDockedWith != null;
}
