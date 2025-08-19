using System.Numerics;
using Content.Shared._Orehum.MoreGoggles;
using Content.Shared.Mining.Components;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Shared.Enums;

namespace Content.Client._Orehum.MoreGoggles;

public sealed class MinerVisionOverlay : Overlay
{
    [Dependency] private readonly IEntityManager _entity = default!;
    [Dependency] private readonly IPlayerManager _player = default!;

    private readonly TransformSystem _transform;
    private readonly SpriteSystem _sprite;

    public override bool RequestScreenTexture => true;

    public override OverlaySpace Space => OverlaySpace.WorldSpace;

    public MinerVisionComponent? Comp;

    public MinerVisionOverlay()
    {
        IoCManager.InjectDependencies(this);

        _transform = _entity.System<TransformSystem>();
        _sprite = _entity.System<SpriteSystem>();

        ZIndex = -1;
    }

    protected override void Draw(in OverlayDrawArgs args)
    {
        if (ScreenTexture is null || Comp is null)
            return;

        var worldHandle = args.WorldHandle;
        var eye = args.Viewport.Eye;

        if (eye == null)
            return;

        var player = _player.LocalEntity;

        if (!_entity.TryGetComponent(player, out TransformComponent? playerXform))
            return;

        var mapId = eye.Position.MapId;
        var eyeRot = eye.Rotation;

        var accumulator = Math.Clamp(Comp.PulseAccumulator, 0f, Comp.PulseTime);
        var alpha = Comp.PulseTime <= 0f ? 1f : float.Lerp(1f, 0f, accumulator / Comp.PulseTime);

        var entities = _entity.EntityQueryEnumerator<OreVeinComponent, SpriteComponent, TransformComponent>();
        while (entities.MoveNext(out var uid, out _, out var sprite, out var xform))
        {
            if (xform.MapID != mapId)
                continue;

            var position = _transform.GetWorldPosition(xform);
            var rotation = _transform.GetWorldRotation(xform);

            var originalColor = sprite.Color;
            Entity<SpriteComponent> ent = (uid, sprite);
            var entNullable = ent.AsNullable();
            _sprite.SetColor(entNullable, originalColor.WithAlpha(alpha));
            _sprite.RenderSprite(ent, worldHandle, eyeRot, rotation, position);
            _sprite.SetColor(entNullable, originalColor);
        }

        worldHandle.SetTransform(Matrix3x2.Identity);
    }
}
