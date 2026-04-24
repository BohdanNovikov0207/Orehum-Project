// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 ElectroJr <leonsfriedrich@gmail.com>
// SPDX-FileCopyrightText: 2023 Emisse <99158783+Emisse@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 TemporalOroboros <TemporalOroboros@gmail.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Tayrtahn <tayrtahn@gmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 BombasterDS <deniskaporoshok@gmail.com>
// SPDX-FileCopyrightText: 2025 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.Chemistry.Components;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Shared.Fluids.Components;

/// <summary>
/// Puddle on a floor
/// </summary>
[RegisterComponent] [NetworkedComponent] [Access(typeof(SharedPuddleSystem))]
public sealed partial class PuddleComponent : Component
{
    // Corvax-Next-Footprints-Start
    [DataField]
    public bool AffectsMovement = true;

    [DataField]
    public bool AffectsSound = true;

    /// <summary>
    /// Default minimum speed someone must be moving to slip for all reagents.
    /// </summary>
    [DataField]
    public float DefaultSlippery = 5.5f;

    [DataField]
    public FixedPoint2 OverflowVolume = FixedPoint2.New(20);

    [ViewVariables]
    public Entity<SolutionComponent>? Solution;

    [DataField("solution")] public string SolutionName = "puddle";

    [DataField]
    public SoundSpecifier SpillSound = new SoundPathSpecifier("/Audio/Effects/Fluids/splat.ogg");
    // Corvax-Next-Footprints-End
}
