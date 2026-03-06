using Content.Shared._Orehum.Generic;

namespace Content.Server._Orehum.Generic;

public sealed class FadeAndDeleteSystem : EntitySystem
{
    public override void Update(float frameTime)
    {
        var query = EntityManager.EntityQueryEnumerator<FadeAndDeleteComponent>();
        while (query.MoveNext(out var uid, out var fade))
        {
            fade.Accumulator += frameTime;
            while (fade.Accumulator >= 1f)
            {
                fade.Accumulator -= 1f;
                fade.ServerSideAlpha -= fade.RemoveAlphaPerSecond;
                if (fade.ServerSideAlpha <= 0f)
                {
                    EntityManager.DeleteEntity(uid);
                }
            }
        }
    }
}
