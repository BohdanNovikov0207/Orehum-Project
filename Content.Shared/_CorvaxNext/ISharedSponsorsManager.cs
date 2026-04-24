using System.Diagnostics.CodeAnalysis;
using Robust.Shared.Network;

namespace Content.Corvax.Interfaces.Shared;

public interface ISharedSponsorsManager
{
    void Initialize();

    // Client
    virtual List<string> GetClientPrototypes() => new();

    // Server
    virtual bool TryGetServerPrototypes(NetUserId userId, [NotNullWhen(true)] out List<string>? prototypes) =>
        throw new NotImplementedException();

    virtual bool TryGetServerOocColor(NetUserId userId, [NotNullWhen(true)] out Color? color) =>
        throw new NotImplementedException();

    virtual int GetServerExtraCharSlots(NetUserId userId) => throw new NotImplementedException();

    virtual bool HaveServerPriorityJoin(NetUserId userId) => throw new NotImplementedException();

    // backmen
    void Cleanup();

    virtual bool TryGetGhostTheme(NetUserId userId, [NotNullWhen(true)] out string? ghostTheme) =>
        throw new NotImplementedException();

    bool TryGetLoadouts(NetUserId userId, [NotNullWhen(true)] out List<string>? prototypes) =>
        throw new NotImplementedException();

    List<string> GetClientLoadouts() => throw new NotImplementedException();

    bool IsClientAllRoles() => throw new NotImplementedException();

    bool IsServerAllRoles(NetUserId userId) => throw new NotImplementedException();
}
