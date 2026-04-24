// SPDX-FileCopyrightText: 2022 Ygg01 <y.laughing.man.y@gmail.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Content.Goobstation.Maths.FixedPoint;
using Robust.Shared.Serialization;

namespace Content.Shared.Fluids;

public abstract class SharedPuddleDebugOverlaySystem : EntitySystem
{
    protected const float LocalViewRange = 16;
    protected TimeSpan Cooldown = TimeSpan.FromSeconds(0.5f);
    protected TimeSpan? NextTick = null;
}

/// <summary>
/// Message for disable puddle overlay
/// </summary>
[Serializable] [NetSerializable]
public sealed class PuddleOverlayDisableMessage : EntityEventArgs
{
}

/// <summary>
/// Message for puddle overlay display data
/// </summary>
[Serializable] [NetSerializable]
public sealed class PuddleOverlayDebugMessage : EntityEventArgs
{
    public PuddleOverlayDebugMessage(NetEntity gridUid, PuddleDebugOverlayData[] overlayData)
    {
        GridUid = gridUid;
        OverlayData = overlayData;
    }

    public PuddleDebugOverlayData[] OverlayData { get; }

    public NetEntity GridUid { get; }
}

[Serializable] [NetSerializable]
public readonly struct PuddleDebugOverlayData
{
    public readonly Vector2i Pos;
    public readonly FixedPoint2 CurrentVolume;

    public PuddleDebugOverlayData(Vector2i pos, FixedPoint2 currentVolume)
    {
        CurrentVolume = currentVolume;
        Pos = pos;
    }
}
