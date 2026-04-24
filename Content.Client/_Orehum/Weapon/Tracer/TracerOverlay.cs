// SPDX-FileCopyrightText: 2026 Space Station 14 Contributors
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Client._Orehum.Weapon.Tracer.Systems;
using Robust.Client.Graphics;
using Robust.Shared.Enums;

namespace Content.Client._Orehum.Weapon.Tracer;

public sealed class TracerOverlay : Overlay
{
    private readonly TracerSystem _tracer;

    public TracerOverlay(TracerSystem tracer)
    {
        _tracer = tracer;
    }

    public override OverlaySpace Space => OverlaySpace.WorldSpaceEntities;

    protected override void Draw(in OverlayDrawArgs args) => _tracer.Draw(args.WorldHandle, args.MapId);
}
