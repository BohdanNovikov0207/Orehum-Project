// SPDX-FileCopyrightText: 2022 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 LordEclipse <106132477+LordEclipse@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 brainfood1183 <113240905+brainfood1183@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 NULL882 <gost6865@yandex.ru>
// SPDX-FileCopyrightText: 2024 ScyronX <166930367+ScyronX@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 gluesniffler <159397573+gluesniffler@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 gluesniffler <linebarrelerenthusiast@gmail.com>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.Whitelist;
using Robust.Shared.Containers;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.Mech.Components;

/// <summary>
/// A large, pilotable machine that has equipment that is
/// powered via an internal battery.
/// </summary>
[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class MechComponent : Component
{
    [ViewVariables]
    public readonly string BatterySlotId = "mech-battery-slot";

    [ViewVariables]
    public readonly string EquipmentContainerId = "mech-equipment-container";

    [ViewVariables]
    public readonly string PilotSlotId = "mech-pilot-slot";

    /// <summary>
    /// Whether or not the mech is airtight.
    /// </summary>
    /// <remarks>
    /// This needs to be redone
    /// when mech internals are added
    /// </remarks>
    [DataField] [ViewVariables(VVAccess.ReadWrite)]
    public bool Airtight;

    /// <summary>
    /// How long it takes to pull out the battery.
    /// </summary>
    [DataField] [ViewVariables(VVAccess.ReadWrite)]
    public float BatteryRemovalDelay = 2;

    /// <summary>
    /// The slot the battery is stored in.
    /// </summary>
    [ViewVariables]
    public ContainerSlot BatterySlot = default!;

    /// <summary>
    /// Goobstation: Whether or not an emag disables it.
    /// </summary>
    [DataField("breakOnEmag")]
    [AutoNetworkedField]
    public bool BreakOnEmag = true;

    /// <summary>
    /// Whether the mech has been destroyed and is no longer pilotable.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)] [AutoNetworkedField]
    public bool Broken = false;

    /// <summary>
    /// The current selected equipment of the mech.
    /// If null, the mech is using just its fists.
    /// </summary>
    [ViewVariables] [AutoNetworkedField]
    public EntityUid? CurrentSelectedEquipment;

    /// <summary>
    /// How much energy the mech has.
    /// Derived from the currently inserted battery.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)] [AutoNetworkedField]
    public FixedPoint2 Energy = 0;

    /// <summary>
    /// How long it takes to enter the mech.
    /// </summary>
    [DataField] [ViewVariables(VVAccess.ReadWrite)]
    public float EntryDelay = 3;


    /// <summary>
    /// A container for storing the equipment entities.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)]
    public Container EquipmentContainer = default!;

    /// <summary>
    /// A whitelist for inserting equipment items.
    /// </summary>
    [DataField]
    public EntityWhitelist? EquipmentWhitelist;

    /// <summary>
    /// How long it takes to pull *another person*
    /// outside of the mech. You can exit instantly yourself.
    /// </summary>
    [DataField] [ViewVariables(VVAccess.ReadWrite)]
    public float ExitDelay = 3;

    /// <summary>
    /// How much "health" the mech has left.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)] [AutoNetworkedField]
    public FixedPoint2 Integrity;

    /// <summary>
    /// The maximum amount of energy the mech can have.
    /// Derived from the currently inserted battery.
    /// </summary>
    [DataField] [AutoNetworkedField] [ViewVariables(VVAccess.ReadWrite)]
    public FixedPoint2 MaxEnergy = 0;

    /// <summary>
    /// The maximum amount of equipment items that can be installed in the mech
    /// </summary>
    [DataField("maxEquipmentAmount")] [ViewVariables(VVAccess.ReadWrite)]
    public int MaxEquipmentAmount = 3;

    /// <summary>
    /// The maximum amount of damage the mech can take.
    /// </summary>
    [DataField] [AutoNetworkedField] [ViewVariables(VVAccess.ReadWrite)]
    public FixedPoint2 MaxIntegrity = 250;

    [DataField] public EntityUid? MechCycleActionEntity;
    [DataField] public EntityUid? MechEjectActionEntity;

    /// <summary>
    /// A multiplier used to calculate how much of the damage done to a mech
    /// is transfered to the pilot
    /// </summary>
    [DataField] [ViewVariables(VVAccess.ReadWrite)]
    public float MechToPilotDamageMultiplier;

    [DataField] public EntityUid? MechUiActionEntity;

    [DataField]
    public EntityWhitelist? PilotBlacklist; // Goobstation Change

    /// <summary>
    /// The slot the pilot is stored in.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite)]
    public ContainerSlot PilotSlot = default!;

    [DataField]
    public EntityWhitelist? PilotWhitelist;

    /// <summary>
    /// The equipment that the mech initially has when it spawns.
    /// Good for things like nukie mechs that start with guns.
    /// </summary>
    [DataField]
    public List<EntProtoId> StartingEquipment = new();

    [DataField] [AutoNetworkedField] public EntityUid? ToggleActionEntity; //Goobstation Mech Lights toggle action

    #region Action Prototypes

    [DataField]
    public EntProtoId MechCycleAction = "ActionMechCycleEquipment";

    [DataField]
    public EntProtoId ToggleAction = "ActionToggleLight"; //Goobstation Mech Lights toggle action

    [DataField]
    public EntProtoId MechUiAction = "ActionMechOpenUI";

    [DataField]
    public EntProtoId MechEjectAction = "ActionMechEject";

    #endregion

    #region Visualizer States

    [DataField]
    public string? BaseState;

    [DataField]
    public string? OpenState;

    [DataField]
    public string? BrokenState;

    #endregion
}
