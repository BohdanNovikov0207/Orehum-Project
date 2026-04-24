// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aidenkrz <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <aviu00@protonmail.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Lincoln McQueen <lincoln.mcqueen@gmail.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 SX-7 <sn1.test.preria.2002@gmail.com>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;

namespace Content.Goobstation.Common.MartialArts;

[Prototype("combo")]
public sealed class ComboPrototype : IPrototype
{
    [DataField("attacks", required: true)]
    public List<ComboAttackType> AttackTypes = new();

    /// <summary>
    /// Can a lying person perform this combo
    /// </summary>
    [DataField]
    public bool CanDoWhileProne = true;

    /// <summary>
    /// Blunt, Slash, etc.
    /// </summary>
    [DataField]
    public string DamageType = "Blunt";

    /// <summary>
    /// Should the target drop items on knockdown?
    /// </summary>
    [DataField]
    public bool DropItems = false;

    /// <summary>
    /// How much extra damage should this move do on perform?
    /// </summary>
    [DataField]
    public float ExtraDamage;

    [DataField(required: true)]
    public MartialArtsForms MartialArtsForm;

    /// <summary>
    /// Name of the move
    /// </summary>
    [DataField(required: true)]
    public string Name = string.Empty;

    /// <summary>
    /// Stun time in seconds
    /// </summary>
    [DataField]
    public int ParalyzeTime;

    /// <summary>
    /// Is this combo performed on self only or only on other targets
    /// </summary>
    [DataField]
    public bool PerformOnSelf;

    //[DataField("weapon")] // Will be done later
    //public string? WeaponProtoId;
    [DataField("event", required: true)]
    public object? ResultEvent;

    /// <summary>
    /// How much stamina damage should this move do on perform.
    /// </summary>
    [DataField]
    public float StaminaDamage;

    /// <summary>
    /// How fast people are thrown on combo
    /// </summary>
    [DataField]
    public float ThrownSpeed = 7f;

    [IdDataField] public string ID { get; } = default!;
}

[Prototype("comboList")]
public sealed class ComboListPrototype : IPrototype
{
    [DataField(required: true)]
    public List<ProtoId<ComboPrototype>> Combos = new();

    [IdDataField] public string ID { get; } = default!;
}
