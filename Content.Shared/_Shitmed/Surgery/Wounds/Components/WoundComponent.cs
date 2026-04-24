// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 deltanedas <@deltanedas:kde.org>
// SPDX-FileCopyrightText: 2025 gluesniffler <159397573+gluesniffler@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 gluesniffler <linebarrelerenthusiast@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.Damage.Prototypes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._Shitmed.Medical.Surgery.Wounds.Components;

[RegisterComponent] [NetworkedComponent]
[EntityCategory("Wounds")]
public sealed partial class WoundComponent : Component
{
    /// <summary>
    /// "Can be healed".
    /// </summary>
    [DataField]
    public bool CanBeHealed = true;

    /// <summary>
    /// Damage group of this wound.
    /// </summary>
    [DataField]
    [ViewVariables(VVAccess.ReadOnly)]
    public ProtoId<DamageGroupPrototype>? DamageGroup;

    /// <summary>
    /// Damage type of this wound.
    /// </summary>
    [DataField(required: true)]
    public ProtoId<DamageTypePrototype> DamageType;

    /// <summary>
    /// 'Parent' of wound. Basically the entity to which the wound was applied.
    /// </summary>
    [ViewVariables(VVAccess.ReadOnly)]
    public EntityUid HoldingWoundable;

    /// <summary>
    /// Well, name speaks for this.
    /// </summary>
    [DataField]
    public bool IsScar;

    /// <summary>
    /// Whether the wound can mangle its woundable, and at which severity.
    /// </summary>
    [DataField]
    public WoundSeverity? MangleSeverity;

    /// <summary>
    /// Scar wound prototype, what will be spawned upon healing this wound.
    /// If null - no scar wound will be spawned.
    /// </summary>
    [DataField]
    public EntProtoId? ScarWound;

    /// <summary>
    /// Multiplier for self-healing.
    /// </summary>
    [DataField]
    public float SelfHealMultiplier = 1.0f;

    /// <summary>
    /// String of text used for displaying things about the wound in popups and self inspects.
    /// </summary>
    [DataField]
    public string TextString = "wound";

    /// <summary>
    /// How much damage this wound does to it's parent woundable?
    /// </summary>
    [DataField("integrityMultiplier")]
    public FixedPoint2 WoundableIntegrityMultiplier = 1;

    /// <summary>
    /// Wound severity. Has six severities: Healed/Minor/Moderate/Severe/Critical and Loss.
    /// Directly depends on <see cref="WoundSeverityPoint" />
    /// </summary>
    [DataField]
    public WoundSeverity WoundSeverity;

    /// <summary>
    /// Actually, severity of the wound. The more the worse.
    /// Directly depends on <see cref="WoundSeverity" />
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)]
    public FixedPoint2 WoundSeverityPoint;

    /// <summary>
    /// maybe some cool mechanical stuff to treat those wounds later. I genuinely have no idea
    /// Wound type. External/Internal basically.
    /// </summary>
    [DataField]
    public WoundType WoundType = WoundType.External;

    /// <summary>
    /// When wound is visible. Always/HandScanner/AdvancedScanner.
    /// </summary>
    [DataField]
    public WoundVisibility WoundVisibility = WoundVisibility.Always;

    /// <summary>
    /// The damage this wound applies to it's woundable
    /// </summary>
    public FixedPoint2 WoundIntegrityDamage => WoundSeverityPoint; //* WoundableIntegrityMultiplier;
}

[Serializable] [NetSerializable]
public sealed class WoundComponentState : ComponentState
{
    public bool CanBeHealed;

    public ProtoId<DamageGroupPrototype>? DamageGroup;
    public string? DamageType;
    public NetEntity HoldingWoundable;

    public bool IsScar;

    public EntProtoId? ScarWound;

    public float SelfHealMultiplier;
    public FixedPoint2 WoundableIntegrityMultiplier;

    public WoundSeverity WoundSeverity;

    public FixedPoint2 WoundSeverityPoint;

    public WoundType WoundType;

    public WoundVisibility WoundVisibility;
}
