// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 username <113782077+whateverusername0@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 whateverusername0 <whateveremail>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;

namespace Content.Shared.Heretic.Prototypes;

[Prototype("hereticKnowledge")]
public sealed class HereticKnowledgePrototype : IPrototype
{
    /// <summary>
    /// What actions should be given
    /// </summary>
    [DataField] public List<EntProtoId>? ActionPrototypes;

    /// <summary>
    /// What event should be raised
    /// </summary>
    [DataField] [NonSerialized] public HereticKnowledgeEvent? Event;

    /// <summary>
    /// Used for codex
    /// </summary>
    [DataField] public string LocDesc = string.Empty;

    /// <summary>
    /// Used for codex
    /// </summary>
    [DataField] public string LocName = string.Empty;

    [DataField] public string? Path;

    /// <summary>
    /// What rituals should be given
    /// </summary>
    [DataField] public List<ProtoId<HereticRitualPrototype>>? RitualPrototypes;

    /// <summary>
    /// Indicates that this should not be on a main branch.
    /// </summary>
    [DataField] public bool SideKnowledge = false;

    [DataField] public int Stage = 1;
    [IdDataField] public string ID { get; } = default!;
}
