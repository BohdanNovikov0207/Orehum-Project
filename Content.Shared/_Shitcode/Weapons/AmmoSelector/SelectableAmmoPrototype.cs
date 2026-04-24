// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <93730715+Aviu00@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 SolsticeOfTheWinter <solsticeofthewinter@gmail.com>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Audio;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;
using Robust.Shared.Utility;

namespace Content.Shared._Goobstation.Weapons.AmmoSelector;

[Prototype("selectableAmmo")]
public sealed class SelectableAmmoPrototype : IPrototype
{
    [DataField]
    public Color? Color;

    [DataField(required: true)]
    public string Desc = default!;

    [DataField]
    public float FireCost = 100f;

    [DataField]
    public float FireRate = 8f;

    [DataField(customTypeSerializer: typeof(FlagSerializer<SelectableAmmoWeaponFlags>))]
    public int Flags = (int) SelectableAmmoFlags.ChangeWeaponFireCost;

    [DataField(required: true)]
    public SpriteSpecifier Icon = default!;

    [DataField(required: true)]
    public string ProtoId = default!; // this has to be a string because of how hitscan projectiles work

    [DataField]
    public SoundSpecifier? SoundGunshot;

    [IdDataField]
    public string ID { get; } = default!;
}

public sealed class SelectableAmmoWeaponFlags;

[Serializable] [NetSerializable]
[Flags] [FlagsFor(typeof(SelectableAmmoWeaponFlags))]
public enum SelectableAmmoFlags
{
    None = 0,
    ChangeWeaponFireCost = 1 << 0,
    ChangeWeaponFireSound = 1 << 1,
    ChangeWeaponFireRate = 1 << 2,
    All = ~None,
}
