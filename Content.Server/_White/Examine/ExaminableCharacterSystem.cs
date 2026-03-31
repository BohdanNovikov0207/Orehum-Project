// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Chat.Managers;
using Content.Goobstation.Common.Examine; // Goobstation Change
using Content.Goobstation.Common.CCVar; // Goobstation Change
using Content.Shared.Chat;
using Content.Shared.Examine;
using Content.Shared.IdentityManagement;
using Content.Shared.Inventory;
using Robust.Shared.Configuration;
using Robust.Shared.Player;
using Robust.Shared.Utility;
using System.Globalization;
using Content.Trauma.Common.Heretic;

namespace Content.Server._White.Examine;
public sealed class ExaminableCharacterSystem : EntitySystem
{
    [Dependency] private readonly InventorySystem _inventorySystem = default!;
    [Dependency] private readonly IdentitySystem _identitySystem = default!;
    [Dependency] private readonly IChatManager _chatManager = default!;
    [Dependency] private readonly INetConfigurationManager _netConfigManager = default!;

    private List<string> _logLines = new();

    public override void Initialize()
    {
        SubscribeLocalEvent<ExaminableCharacterComponent, ExaminedEvent>(HandleExamine);
        SubscribeLocalEvent<MetaDataComponent, ExamineCompletedEvent>(HandleExamine);
    }

    private void HandleExamine(EntityUid uid, ExaminableCharacterComponent comp, ExaminedEvent args)
    {
        if (!TryComp<ActorComponent>(args.Examiner, out var actorComponent)
            || !args.IsInDetailsRange)
            return;

        var showExamine = _netConfigManager.GetClientCVar(actorComponent.PlayerSession.Channel, GoobCVars.DetailedExamine);

        var selfaware = args.Examiner == args.Examined;

        var priority = 13;

        FormattedMessage message = new();
        message.PushTag(new MarkupNode("examineborder", null, null)); // border
        message.PushNewline();
        message.PushNewline();
        AddLine(message);
        foreach (var line in _logLines)
        {
            message.AddMarkupPermissive(line);
            message.PushNewline();
        }
        AddLine(message);
        message.Pop();

    }

    private void HandleExamine(EntityUid uid, MetaDataComponent metaData, ExamineCompletedEvent args)
    {
        if (HasComp<ExaminableCharacterComponent>(args.Examined)
            && !args.IsSecondaryInfo)
            return;

        if (TryComp<ActorComponent>(args.Examiner, out var actorComponent)
            && _netConfigManager.GetClientCVar(actorComponent.PlayerSession.Channel, GoobCVars.DetailedExamine)
            && _netConfigManager.GetClientCVar(actorComponent.PlayerSession.Channel, GoobCVars.LogInChat))
        {
            FormattedMessage message = new();
            message.PushTag(new MarkupNode("examineborder", null, null)); // border
            message.PushNewline();
            message.Pop();

            if (!args.IsSecondaryInfo)
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                var name = textInfo.ToTitleCase(metaData.EntityName);
                name = FormattedMessage.EscapeText(name);
                var item = Loc.GetString("examine-present-tex", ("name", name), ("id", GetNetEntity(uid, metaData).Id), ("size", 14));
                message.AddMarkupPermissive($"[color=DarkGray][font size=11]{item}[/font][/color]");
                message.PushNewline();
            }
            AddLine(message);
            message.AddMarkupPermissive($"[font size=10]{args.Message.ToMarkup()}[/font]");
            message.PushNewline();
            AddLine(message);
            message.Pop();

        }
    }

    private void AddLine(FormattedMessage message)
    {
        message.PushColor(Color.FromHex("#282D31"));
        message.AddText(Loc.GetString("examine-border-line"));
        message.PushNewline();
        message.Pop();
    }
}
