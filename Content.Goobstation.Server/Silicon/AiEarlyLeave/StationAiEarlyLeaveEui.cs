using Content.Goobstation.Shared.Silicons;
using Content.Server.EUI;
using Content.Shared.Eui;
using Content.Shared.Silicons.StationAi;
using Robust.Shared.Network;

namespace Content.Goobstation.Server.Silicons;

public sealed class StationAiEarlyLeaveEui : BaseEui
{
    private readonly EntityUid _ai;
    private readonly Entity<StationAiCoreComponent> _aiCore;
    private readonly StationAiEarlyLeaveSystem _leaveSystem;
    private readonly NetUserId _userId;

    public StationAiEarlyLeaveEui(Entity<StationAiCoreComponent> aiCore,
        EntityUid ai,
        NetUserId userId,
        StationAiEarlyLeaveSystem leaveSystem)
    {
        _aiCore = aiCore;
        _ai = ai;
        _userId = userId;
        _leaveSystem = leaveSystem;
    }

    public override void HandleMessage(EuiMessageBase msg)
    {
        base.HandleMessage(msg);

        if (msg is not StationAiEarlyLeaveMessage choice ||
            !choice.Confirmed)
        {
            Close();
            return;
        }

        _leaveSystem.EarlyLeave(_aiCore, _ai, _userId);

        Close();
    }
}
