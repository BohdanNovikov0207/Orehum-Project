// SPDX-FileCopyrightText: 2024 DEATHB4DEFEAT <77995199+DEATHB4DEFEAT@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 portfiend <109661617+portfiend@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2025 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2025 deltanedas <39013340+deltanedas@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 deltanedas <@deltanedas:kde.org>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Maths.FixedPoint;
using Content.Shared._DV.Storage.EntitySystems;
using Robust.Shared.Containers;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._DV.Storage.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
[Access(typeof(SharedMouthStorageSystem))]
public sealed partial class MouthStorageComponent : Component
{
    public const string MouthContainerId = "mouth";

    [DataField] [AutoNetworkedField]
    public EntityUid? Action;

    [ViewVariables]
    public Container Mouth = default!;

    [DataField]
    public EntityUid? MouthId;

    [DataField]
    public EntProtoId MouthProto = "ActionOpenMouthStorage";

    [DataField] [AutoNetworkedField]
    public EntProtoId? OpenStorageAction;

    // Mimimum inflicted damage on hit to spit out items
    [DataField]
    public FixedPoint2 SpitDamageThreshold = FixedPoint2.New(2);
}
