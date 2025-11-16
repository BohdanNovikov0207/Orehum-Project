using Content.Server.Chat.Managers;
using Content.Shared._Orehum.AC;

namespace Content.Server._Orehum.AC;

public sealed class ACSystem : EntitySystem
{
    [Dependency] private readonly IChatManager _chatManager = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeNetworkEvent<ACEvent>(OnACEvent);
    }

    private void OnACEvent(ACEvent even, EntitySessionEventArgs args)
    {
        var version = even.Version != "unknown" ? $"v0.{even.Version}.0" : even.Version;
        var msg = $"Игрок {args.SenderSession.Name} использует лаунчер: {version}.{(string.IsNullOrWhiteSpace(even.Modifications) ? null : $"\nОбнаружены следы: {even.Modifications}")}";
        _chatManager.SendAdminAnnouncement(msg);
        Log.Info(msg);
    }
}
