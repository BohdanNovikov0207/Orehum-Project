// SPDX-FileCopyrightText: 2022 Andreas Kämper <andreas.kaemper@5minds.de>
// SPDX-FileCopyrightText: 2022 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Kara <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2024 keronshb <54602815+keronshb@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <93730715+Aviu00@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Damage;
using Robust.Shared.Audio;

namespace Content.Server.ImmovableRod;

[RegisterComponent]
public sealed partial class ImmovableRodComponent : Component
{
    /// <summary>
    /// Damage done, if not gibbing
    /// </summary>
    [DataField]
    public DamageSpecifier? Damage;

    /// <summary>
    /// Goobstation
    /// List of all mobs that the rod has damaged
    /// </summary>
    [DataField]
    public List<EntityUid> DamagedEntities = new();

    /// <summary>
    /// With this set to true, rods will automatically set the tiles under them to space.
    /// </summary>
    [DataField("destroyTiles")]
    public bool DestroyTiles = true;

    /// <summary>
    /// Overrides the random direction for an immovable rod.
    /// </summary>
    [DataField("directionOverride")]
    public Angle DirectionOverride = Angle.Zero;

    [DataField("hitSoundProbability")]
    public float HitSoundProbability = 0.1f;

    /// <summary>
    /// Goobstation
    /// Whether the rod should ignore resistances, if not gibbing
    /// </summary>
    [DataField]
    public bool IgnoreResistances;

    /// <summary>
    /// Goobstation
    /// If it is above 0, knock down targets when rod hits them
    /// </summary>
    [DataField]
    public TimeSpan KnockdownTime = TimeSpan.Zero;

    [DataField("maxSpeed")]
    public float MaxSpeed = 35f;

    [DataField("minSpeed")]
    public float MinSpeed = 10f;

    public int MobCount = 0;

    /// <summary>
    /// Goobstation
    /// Part damage multiplier done, if not gibbing
    /// </summary>
    [DataField]
    public float PartDamageMultiplier = 2f;

    /// <remarks>
    /// Stuff like wizard rods might want to set this to false, so that they can set the velocity themselves.
    /// </remarks>
    [DataField("randomizeVelocity")]
    public bool RandomizeVelocity = true;

    /// <summary>
    /// If true, this will gib & delete bodies
    /// </summary>
    [DataField]
    public bool ShouldGib = true;

    [DataField("hitSound")]
    public SoundSpecifier Sound = new SoundCollectionSpecifier("MetalSlam");
}
