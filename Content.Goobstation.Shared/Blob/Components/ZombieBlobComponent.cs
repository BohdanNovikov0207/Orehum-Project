// SPDX-FileCopyrightText: 2024 Aiden <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2024 Fishbait <Fishbait@git.ml>
// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 fishbait <gnesse@gmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Ilya246 <57039557+Ilya246@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Ilya246 <ilyukarno@gmail.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 Rinary <72972221+Rinary1@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._Starlight.CollectiveMind;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Blob.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class ZombieBlobComponent : Component
{
    [AutoNetworkedField]
    public EntityUid BlobPodUid = default!;

    [DataField] [AutoNetworkedField]
    public bool CanShoot = false;

    [DataField]
    public ProtoId<CollectiveMindPrototype> CollectiveMindAdded = "Blobmind";

    [DataField("greetSoundNotification")]
    public SoundSpecifier GreetSoundNotification = new SoundPathSpecifier("/Audio/Ambience/Antag/zombie_start.ogg");

    public float? OldColdDamageThreshold = null;
    public List<string> OldFactions = new();

    [ViewVariables]
    public Dictionary<string, int> DisabledFixtureMasks { get; } = new();
}
