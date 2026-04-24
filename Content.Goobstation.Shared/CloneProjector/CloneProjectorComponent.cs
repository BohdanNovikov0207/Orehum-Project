// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Solstice <solsticeofthewinter@gmail.com>
// SPDX-FileCopyrightText: 2025 SolsticeOfTheWinter <solsticeofthewinter@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Damage;
using Content.Shared.Damage.Prototypes;
using Content.Shared.Whitelist;
using Robust.Shared.Containers;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.CloneProjector;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class CloneProjectorComponent : Component
{
    /// <summary>
    /// The ID of the action used to activate the projector.
    /// </summary>
    [DataField]
    public EntProtoId Action = "ActionActivateProjector";

    [ViewVariables(VVAccess.ReadOnly)] [AutoNetworkedField]
    public EntityUid? ActionEntity;

    [DataField]
    public ComponentRegistry? AddedComponents;

    [ViewVariables(VVAccess.ReadOnly)]
    public Container CloneContainer = new();

    [DataField]
    public ProtoId<DamageModifierSetPrototype> CloneDamageModifierSet = "LivingLight";

    /// <summary>
    /// Items on the host that *have* this component will not be duplicated.
    /// </summary>
    [DataField]
    public EntityWhitelist? ClonedItemBlacklist;

    /// <summary>
    /// Items on the host that do *not* have this component will not be duplicated.
    /// </summary>
    [DataField]
    public EntityWhitelist? ClonedItemWhitelist;

    [DataField]
    public LocId CloneGeneratedMessage = "gemini-projector-clone-created";

    [DataField]
    public LocId CloneRetrievedMessage = "gemini-projector-clone-retrieved";

    /// <summary>
    /// The UID of the active clone.
    /// </summary>
    [ViewVariables(VVAccess.ReadOnly)] [AutoNetworkedField]
    public EntityUid? CloneUid;

    [ViewVariables(VVAccess.ReadOnly)] [AutoNetworkedField]
    public EntityUid? CurrentHost;

    /// <summary>
    /// How much damage does the host take when the clone is destroyed?
    /// </summary>
    [DataField]
    public DamageSpecifier DamageOnDestroyed = new();

    /// <summary>
    /// How long it takes to regenerate the clone when destroyed.
    /// </summary>
    [DataField]
    public TimeSpan DestroyedCooldown = TimeSpan.FromSeconds(90);

    /// <summary>
    /// Should the host be stunned when the clone is destroyed?
    /// </summary>
    [DataField]
    public bool DoStun = true;

    [DataField]
    public LocId EquippedMessage = "gemini-projector-installed";

    [DataField]
    public LocId FlavorText = "gemini-projector-clone-flavor-text";

    [DataField]
    public LocId GhostRoleDescription = "ghost-role-information-gemini-clone-description";

    [DataField]
    public LocId GhostRoleName = "ghost-role-information-gemini-clone-name";

    [DataField]
    public LocId GhostRoleRules = "ghost-role-information-familiar-rules";

    /// <summary>
    /// The suffix attached to the end of this clones name.
    /// </summary>
    [DataField]
    public LocId NameSuffix = "gemini-projector-clone-name-suffix";

    [DataField]
    public ComponentRegistry? RemovedComponents;

    /// <summary>
    /// Should the clone be prevented from using ranged weapons?
    /// </summary>
    [DataField]
    public bool RestrictRangedWeapons = true;

    /// <summary>
    /// How much the strip time should be increased by.
    /// </summary>
    [DataField]
    public TimeSpan StripTimeAddition = TimeSpan.FromSeconds(15);

    /// <summary>
    /// How long the host is stunned when the hologram is destroyed.
    /// </summary>
    [DataField]
    public TimeSpan StunDuration = TimeSpan.FromSeconds(8);

    [DataField]
    public LocId UnequippedMessage = "gemini-projector-removed";

    /// <summary>
    /// If the entity using this projector matches the whitelist, prevent use.
    /// </summary>
    [DataField]
    public EntityWhitelist? UserBlacklist;
}
