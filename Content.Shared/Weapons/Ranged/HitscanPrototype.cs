// SPDX-FileCopyrightText: 2022 ElectroJr <leonsfriedrich@gmail.com>
// SPDX-FileCopyrightText: 2022 Kara <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2022 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 T-Stalker <43253663+DogZeroX@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 T-Stalker <le0nel_1van@hotmail.com>
// SPDX-FileCopyrightText: 2022 metalgearsloth <metalgearsloth@gmail.com>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Visne <39844191+Visne@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <aviu00@protonmail.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Tayrtahn <tayrtahn@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Damage;
using Content.Shared.Physics;
using Content.Shared.Weapons.Reflect;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared.Weapons.Ranged;

[Prototype]
public sealed class HitscanPrototype : IPrototype, IShootable
{
    [DataField("collisionMask")]
    public int CollisionMask = (int) CollisionGroup.Opaque;

    [ViewVariables(VVAccess.ReadWrite)] [DataField("damage")]
    public DamageSpecifier? Damage;

    // Goobstation
    [DataField]
    public float FireStacks;

    /// <summary>
    /// Force the hitscan sound to play rather than potentially playing the entity's sound.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)] [DataField("forceSound")]
    public bool ForceSound;

    [ViewVariables(VVAccess.ReadOnly)] [DataField("impactFlash")]
    public SpriteSpecifier? ImpactFlash;

    /// <summary>
    /// Try not to set this too high.
    /// </summary>
    [DataField("maxLength")]
    public float MaxLength = 20f;

    [ViewVariables(VVAccess.ReadOnly)] [DataField("muzzleFlash")]
    public SpriteSpecifier? MuzzleFlash;

    /// <summary>
    /// What we count as for reflection.
    /// </summary>
    [DataField("reflective")] public ReflectType Reflective = ReflectType.Energy;

    /// <summary>
    /// Sound that plays upon the thing being hit.
    /// </summary>
    [DataField("sound")]
    public SoundSpecifier? Sound;

    [ViewVariables(VVAccess.ReadWrite)] [DataField("staminaDamage")]
    public float StaminaDamage;

    [ViewVariables(VVAccess.ReadOnly)] [DataField("travelFlash")]
    public SpriteSpecifier? TravelFlash;

    [ViewVariables]
    [IdDataField]
    public string ID { get; } = default!;
}
