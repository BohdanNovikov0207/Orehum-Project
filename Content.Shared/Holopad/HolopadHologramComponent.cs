// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 chromiumboy <50505512+chromiumboy@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Numerics;
using Robust.Shared.GameStates;

namespace Content.Shared.Holopad;

/// <summary>
/// Holds data pertaining to holopad holograms
/// </summary>
[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class HolopadHologramComponent : Component
{
    /// <summary>
    /// The shared color alpha
    /// </summary>
    [DataField]
    public float Alpha = 1f;

    /// <summary>
    /// The primary color
    /// </summary>
    [DataField]
    public Color Color1 = Color.White;

    /// <summary>
    /// The secondary color
    /// </summary>
    [DataField]
    public Color Color2 = Color.White;

    /// <summary>
    /// The color brightness
    /// </summary>
    [DataField]
    public float Intensity = 1f;

    /// <summary>
    /// An entity that is linked to this hologram
    /// </summary>
    [ViewVariables] [AutoNetworkedField]
    public EntityUid? LinkedEntity = null;

    /// <summary>
    /// The sprite offset
    /// </summary>
    [DataField]
    public Vector2 Offset = new();

    /// <summary>
    /// Default RSI path
    /// </summary>
    [DataField]
    public string RsiPath = string.Empty;

    /// <summary>
    /// Default RSI state
    /// </summary>
    [DataField]
    public string RsiState = string.Empty;

    /// <summary>
    /// The scroll rate of the hologram shader
    /// </summary>
    [DataField]
    public float ScrollRate = 1f;

    /// <summary>
    /// Name of the shader to use
    /// </summary>
    [DataField]
    public string ShaderName = string.Empty;
}
