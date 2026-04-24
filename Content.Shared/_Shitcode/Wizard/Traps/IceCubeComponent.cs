// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <93730715+Aviu00@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Atmos;
using Content.Shared.Damage;
using Content.Shared.Physics;
using Robust.Shared.GameStates;
using Robust.Shared.Physics;
using Robust.Shared.Physics.Dynamics;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;
using Robust.Shared.Utility;

namespace Content.Shared._Goobstation.Wizard.Traps;

[RegisterComponent] [NetworkedComponent]
public sealed partial class IceCubeComponent : Component
{
    [DataField]
    public TimeSpan BreakFreeDelay = TimeSpan.FromSeconds(10);

    [DataField(customTypeSerializer: typeof(FlagSerializer<CollisionLayer>))]
    public int CollisionLayer = (int) CollisionGroup.WallLayer;

    [DataField(customTypeSerializer: typeof(FlagSerializer<CollisionMask>))]
    public int CollisionMask = (int) CollisionGroup.FullTileMask;

    [DataField]
    public float DamageMeltProbabilityThreshold = 60f;

    [DataField]
    public DamageModifierSet DamageReduction = new()
    {
        Coefficients =
        {
            { "Blunt", 0.35f },
            { "Slash", 0.35f },
            { "Piercing", 0.35f },
        },
    };

    [DataField]
    public BodyType FrozenBodyType = BodyType.Dynamic;

    [DataField]
    public float FrozenTemperature = Atmospherics.T0C - 200f;

    [ViewVariables(VVAccess.ReadOnly)]
    public BodyType? OldBodyType = null;

    [DataField]
    public float Restitution = 0.8f;

    [DataField]
    public SpriteSpecifier Sprite =
        new SpriteSpecifier.Rsi(new ResPath("_Goobstation/Wizard/Effects/effects.rsi"), "ice_cube");

    [DataField]
    public float StaminaDamageMeltProbabilityMultiplier = 5f;

    [DataField]
    public float SustainedDamage;

    [DataField]
    public float SustainedDamageMeltProbabilityMultiplier = 4f;

    [DataField]
    public float TemperaturePerHeatDamageIncrease = 5f;

    [DataField]
    public float TileFriction = 0.01f;

    [DataField]
    public float UnfreezeTemperatureThreshold = Atmospherics.T0C;

    [DataField]
    public float UnfrozenTemperature = Atmospherics.T0C - 100f;

    [DataField]
    public float VelocityMultiplier = 0.2f;
}

public enum IceCubeKey : byte
{
    Key,
}
