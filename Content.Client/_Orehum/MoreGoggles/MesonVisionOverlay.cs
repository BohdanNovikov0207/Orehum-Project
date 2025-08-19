using System.Numerics;
using Content.Shared._Orehum.MoreGoggles;
using Content.Shared.Tag;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Shared.Enums;
using Robust.Shared.Prototypes;

namespace Content.Client._Orehum.MoreGoggles;

public sealed class MesonVisionOverlay : Overlay
{
    private static readonly List<ProtoId<TagPrototype>> VisibleStructures;

    static MesonVisionOverlay()
    {
        VisibleStructures = new();
        VisibleStructures.Add("Wall");
        VisibleStructures.Add("Window");
        VisibleStructures.Add("Airlock");
        VisibleStructures.Add("GlassAirlock");
    }

    [Dependency] private readonly IEntityManager _entity = default!;
    [Dependency] private readonly IPlayerManager _player = default!;

    private readonly TransformSystem _transform;
    private readonly SpriteSystem _sprite;
    private readonly TagSystem _tags;

    public override bool RequestScreenTexture => true;

    public override OverlaySpace Space => OverlaySpace.WorldSpace;

    public MesonVisionComponent? Comp;

    public MesonVisionOverlay()
    {
        IoCManager.InjectDependencies(this);

        _transform = _entity.System<TransformSystem>();
        _sprite = _entity.System<SpriteSystem>();
        _tags = _entity.System<TagSystem>();

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

        // да спаси робаст этот неоптимизированный кусок кода
        var entities = _entity.EntityQueryEnumerator<TagComponent, SpriteComponent, TransformComponent>();
        while (entities.MoveNext(out var uid, out var tag, out var sprite, out var xform))
        {
            if (xform.MapID != mapId)
                continue;

            if (!_tags.HasAnyTag(tag, VisibleStructures))
                continue;

            var position = _transform.GetWorldPosition(xform);
            var rotation = _transform.GetWorldRotation(xform);

            var originalColor = sprite.Color;
            Entity<SpriteComponent> ent = (uid, sprite);
            var entNullable = ent.AsNullable();
            _sprite.SetColor(entNullable, originalColor.WithAlpha(0.25f));
            _sprite.RenderSprite(ent, worldHandle, eyeRot, rotation, position);
            _sprite.SetColor(entNullable, originalColor);
        }

        worldHandle.SetTransform(Matrix3x2.Identity);
    }
}
