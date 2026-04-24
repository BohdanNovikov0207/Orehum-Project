// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <93730715+Aviu00@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 Spatison <137375981+Spatison@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Audio;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Goobstation.Shared.Overlays;

public abstract partial class SwitchableVisionOverlayComponent : BaseVisionOverlayComponent
{
    [DataField]
    public SoundSpecifier? ActivateSound = new SoundPathSpecifier("/Audio/_White/Items/Goggles/activate.ogg");

    [DataField]
    public SoundSpecifier? DeactivateSound = new SoundPathSpecifier("/Audio/_White/Items/Goggles/deactivate.ogg");

    [DataField]
    public bool DrawOverlay = true;

    [DataField]
    public float FlashDurationMultiplier = 1f;

    [DataField]
    public bool IsActive;

    /// <summary>
    /// Whether it should grant equipment enhanced vision or is it mob vision
    /// </summary>
    [DataField]
    public bool IsEquipment;

    [DataField]
    public float OverlayOpacity = 0.5f;

    [ViewVariables(VVAccess.ReadOnly)]
    public float PulseAccumulator;

    /// <summary>
    /// If it is greater than 0, overlay isn't toggled but pulsed instead
    /// </summary>
    [DataField]
    public float PulseTime;

    [ViewVariables]
    public EntityUid? ToggleActionEntity;

    [DataField]
    public virtual EntProtoId? ToggleAction { get; set; }
}

[Serializable] [NetSerializable]
public sealed class SwitchableVisionOverlayComponentState : IComponentState
{
    public SoundSpecifier? ActivateSound;
    public Color Color;
    public SoundSpecifier? DeactivateSound;
    public bool DrawOverlay;
    public float FlashDurationMultiplier;
    public bool IsActive;
    public bool IsEquipment;
    public float LightRadius;
    public float OverlayOpacity;
    public string? ThermalShader;
    public EntProtoId? ToggleAction;
}
