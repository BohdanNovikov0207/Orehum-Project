// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <93730715+Aviu00@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Maths.FixedPoint;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Changeling;

[Prototype("reagentStingConfiguration")]
public sealed class ReagentStingConfigurationPrototype : IPrototype
{
    [DataField(required: true)]
    public Dictionary<string, FixedPoint2> Reagents = new();

    [IdDataField]
    public string ID { get; } = default!;
}
