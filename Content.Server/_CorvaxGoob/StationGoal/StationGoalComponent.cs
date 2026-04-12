// SPDX-FileCopyrightText: 2026 Space Station 14 Contributors
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;

namespace Content.Server._CorvaxGoob.StationGoal
{
    /// <summary>
    ///     if attached to a station prototype, will send the station a random goal from the list
    /// </summary>
    [RegisterComponent]
    public sealed partial class StationGoalComponent : Component
    {
        [DataField]
        public List<ProtoId<StationGoalPrototype>> Goals = new();
    }
}
