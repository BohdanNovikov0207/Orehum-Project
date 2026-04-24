// SPDX-FileCopyrightText: 2026 Space Station 14 Contributors
//
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared.GameTicking;

public sealed class RoundStartedEvent : EntityEventArgs
{
    public RoundStartedEvent(int roundId)
    {
        RoundId = roundId;
    }

    public int RoundId { get; }
}
