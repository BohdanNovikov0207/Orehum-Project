using Content.Corvax.Interfaces.Shared;
using Content.Shared.Ghost;
using Robust.Shared.Player;
using Content.Shared._Orehum.Sponsors;

namespace Content.Server._Orehum.Sponsors;

public sealed class SponsorSystem : EntitySystem
{
    [Dependency] private readonly ISharedSponsorsManager _sponsorsMgr = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PlayerAttachedEvent>(OnPlayerAttached);
    }

    private void OnPlayerAttached(PlayerAttachedEvent args)
    {
        var uid = args.Entity;

        if (HasComp<GhostComponent>(uid))
            return;

        if (!_sponsorsMgr.TryGetTier(args.Player.UserId, out var tier))
            return;

        if (tier <= 3)
            return;

        EnsureComp<SponsorComponent>(uid);
    }
}
