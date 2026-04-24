// SPDX-FileCopyrightText: 2022 Jezithyr <Jezithyr@gmail.com>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Jezithyr <jezithyr@gmail.com>
// SPDX-FileCopyrightText: 2023 Leon Friedrich <60421075+ElectroJr@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <comedian_vs_clown@hotmail.com>
// SPDX-FileCopyrightText: 2024 Aviu00 <93730715+Aviu00@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 username <113782077+whateverusername0@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 whateverusername0 <whateveremail>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Kayzel <43700376+KayzelW@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Roudenn <romabond091@gmail.com>
// SPDX-FileCopyrightText: 2025 Spatison <137375981+Spatison@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Trest <144359854+trest100@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 deltanedas <@deltanedas:kde.org>
// SPDX-FileCopyrightText: 2025 gluesniffler <159397573+gluesniffler@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 gluesniffler <linebarrelerenthusiast@gmail.com>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
// SPDX-FileCopyrightText: 2025 kurokoTurbo <92106367+kurokoTurbo@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Shitmed.Body.Part;
using Content.Shared._Shitmed.Medical.Surgery.Tools;
using Content.Shared._Shitmed.Medical.Surgery.Wounds;
using Content.Shared.Body.Systems;
using Content.Shared.Containers.ItemSlots;
using Robust.Shared.Containers;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
// Shitmed Change

namespace Content.Shared.Body.Part;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
//[Access(typeof(SharedBodySystem))] // goob edit - all access :godo:
public sealed partial class BodyPartComponent : Component, ISurgeryToolComponent // Shitmed Change
{
    /// <summary>
    /// Shitmed Change: The ID of the base layer for this body part.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public string? BaseLayerId;

    // Need to set this on container changes as it may be several transform parents up the hierarchy.
    /// <summary>
    /// Parent body for this part.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public EntityUid? Body;

    /// <summary>
    /// Whether this body part can attach children or not.
    /// </summary>
    [DataField]
    public bool CanAttachChildren = true;

    /// <summary>
    /// Shitmed Change: Whether this body part can be enabled or not. Used for non-functional prosthetics.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public bool CanEnable = true;

    // Shitmed Change End

    /// <summary>
    /// Child body parts attached to this body part.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public Dictionary<string, BodyPartSlot> Children = new();

    /// <summary>
    /// Shitmed Change: Whether this body part is enabled or not.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public bool Enabled = true;

    /// <summary>
    /// Shitmed Change: On what WoundableSeverity we should re-enable the part.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public WoundableSeverity EnableIntegrity = WoundableSeverity.Severe;

    /// <summary>
    /// Shitmed Change: The slot for item insertion.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public ItemSlot ItemInsertionSlot = new();

    /// <summary>
    /// When attached, the part will ensure these components on the entity, and delete them on removal.
    /// </summary>
    [DataField] [AlwaysPushInheritance]
    public ComponentRegistry? OnAdd;

    /// <summary>
    /// When removed, the part will ensure these components on the entity, and add them on removal.
    /// </summary>
    [DataField] [AlwaysPushInheritance]
    public ComponentRegistry? OnRemove;

    /// <summary>
    /// Organs attached to this body part.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public Dictionary<string, OrganSlot> Organs = new();

    // Shitmed Change Start

    [DataField] [AutoNetworkedField]
    public BodyPartSlot? ParentSlot;

    /// <summary>
    /// Shitmed Change: What composition does this body part classify as
    /// </summary>
    [DataField]
    public BodyPartComposition PartComposition = BodyPartComposition.Organic;

    [DataField] [AutoNetworkedField]
    public BodyPartType PartType = BodyPartType.Other;

    [DataField]
    public string SlotId = string.Empty;

    [DataField] [AutoNetworkedField]
    public BodyPartSymmetry Symmetry = BodyPartSymmetry.None;

    /// <summary>
    /// Shitmed Change: The name of the container for this body part. Used in insertion surgeries.
    /// </summary>
    [DataField]
    public string ContainerName { get; set; } = "part_slot";


    /// <summary>
    /// Shitmed Change: Current species. Dictates things like body part sprites.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public string Species { get; set; } = "";

    /// <summary>
    /// These are only for VV/Debug do not use these for gameplay/systems
    /// </summary>
    [ViewVariables]
    private List<ContainerSlot> BodyPartSlotsVV
    {
        get
        {
            List<ContainerSlot> temp = new();
            var containerSystem = IoCManager.Resolve<IEntityManager>().System<SharedContainerSystem>();

            foreach (var slotId in Children.Keys)
            {
                temp.Add((ContainerSlot) containerSystem.GetContainer(Owner,
                    SharedBodySystem.PartSlotContainerIdPrefix + slotId));
            }

            return temp;
        }
    }

    [ViewVariables]
    private List<ContainerSlot> OrganSlotsVV
    {
        get
        {
            List<ContainerSlot> temp = new();
            var containerSystem = IoCManager.Resolve<IEntityManager>().System<SharedContainerSystem>();

            foreach (var slotId in Organs.Keys)
            {
                temp.Add((ContainerSlot) containerSystem.GetContainer(Owner,
                    SharedBodySystem.OrganSlotContainerIdPrefix + slotId));
            }

            return temp;
        }
    }

    [DataField]
    public string ToolName { get; set; } = "A body part";

    [DataField] [AutoNetworkedField]
    public bool? Used { get; set; } = null;

    [DataField]
    public float Speed { get; set; } = 1f;
}

/// <summary>
/// Contains metadata about a body part in relation to its slot.
/// </summary>
[NetSerializable] [Serializable]
[DataRecord]
public struct BodyPartSlot
{
    public string Id;
    public BodyPartType Type;
    public BodyPartSymmetry Symmetry; // Shitmed Change - Adds Symmetry to BodyPartSlot

    public BodyPartSlot(string id, BodyPartType type, BodyPartSymmetry symmetry)
    {
        Id = id;
        Type = type;
        Symmetry = symmetry;
    }
}

/// <summary>
/// Contains metadata about an organ part in relation to its slot.
/// </summary>
[NetSerializable] [Serializable]
[DataRecord]
public struct OrganSlot
{
    public string Id;

    public OrganSlot(string id)
    {
        Id = id;
    }
}
