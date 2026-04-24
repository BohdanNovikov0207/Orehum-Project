// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <93730715+Aviu00@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Maths.FixedPoint;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Goobstation.Wizard.SanguineStrike;

[RegisterComponent] [NetworkedComponent]
public sealed partial class SanguineStrikeComponent : Component
{
    [DataField]
    public EntProtoId BloodEffect = "SanguineBloodEffect";

    [DataField]
    public FixedPoint2 BloodSuckAmount = 50;

    [DataField]
    public Color Color = Color.FromHex("#C41515");

    [DataField]
    public float DamageMultiplier = 2f;

    [DataField]
    public EntProtoId Effect = "SanguineFlashEffect";

    [ViewVariables(VVAccess.ReadOnly)]
    public bool HadPointLight;

    [DataField]
    public SoundSpecifier HitSound = new SoundPathSpecifier("/Audio/_Goobstation/Wizard/crackandbleed.ogg");

    [DataField]
    public SoundSpecifier LifestealSound = new SoundPathSpecifier("/Audio/_Goobstation/Wizard/charge.ogg");

    [DataField]
    public float Lifetime = 15f;

    [DataField]
    public float LightEnergy = 3f;

    [DataField]
    public float LightRadius = 4f;

    [DataField]
    public float MaxDamageModifier = 20f;

    [ViewVariables(VVAccess.ReadOnly)]
    public Color OldColor = Color.White;
}
