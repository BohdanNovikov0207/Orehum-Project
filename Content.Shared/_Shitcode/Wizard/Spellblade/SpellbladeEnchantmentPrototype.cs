// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <93730715+Aviu00@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._Goobstation.Wizard.Spellblade;

[DataDefinition]
[Prototype("spellbladeEnchantment")]
public sealed partial class SpellbladeEnchantmentPrototype : IPrototype
{
    [DataField(required: true)]
    public string Desc;

    [DataField(required: true)]
    public object? Event;

    [DataField(required: true)]
    public SpriteSpecifier Icon;

    [DataField(required: true)]
    public LocId Name;

    [IdDataField]
    public string ID { get; private set; }
}
