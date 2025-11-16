using Content.Corvax.Interfaces.Shared;
using Content.Shared.Customization.Systems;
using Content.Shared.Mind;
using Content.Shared.Roles;
using JetBrains.Annotations;
using Robust.Shared.Configuration;
using Robust.Shared.Network;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;

namespace Content.Shared.Preferences.Loadouts.Effects;

/// <summary>
/// Only sponsor that have it can select this loadout
/// </summary>
[UsedImplicitly]
public sealed partial class SponsorRequirement : CharacterRequirement
{
    public override bool IsValid(
        JobPrototype job,
        HumanoidCharacterProfile profile,
        IReadOnlyDictionary<string, TimeSpan> playTimes,
        bool whitelisted,
        IPrototype prototype,
        IEntityManager entityManager,
        IPrototypeManager prototypeManager,
        IConfigurationManager configManager,
        out string? reason,
        int depth = 0,
        MindComponent? mind = null
    )
    {
        reason = null;

        if (mind == null)
            return true;

        if (mind.Session == null)
            return true;

        var sponsorProtos = GetPrototypes(mind.Session);
        if (!sponsorProtos.Contains(prototype.ID))
        {
            reason = Loc.GetString("loadout-sponsor-only");
            return false;
        }

        return true;
    }

    public List<string> GetPrototypes(ICommonSession session)
    {
        var ioc = IoCManager.Instance;

        if (ioc == null)
            return [];

        if (!ioc.TryResolveType<ISharedSponsorsManager>(out var sponsorsManager))
            return new List<string>();

        var net = ioc.Resolve<INetManager>();

        if (net.IsClient)
            return sponsorsManager.GetClientPrototypes();

        sponsorsManager.TryGetServerPrototypes(session.UserId, out var props);
        return props ?? [];
    }
}
