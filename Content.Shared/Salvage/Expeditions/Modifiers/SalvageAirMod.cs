// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 deltanedas <@deltanedas:kde.org>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <comedian_vs_clown@hotmail.com>
// SPDX-FileCopyrightText: 2024 chavonadelal <156101927+chavonadelal@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Atmos;
using Robust.Shared.Prototypes;

namespace Content.Shared.Salvage.Expeditions.Modifiers;

/// <summary>
/// Prototype for a planet's air gas mixture.
/// Used when creating the planet for a salvage expedition.
/// Which one is selected depends on the mission difficulty, different weightedRandoms are picked from.
/// </summary>
[Prototype("salvageAirMod")]
public sealed class SalvageAirMod : IPrototype, IBiomeSpecificMod
{
    /// <summary>
    /// Number of moles of each gas in the mixture.
    /// </summary>
    [DataField("gases")]
    public float[] Gases = new float[Atmospherics.AdjustedNumberOfGases];

    /// <summary>
    /// Set to true if this planet will have no atmosphere.
    /// </summary>
    [DataField("space")]
    public bool Space;

    /// <inheritdoc />
    [DataField("desc")]
    public LocId Description { get; } = string.Empty;

    /// <inheritdoc />
    [DataField("cost")]
    public float Cost { get; } = 0f;

    /// <inheritdoc />
    [DataField]
    public List<ProtoId<SalvageBiomeModPrototype>>? Biomes { get; } = null;

    [IdDataField]
    public string ID { get; } = default!;
}
