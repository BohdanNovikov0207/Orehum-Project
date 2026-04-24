// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 LuciferMkshelter <154002422+LuciferEOS@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Shitmed.Damage;
using Content.Shared._Shitmed.Targeting;
using Content.Shared.Damage;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization.TypeSerializers.Implementations;

namespace Content.Goobstation.Shared.Clothing.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState] [AutoGenerateComponentPause]
public sealed partial class DamageOverTimeComponent : Component
{
    [DataField(customTypeSerializer: typeof(TimespanSerializer))] [AutoNetworkedField]
    public TimeSpan Interval = TimeSpan.FromSeconds(1);

    [DataField] [AutoNetworkedField]
    public float Multiplier = 1f;

    [DataField] [AutoNetworkedField]
    public float MultiplierIncrease;

    [DataField] [AutoPausedField]
    public TimeSpan NextTickTime = TimeSpan.Zero;

    [DataField] [AutoNetworkedField]
    public SplitDamageBehavior Split = SplitDamageBehavior.Split;

    [DataField] [AutoNetworkedField]
    public TargetBodyPart? TargetBodyPart;

    [DataField(required: true)] [AutoNetworkedField]
    public DamageSpecifier Damage { get; set; } = new();

    [DataField] [AutoNetworkedField]
    public bool IgnoreResistances { get; set; }
}
