// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 DoutorWhite <thedoctorwhite@gmail.com>
// SPDX-FileCopyrightText: 2025 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameStates;
using Robust.Shared.Map.Components;

namespace Content.Shared.Light.Components;

/// <summary>
/// Cycles through colors AKA "Day / Night cycle" on <see cref="MapLightComponent" />
/// </summary>
[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class LightCycleComponent : Component
{
    [DataField] [AutoNetworkedField]
    public Color ClipLevel = new(1f, 1f, 1.25f);

    [DataField] [AutoNetworkedField]
    public float ClipLight = 1.25f;

    /// <summary>
    /// How long an entire cycle lasts
    /// </summary>
    [DataField] [AutoNetworkedField]
    public TimeSpan Duration = TimeSpan.FromMinutes(30);

    [DataField] [AutoNetworkedField]
    public bool Enabled = true;

    /// <summary>
    /// Should the offset be randomised upon MapInit.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public bool InitialOffset = true;

    [DataField] [AutoNetworkedField]
    public Color MaxLevel = new(2f, 2f, 5f);

    /// <summary>
    /// Peak of the oscillation
    /// </summary>
    [DataField] [AutoNetworkedField]
    public float MaxLightLevel = 3f;

    [DataField] [AutoNetworkedField]
    public Color MinLevel = new(0.1f, 0.15f, 0.50f);

    /// <summary>
    /// Trench of the oscillation.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public float MinLightLevel = 0f;

    [DataField] [AutoNetworkedField]
    public TimeSpan Offset;

    [DataField] [AutoNetworkedField]
    public Color OriginalColor = Color.Transparent;
}
