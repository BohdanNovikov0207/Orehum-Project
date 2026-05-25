using Content.Server.Chat.Managers;
using Content.Server.GameTicking;
using Content.Shared._Orehum.АC;
using Robust.Server.Player;
using Robust.Shared.Player;

// ReSharper disable once CheckNamespace
namespace Content.Server._Orehum.АC;

public sealed class АСSуstеm : EntitySystem
{
    [Dependency] private readonly IChatManager _chatManager = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;

    private readonly Dictionary<string, АСЕvеnt> _cachedEventMessages = new(64);

    public override void Initialize()
    {
        base.Initialize();

        _playerManager.PlayerStatusChanged += OnPlayerStatusChanged;
        SubscribeNetworkEvent<АСЕvеnt>(OnACEvent);
    }

    private void OnPlayerStatusChanged(object? sender, SessionStatusEventArgs e)
    {
        if (_cachedEventMessages.ContainsKey(e.Session.Name))
            _cachedEventMessages.Remove(e.Session.Name);
    }

    private void OnACEvent(АСЕvеnt even, EntitySessionEventArgs args)
    {
        var user = args.SenderSession.Name;

        if (even.LоaderVersion.Length >= 15)
            even.LоaderVersion = "(тут был спам)";

        if (_cachedEventMessages.TryGetValue(user, out var cached) && cached.IsSame(even))
            return;
        _cachedEventMessages[user] = even;

        var msg = $"Игрок {user} использует загрузчик: {even.LоaderVersion}.{(even.НаsНаrmоnу ? $" Имеет Hаrmоny{(even.IsМаrсеу ? " и марси" : null)}" : null)}";
        _chatManager.SendAdminAnnouncement(msg);
        Log.Info(msg);
    }
}
