// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aidenkrz <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <aviu00@protonmail.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Lincoln McQueen <lincoln.mcqueen@gmail.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 SX-7 <sn1.test.preria.2002@gmail.com>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Common.MartialArts;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.MartialArts.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class CanPerformComboComponent : Component
{
    [DataField]
    public List<ComboPrototype> AllowedCombos = new();

    [DataField] [AutoNetworkedField]
    public ProtoId<ComboPrototype> BeingPerformed;

    [DataField] [AutoNetworkedField]
    public int ConsecutiveGnashes;

    [DataField] [AutoNetworkedField]
    public EntityUid? CurrentTarget;

    [DataField] [AutoNetworkedField]
    public List<ComboAttackType> LastAttacks = new();

    [DataField]
    public int LastAttacksLimit = 4;

    [DataField]
    public List<ComboAttackType>? LastAttacksSaved = new();

    [DataField]
    public TimeSpan ResetTime = TimeSpan.Zero;

    [DataField]
    public List<ProtoId<ComboPrototype>> RoundstartCombos = new();
}
