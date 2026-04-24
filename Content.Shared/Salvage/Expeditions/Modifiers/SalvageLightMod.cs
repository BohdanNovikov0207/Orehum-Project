// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 deltanedas <@deltanedas:kde.org>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <comedian_vs_clown@hotmail.com>
// SPDX-FileCopyrightText: 2024 chavonadelal <156101927+chavonadelal@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tayrtahn <tayrtahn@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;

namespace Content.Shared.Salvage.Expeditions.Modifiers;

[Prototype("salvageLightMod")]
public sealed class SalvageLightMod : IPrototype, IBiomeSpecificMod
{
    [DataField("color", required: true)] public Color? Color;

    [DataField("desc")] public LocId Description { get; } = string.Empty;

    /// <inheritdoc />
    [DataField("cost")]
    public float Cost { get; } = 0f;

    /// <inheritdoc />
    [DataField]
    public List<ProtoId<SalvageBiomeModPrototype>>? Biomes { get; } = null;

    [IdDataField] public string ID { get; } = default!;
}
