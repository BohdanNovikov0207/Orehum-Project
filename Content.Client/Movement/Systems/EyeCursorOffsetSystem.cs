// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 SX-7 <92227810+SX-7@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 SlamBamActionman <83650252+SlamBamActionman@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Winkarst <74284083+Winkarst-cpu@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Numerics;
using Content.Client.Movement.Components;
using Content.Client.UserInterface.Controls;
using Content.Shared.Camera;
using Robust.Client.Graphics;
using Robust.Client.Input;
using Robust.Client.Player;
using Robust.Client.UserInterface;
using Robust.Shared.Map;
// Goob - grabbed wizden PR #35087; if proper version of that gets merged remove these comments and code

// Goob - grabbed wizden PR #35087

namespace Content.Client.Movement.Systems;

public sealed class EyeCursorOffsetSystem : EntitySystem
{
    // This value is here to make sure the user doesn't have to move their mouse
    // all the way out to the edge of the screen to get the full offset.
    private static readonly float _edgeOffset = 0.9f;
    [Dependency] private readonly IClyde _clyde = default!;
    [Dependency] private readonly IEyeManager _eyeManager = default!;
    [Dependency] private readonly IInputManager _inputManager = default!;
    [Dependency] private readonly IPlayerManager _player = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly IUserInterfaceManager _uiManager = default!; // Goob - grabbed wizden PR #35087

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<EyeCursorOffsetComponent, GetEyeOffsetEvent>(OnGetEyeOffsetEvent);
    }

    private void OnGetEyeOffsetEvent(EntityUid uid, EyeCursorOffsetComponent component, ref GetEyeOffsetEvent args)
    {
        var offset = OffsetAfterMouse(uid, component);
        if (offset == null)
            return;

        args.Offset += offset.Value;
    }

    public Vector2? OffsetAfterMouse(EntityUid uid, EyeCursorOffsetComponent? component)
    {
        // <Goob - wizden PR #35087>
        // We need the main viewport where the game content is displayed, as certain UI layouts (e.g. Separated Chat)
        // can make it a different size to the game window.
        if (_uiManager.ActiveScreen == null ||
            !_uiManager.ActiveScreen!.TryGetWidget<MainViewport>(out var mainViewport))
            return null;
        // </Goob - wizden PR #35087>
        var localPlayer = _player.LocalPlayer?.ControlledEntity;
        var mousePos = _inputManager.MouseScreenPosition;
        var screenSize = mainViewport.Size; // Goob - grabbed wizden PR #35087
        var minValue = MathF.Min(screenSize.X / 2, screenSize.Y / 2) * _edgeOffset;

        var mouseNormalizedPos = new Vector2(-(mousePos.X - screenSize.X / 2) / minValue,
            (mousePos.Y - screenSize.Y / 2) /
            minValue); // X needs to be inverted here for some reason, otherwise it ends up flipped.

        if (localPlayer == null)
            return null;

        var playerPos = _transform.GetWorldPosition(localPlayer.Value);

        if (component == null)
            component = EnsureComp<EyeCursorOffsetComponent>(uid);

        // Doesn't move the offset if the mouse has left the game window!
        if (mousePos.Window != WindowId.Invalid)
        {
            // The offset must account for the in-world rotation.
            var eyeRotation = _eyeManager.CurrentEye.Rotation;
            var mouseActualRelativePos = Vector2.Transform(mouseNormalizedPos,
                Quaternion.CreateFromAxisAngle(-Vector3.UnitZ,
                    (float) eyeRotation.Opposite().Theta)); // I don't know, it just works.

            // Caps the offset into a circle around the player.
            mouseActualRelativePos *= component.MaxOffset;
            if (mouseActualRelativePos.Length() > component.MaxOffset)
                mouseActualRelativePos = mouseActualRelativePos.Normalized() * component.MaxOffset;

            component.TargetPosition = mouseActualRelativePos;

            //Makes the view not jump immediately when moving the cursor fast.
            if (component.CurrentPosition != component.TargetPosition)
            {
                var vectorOffset = component.TargetPosition - component.CurrentPosition;
                if (vectorOffset.Length() > component.OffsetSpeed)
                    vectorOffset = vectorOffset.Normalized() * component.OffsetSpeed;
                component.CurrentPosition += vectorOffset;
            }
        }

        return component.CurrentPosition;
    }
}
