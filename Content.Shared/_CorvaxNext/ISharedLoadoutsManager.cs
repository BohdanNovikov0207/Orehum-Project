using System.Diagnostics.CodeAnalysis;
using Content.Shared.Preferences.Loadouts;
using Robust.Shared.Network;

namespace Content.Corvax.Interfaces.Shared;

public interface ISharedLoadoutsManager
{
    void Initialize();

    bool TryGetServerPrototypes(NetUserId userId, [NotNullWhen(true)] out List<string>? prototypes) =>
        throw new NotImplementedException();

    List<string> GetClientPrototypes() => throw new NotImplementedException();

    List<LoadoutPrototype> GetClientLoadoutPrototypes() => throw new NotImplementedException();
}
