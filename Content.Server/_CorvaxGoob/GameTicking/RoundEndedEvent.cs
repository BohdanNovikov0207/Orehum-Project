// SPDX-FileCopyrightText: 2026 Space Station 14 Contributors
//
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared.GameTicking;

public sealed class RoundEndedEvent : EntityEventArgs
{
    public int RoundId { get; }
    public TimeSpan RoundDuration { get; }

    public RoundEndedEvent(int roundId, TimeSpan roundDuration)
    {
        RoundId = roundId;
        RoundDuration = roundDuration;
    }
}
