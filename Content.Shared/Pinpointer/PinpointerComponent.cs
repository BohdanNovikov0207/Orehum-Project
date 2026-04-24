// SPDX-FileCopyrightText: 2021 Alexander Evgrashin <evgrashin.adl@gmail.com>
// SPDX-FileCopyrightText: 2022 Alex Evgrashin <aevgrashin@yandex.ru>
// SPDX-FileCopyrightText: 2022 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2022 Vera Aguilera Puerto <6766154+Zumorica@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Chief-Engineer <119664036+Chief-Engineer@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Slava0135 <40753025+Slava0135@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <comedian_vs_clown@hotmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Roudenn <romabond091@gmail.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.Alert;
using Content.Shared.Whitelist;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Pinpointer;

/// <summary>
/// Displays a sprite on the item that points towards the target component.
/// </summary>
[RegisterComponent] [NetworkedComponent]
[AutoGenerateComponentState]
[Access(typeof(SharedPinpointerSystem))]
public sealed partial class PinpointerComponent : Component
{
    // WD EDIT START
    [DataField]
    public ProtoId<AlertPrototype>? Alert;

    [ViewVariables] [AutoNetworkedField]
    public Angle ArrowAngle;

    [DataField]
    public EntityWhitelist? Blacklist;

    [DataField]
    public bool CanEmag = true;

    [DataField]
    public bool CanExamine = true;

    /// <summary>
    /// Whether or not the target can be reassigned.
    /// </summary>
    [DataField("canRetarget")] [ViewVariables(VVAccess.ReadWrite)]
    public bool CanRetarget;

    /// <summary>
    /// Goob edit: if true, this pinpointer will automatically track ANY nearest entity of a specified type.
    /// Doesn't work with retargeting, it will always left only one entity in target list.
    /// </summary>
    [DataField]
    public bool CanTargetMultiple = true;

    [DataField]
    public bool CanToggle = true;

    [DataField("closeDistance")] [ViewVariables(VVAccess.ReadWrite)]
    public float CloseDistance = 8f;

    [ViewVariables] [AutoNetworkedField]
    public Distance DistanceToTarget = Distance.Unknown;
    // WD EDIT END

    [DataField] [AutoNetworkedField] // WD EDIT: ViewVariables -> DataField
    public bool IsActive = false;

    [DataField("mediumDistance")] [ViewVariables(VVAccess.ReadWrite)]
    public float MediumDistance = 16f;

    /// <summary>
    /// Pinpointer arrow precision in radians.
    /// </summary>
    [DataField("precision")] [ViewVariables(VVAccess.ReadWrite)]
    public double Precision = 0.09;

    [DataField("reachedDistance")] [ViewVariables(VVAccess.ReadWrite)]
    public float ReachedDistance = 1f;

    [DataField]
    public EntityWhitelist? RetargetingBlacklist;

    /// <summary>
    /// Goob edit: pinpointer can retarget only whitelisted entities if specified.
    /// </summary>
    [DataField]
    public EntityWhitelist? RetargetingWhitelist;

    /// <summary>
    /// Name to display of the target being tracked.
    /// </summary>
    [DataField("targetName")] [ViewVariables(VVAccess.ReadWrite)]
    public string? TargetName;

    /// <summary>
    /// Goob edit: many targets instead of just one
    /// </summary>
    [ViewVariables]
    public List<EntityUid> Targets = new();

    /// <summary>
    /// Whether or not the target name should be updated when the target is updated.
    /// </summary>
    [DataField("updateTargetName")] [ViewVariables(VVAccess.ReadWrite)]
    public bool UpdateTargetName;

    /// <summary>
    /// Goob edit: pinpointer now works on EntityWhitelist (actually only on components but nvm)
    /// </summary>
    [DataField]
    public EntityWhitelist? Whitelist;

    [ViewVariables]
    public bool HasTarget => DistanceToTarget != Distance.Unknown;
}

[Serializable] [NetSerializable]
public enum Distance : byte
{
    Unknown,
    Reached,
    Close,
    Medium,
    Far,
}
