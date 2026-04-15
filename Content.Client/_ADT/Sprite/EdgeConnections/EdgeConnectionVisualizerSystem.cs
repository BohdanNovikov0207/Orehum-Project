// SPDX-FileCopyrightText: 2026 Space Station 14 Contributors
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared._ADT.Sprite.EdgeConnections;
using Robust.Client.GameObjects;

namespace Content.Client._ADT.Sprite.EdgeConnections;

public sealed class EdgeConnectionVisualizerSystem : VisualizerSystem<EdgeConnectionComponent>
{
    protected override void OnAppearanceChange(EntityUid uid, EdgeConnectionComponent component, ref AppearanceChangeEvent args)
    {
        if (args.Sprite == null)
            return;

        AppearanceSystem.TryGetData<EdgeConnectionDirections>(uid, EdgeConnectionVisuals.ConnectionMask, out _, args.Component);
    }
}
