// SPDX-FileCopyrightText: 2024 Aiden <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2024 Fishbait <Fishbait@git.ml>
// SPDX-FileCopyrightText: 2024 fishbait <gnesse@gmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Ilya246 <ilyukarno@gmail.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Shared.Blob.Components;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Server.Blob.Components;

[RegisterComponent]
public sealed partial class BlobFactoryComponent : Component
{
    [DataField]
    public int AccumulateToSpawn = 3;

    [DataField]
    public int Accumulator = 0;

    [ViewVariables(VVAccess.ReadOnly)]
    public EntityUid? Blobbernaut = default!;

    [DataField("blobbernautId")] [ViewVariables(VVAccess.ReadWrite)]
    public EntProtoId<BlobbernautComponent> BlobbernautId = "MobBlobBlobbernaut";

    [ViewVariables(VVAccess.ReadOnly)]
    public List<EntityUid> BlobPods = new();

    [DataField("blobSporeId")] [ViewVariables(VVAccess.ReadWrite)]
    public EntProtoId<BlobMobComponent> Pod = "MobBlobPod";

    [DataField("spawnLimit")] [ViewVariables(VVAccess.ReadWrite)]
    public float SpawnLimit = 3;
}

public sealed class ProduceBlobbernautEvent : EntityEventArgs
{
}
