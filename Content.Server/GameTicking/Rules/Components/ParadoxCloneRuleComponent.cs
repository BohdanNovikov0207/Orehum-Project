// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 SolsticeOfTheWinter <solsticeofthewinter@gmail.com>
// SPDX-FileCopyrightText: 2025 slarticodefast <161409025+slarticodefast@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Cloning;
using Content.Shared.Whitelist;
using Robust.Shared.Prototypes;

namespace Content.Server.GameTicking.Rules.Components;

/// <summary>
/// Gamerule component for spawning a paradox clone antagonist.
/// </summary>
[RegisterComponent]
public sealed partial class ParadoxCloneRuleComponent : Component
{
    /// <summary>
    /// Visual effect spawned when gibbing at round end.
    /// </summary>
    [DataField]
    public EntProtoId GibProto = "MobParadoxTimed";

    /// <summary>
    /// Blacklist for Objectives to be copied to the clone.
    /// </summary>
    [DataField]
    public EntityWhitelist? ObjectiveBlacklist;

    /// <summary>
    /// Whitelist for Objectives to be copied to the clone.
    /// </summary>
    [DataField]
    public EntityWhitelist? ObjectiveWhitelist;

    /// <summary>
    /// Entity of the original player.
    /// Gets randomly chosen from all alive players if not specified.
    /// </summary>
    [DataField]
    public EntityUid? OriginalBody;

    /// <summary>
    /// Mind entity of the original player.
    /// Gets assigned when cloning.
    /// </summary>
    [DataField]
    public EntityUid? OriginalMind;

    /// <summary>
    /// Cloning settings to be used.
    /// </summary>
    [DataField]
    public ProtoId<CloningSettingsPrototype> Settings = "Antag";

    /// <summary>
    /// If an entity has one of these components, it cannot have a paradox clone. - Goobstation.
    /// </summary>
    [DataField]
    public EntityWhitelist? TargetBlacklist;
}
