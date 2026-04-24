// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Pieter-Jan Briers <pieterjan.briers@gmail.com>
// SPDX-FileCopyrightText: 2024 Aidenkrz <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2024 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2024 Tayrtahn <tayrtahn@gmail.com>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Shuttles.Systems;
using Content.Shared.Tag;
using Content.Shared.Timing;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;

namespace Content.Shared.Shuttles.Components;

/// <summary>
/// Added to a component when it is queued or is travelling via FTL.
/// </summary>
[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class FTLComponent : Component
{
    /// <summary>
    /// If we're docking after FTL what is the prioritised dock tag (if applicable).
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)] [DataField]
    public ProtoId<TagPrototype>? PriorityTag;

    [DataField]
    public EntityUid? StartupStream;

    [ViewVariables(VVAccess.ReadWrite)]
    public float StartupTime = 0f;
    // TODO Full game save / add datafields

    [ViewVariables]
    public FTLState State = FTLState.Available;

    [ViewVariables(VVAccess.ReadWrite)]
    public StartEndTime StateTime;

    [DataField] [AutoNetworkedField]
    public Angle TargetAngle;

    /// <summary>
    /// Coordinates to arrive it: May be relative to another grid (for docking) or map coordinates.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public EntityCoordinates TargetCoordinates;

    [ViewVariables(VVAccess.ReadWrite)] [DataField("soundTravel")]
    public SoundSpecifier? TravelSound = new SoundPathSpecifier("/Audio/Effects/Shuttle/hyperspace_progress.ogg")
    {
        Params = AudioParams.Default.WithVolume(-3f).WithLoop(true),
    };

    [DataField]
    public EntityUid? TravelStream;

    // Because of sphagetti, actual travel time is Math.Max(TravelTime, DefaultArrivalTime)
    [ViewVariables(VVAccess.ReadWrite)]
    public float TravelTime = 0f;

    [DataField] [AutoNetworkedField]
    public EntityUid? VisualizerEntity;

    [DataField]
    public EntProtoId? VisualizerProto = "FtlVisualizerEntity";
}
