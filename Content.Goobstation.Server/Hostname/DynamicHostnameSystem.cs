// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Ted Lukin <66275205+pheenty@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 coderabbitai[bot] <136622811+coderabbitai[bot]@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 pheenty <fedorlukin2006@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Common.CCVar;
using Content.Goobstation.Common.JoinQueue;
using Content.Shared.Dataset;
using Content.Shared.Random.Helpers;
using Robust.Shared;
using Robust.Shared.Configuration;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Goobstation.Server.Hostname;

/// <summary>
/// This handles dynamically updating hostnames.
/// </summary>
public sealed class DynamicHostnameSystem : EntitySystem
{
    private static readonly ProtoId<LocalizedDatasetPrototype> _messagesProto = "MessageOfTheDay";
    [Dependency] private readonly IConfigurationManager _configuration = default!;
    [Dependency] private readonly IGameTiming _gameTiming = default!;
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly IJoinQueueManager _queue = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    private bool _dynHostEnabled;
    private LocalizedDatasetPrototype? _messages;
    private TimeSpan _nextUpdateTime;
    private string _originalHostname = string.Empty;
    private TimeSpan _updateInterval = TimeSpan.FromSeconds(10);

    public override void Initialize()
    {
        base.Initialize();

        _originalHostname = _configuration.GetCVar(CVars.GameHostName);
        Subs.CVar(_configuration, GoobCVars.UseDynamicHostname, OnDynHostChange, true);
        Subs.CVar(_configuration, CVars.HubAdvertiseInterval, OnHubAdIntChange, true);
        _nextUpdateTime = _gameTiming.CurTime + _updateInterval;
        _messages = _proto.Index(_messagesProto);
    }

    private void OnHubAdIntChange(int newValue) => _updateInterval = TimeSpan.FromSeconds(newValue);

    private void OnDynHostChange(bool newValue)
    {
        _dynHostEnabled = newValue;
        if (!_dynHostEnabled)
            _configuration.SetCVar(CVars.GameHostName, _originalHostname);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        if (!_dynHostEnabled || _nextUpdateTime > _gameTiming.CurTime)
            return;

        _nextUpdateTime = _gameTiming.CurTime + _updateInterval;
        UpdateHostname();
    }

    private void UpdateHostname()
    {
        var hostname = _originalHostname;

        if (_queue.PlayerInQueueCount > 0)
            hostname += " | Queue: " + _queue.PlayerInQueueCount + " players";

        if (_messages != null && _messages.Values.Count > 0)
            hostname += " | " + _random.Pick(_messages);

        _configuration.SetCVar(CVars.GameHostName, hostname);
    }
}
