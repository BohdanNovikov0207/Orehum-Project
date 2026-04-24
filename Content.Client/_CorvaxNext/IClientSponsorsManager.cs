using Content.Corvax.Interfaces.Shared;

namespace Content.Corvax.Interfaces.Client;

public interface IClientSponsorsManager : ISharedSponsorsManager
{
    HashSet<string> Prototypes { get; }
    int Tier { get; }
    bool Whitelisted { get; }
}
