// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Fishbait <Fishbait@git.ml>
// SPDX-FileCopyrightText: 2025 fishbait <gnesse@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Alert;
using Content.Shared.Whitelist;
using Robust.Shared.Prototypes;

namespace Content.Shared._Imp.Drone;
// Goobstation - Moved into shared

[RegisterComponent] [AutoGenerateComponentPause] [AutoGenerateComponentState]
public sealed partial class DroneComponent : Component
{
    [DataField]
    public ProtoId<AlertPrototype> BatteryAlert = "DroneBattery";

    [DataField] [AutoNetworkedField] // Goob - Removed redudnant VV attribute
    public EntityWhitelist? Blacklist;

    public float InteractionBlockRange = 1.5f;

    public short LastChargePercent;

    public EntityUid NearestEnt = default!;

    [AutoPausedField]
    public TimeSpan NextProximityAlert = new();

    [DataField]
    public ProtoId<AlertPrototype> NoBatteryAlert = "BorgBatteryNone";

    /// imp. original value was 2.15, changed because it was annoying. this also does not actually block interactions anymore.

    // imp. delay before posting another proximity alert
    public TimeSpan ProximityDelay = TimeSpan.FromMilliseconds(2000);

    [DataField] [AutoNetworkedField] // Goob - Removed redudnant VV attribute
    public EntityWhitelist? Whitelist;
}
