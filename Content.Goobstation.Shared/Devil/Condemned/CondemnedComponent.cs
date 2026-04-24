// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Solstice <solsticeofthewinter@gmail.com>
// SPDX-FileCopyrightText: 2025 SolsticeOfTheWinter <solsticeofthewinter@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Polymorph;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Devil.Condemned;

/// <summary>
/// Marks an entity as having sold their soul.
/// When you die, do NOT collect 200, do NOT pass go. Go directly to hell
/// </summary>
/// <remarks>
/// This should *really* be two components.
/// </remarks>
[RegisterComponent]
public sealed partial class CondemnedComponent : Component
{
    [DataField]
    public ProtoId<PolymorphPrototype> BanishProto = "ShadowJaunt180";

    /// <summary>
    /// Should this entity be banished (sent to limbo for several minutes) or should they just be deleted?
    /// </summary>
    [DataField]
    public CondemnedBehavior CondemnedBehavior = CondemnedBehavior.Delete;

    /// <summary>
    /// Should this entity be sent to hell on death?
    /// </summary>
    [DataField]
    public bool CondemnOnDeath;

    /// <summary>
    /// The current phase of the condemnation animation.
    /// </summary>
    [DataField]
    public CondemnedPhase CurrentPhase = CondemnedPhase.Waiting;

    /// <summary>
    /// Should movement be locked during the animation?
    /// </summary>
    [DataField]
    public bool FreezeDuringCondemnation;

    /// <summary>
    /// How long the hand effect will last
    /// </summary>
    [DataField]
    public float HandDuration;

    [DataField]
    public EntProtoId HandProto = "HellHand";

    [DataField]
    public EntProtoId PentagramProto = "Pentagram";

    /// <summary>
    /// The elapsed time of the phase.
    /// </summary>
    [DataField]
    public float PhaseTimer;

    /// <summary>
    /// If true, scrambles the targets DNA after banishing them.
    /// </summary>
    [DataField]
    public bool ScrambleAfterBanish = true;

    /// <summary>
    /// Should the examine message show when examining someone with this component?
    /// </summary>
    [DataField("showExamine")]
    public bool ShowExamineMessage = true;

    /// <summary>
    /// Is this entities soul owned, but not by a devil?
    /// </summary>
    [DataField]
    public bool SoulOwnedNotDevil;

    /// <summary>
    /// Who owns this entities soul
    /// </summary>
    [DataField]
    public EntityUid? SoulOwner;

    [DataField]
    public SoundPathSpecifier SoundEffect = new("/Audio/_Goobstation/Effects/earth_quake.ogg");

    /// <summary>
    /// Was this target already weak to holy before becoming condemned?
    /// </summary>
    [DataField]
    public bool WasWeakToHoly;
}

public enum CondemnedPhase : byte
{
    Waiting,
    PentagramActive,
    HandActive,
    Complete,
}

public enum CondemnedBehavior : byte
{
    Delete,
    Banish,
}
