// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <93730715+Aviu00@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;

namespace Content.Shared._Goobstation.Wizard.Spellblade;

[RegisterComponent]
public sealed partial class LightningSpellbladeEnchantmentComponent : Component
{
    [DataField]
    public int ArcDepth = 1;

    [DataField]
    public int BoltCount = 3;

    [DataField]
    public EntProtoId LightningPrototype = "HyperchargedLightning";

    [DataField]
    public float Range = 4f;

    [DataField]
    public float ShockDamage = 30f;

    [DataField]
    public float ShockTime = 1f;

    [DataField]
    public float Siemens = 1f;
}
