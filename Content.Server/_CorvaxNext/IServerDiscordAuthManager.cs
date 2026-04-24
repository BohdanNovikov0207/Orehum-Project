using System.Threading;
using System.Threading.Tasks;
using Content.Corvax.Interfaces.Shared;
using Robust.Shared.Network;
using Robust.Shared.Player;
//using Content.Server.Backmen.DiscordAuth;

namespace Content.Corvax.Interfaces.Server;

public interface IServerDiscordAuthManager : ISharedDiscordAuthManager
{
    event EventHandler<ICommonSession>? PlayerVerified;

    //public Task<DiscordAuthManager.DiscordGenerateLinkResponse> GenerateAuthLink(NetUserId userId, CancellationToken cancel);
    Task<bool> IsVerified(NetUserId userId, CancellationToken cancel);
    bool IsCached(ICommonSession user);
}
