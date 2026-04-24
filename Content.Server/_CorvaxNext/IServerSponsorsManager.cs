using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Content.Corvax.Interfaces.Shared;
using Robust.Shared.Network;

namespace Content.Corvax.Interfaces.Server;

public interface IServerSponsorsManager : ISharedSponsorsManager
{
    bool TryGetGhostTheme(NetUserId userId, [NotNullWhen(true)] out string? ghostTheme);
    bool TryGetPrototypes(NetUserId userId, [NotNullWhen(true)] out List<string>? prototypes);
    bool TryGetOocColor(NetUserId userId, [NotNullWhen(true)] out Color? color);
    int GetExtraCharSlots(NetUserId userId);
    bool HavePriorityJoin(NetUserId userId);
    void Cleanup();
}

public interface IServerVPNGuardManager
{
    void Initialize();
    Task<bool> IsConnectionVpn(IPAddress ip);
}
