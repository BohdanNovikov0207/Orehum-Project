using Content.Shared.Rejuvenate;

namespace Content.Server.Administration.Systems;

public sealed class RejuvenateSystem : EntitySystem
{
    public void PerformRejuvenate(EntityUid target)
    {
        var ev = new RejuvenateEvent();
        RaiseLocalEvent(target, ref ev);
    }
}
