// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Kayzel <43700376+KayzelW@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Roudenn <romabond091@gmail.com>
// SPDX-FileCopyrightText: 2025 Spatison <137375981+Spatison@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Trest <144359854+trest100@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 gluesniffler <159397573+gluesniffler@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 gluesniffler <linebarrelerenthusiast@gmail.com>
// SPDX-FileCopyrightText: 2025 kurokoTurbo <92106367+kurokoTurbo@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Maths.FixedPoint;
using Content.Shared._Shitmed.Medical.Surgery.Wounds;
using Content.Shared.Humanoid;

namespace Content.Client._Shitmed.Medical.Surgery.Wounds;

[RegisterComponent] [AutoGenerateComponentState(true)]
public sealed partial class WoundableVisualsComponent : Component
{
    [DataField]
    public string? BleedingOverlay;

    [DataField]
    public Dictionary<BleedingSeverity, FixedPoint2> BleedingThresholds = new()
    {
        { BleedingSeverity.Minor, 2.6 },
        { BleedingSeverity.Severe, 7 },
    };

    [DataField]
    public Dictionary<string, WoundVisualizerSprite>? DamageOverlayGroups = new();

    [DataField(required: true)]
    public HumanoidVisualLayers OccupiedLayer;

    [DataField(required: true)]
    public List<FixedPoint2> Thresholds = [];
}

// :fort:
[DataDefinition]
public sealed partial class WoundVisualizerSprite
{
    [DataField]
    public string? Color;

    [DataField(required: true)]
    public string Sprite = default!;
}
