using Content.Shared._Orehum.Generic;
using Robust.Client.GameObjects;

namespace Content.Client._Orehum.Generic;

public sealed class FadeAndDeleteSystem : EntitySystem
{
    [Dependency] private readonly SpriteSystem _sprite = null!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<FadeAndDeleteComponent, ComponentInit>(OnInit);
    }

    private void OnInit(EntityUid uid, FadeAndDeleteComponent component, ComponentInit args)
    {
        if (TryComp(uid, out SpriteComponent? sprite))
            _sprite.SetColor((uid, sprite), component.Modulate with { A = component.ServerSideAlpha, }); // да щиткод
    }

    public override void Update(float frameTime)
    {
        var query = EntityManager.EntityQueryEnumerator<FadeAndDeleteComponent, SpriteComponent>();
        while (query.MoveNext(out var uid, out var fade, out var sprite))
        {
            fade.Accumulator += frameTime;
            while (fade.Accumulator >= 1f && sprite.Color.A >= 0f)
            {
                fade.Accumulator -= 1f;
                _sprite.SetColor((uid, sprite), fade.Modulate with { A = MathF.Max(0f, sprite.Color.A - fade.RemoveAlphaPerSecond), });
            }
        }
    }
}
