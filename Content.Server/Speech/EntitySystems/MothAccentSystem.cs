// SPDX-FileCopyrightText: 2023 lzk <124214523+lzk228@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 router <messagebus@vk.com>
// SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Content.Shared.Speech;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems;

public sealed class MothAccentSystem : EntitySystem
{
    private static readonly Regex RegexLowerBuzz = new("z{1,3}");
    private static readonly Regex RegexUpperBuzz = new("Z{1,3}");

    // Corvax-Localization-Start
    private static readonly Regex _regexLowerZh = new("ж+");
    private static readonly Regex _regexUpperZh = new("Ж+");
    private static readonly Regex _regexLowerZ = new("з+");
    private static readonly Regex _regexUpperZ = new("З+");

    private static readonly List<string> _replacementsZh = new() { "жж", "жжж" };
    private static readonly List<string> _replacementsZhUpper = new() { "ЖЖ", "ЖЖЖ" };
    private static readonly List<string> _replacementsZ = new() { "зз", "ззз" };
    private static readonly List<string> _replacementsZUpper = new() { "ЗЗ", "ЗЗЗ" };

    [Dependency] private readonly IRobustRandom _random = default!; // Corvax-Localization
    // Corvax-Localization-End

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<MothAccentComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, MothAccentComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // buzzz
        message = RegexLowerBuzz.Replace(message, "zzz");
        // buZZZ
        message = RegexUpperBuzz.Replace(message, "ZZZ");

        // Corvax-Localization-Start
        message = _regexLowerZh.Replace(message, _random.Pick(_replacementsZh));
        message = _regexUpperZh.Replace(message, _random.Pick(_replacementsZhUpper));
        message = _regexLowerZ.Replace(message, _random.Pick(_replacementsZ)); // используем существующий regexLowerZ
        message = _regexUpperZ.Replace(message,
            _random.Pick(_replacementsZUpper)); // используем существующий regexUpperZ
        // Corvax-Localization-End

        args.Message = message;
    }
}
