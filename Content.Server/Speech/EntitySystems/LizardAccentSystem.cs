// SPDX-FileCopyrightText: 2022 Alex Evgrashin <aevgrashin@yandex.ru>
// SPDX-FileCopyrightText: 2024 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Content.Shared.Speech;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems;

public sealed class LizardAccentSystem : EntitySystem
{
    private static readonly Regex RegexLowerS = new("s+");
    private static readonly Regex RegexUpperS = new("S+");
    private static readonly Regex RegexInternalX = new(@"(\w)x");
    private static readonly Regex RegexLowerEndX = new(@"\bx([\-|r|R]|\b)");
    private static readonly Regex RegexUpperEndX = new(@"\bX([\-|r|R]|\b)");

    // Corvax-Localization-Start
    private static readonly Regex _regexLowerC = new("с+");
    private static readonly Regex _regexUpperC = new("С+");
    private static readonly Regex _regexLowerZ = new("з+");
    private static readonly Regex _regexUpperZ = new("З+");
    private static readonly Regex _regexLowerSh = new("ш+");
    private static readonly Regex _regexUpperSh = new("Ш+");
    private static readonly Regex _regexLowerCh = new("ч+");
    private static readonly Regex _regexUpperCh = new("Ч+");
    private static readonly List<string> _replacementsSs = new() { "сс", "ссс" };
    private static readonly List<string> _replacementsSsUpper = new() { "СС", "ССС" };
    private static readonly List<string> _replacementsSh = new() { "шш", "шшш" };
    private static readonly List<string> _replacementsShUpper = new() { "ШШ", "ШШШ" };
    private static readonly List<string> _replacementsCh = new() { "щщ", "щщщ" };

    private static readonly List<string> _replacementsChUpper = new() { "ЩЩ", "ЩЩЩ" };
    // Corvax-Localization-End

    [Dependency] private readonly IRobustRandom _random = default!; // Corvax-Localization

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<LizardAccentComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, LizardAccentComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // hissss
        message = RegexLowerS.Replace(message, "sss");
        // hiSSS
        message = RegexUpperS.Replace(message, "SSS");
        // ekssit
        message = RegexInternalX.Replace(message, "$1kss");
        // ecks
        message = RegexLowerEndX.Replace(message, "ecks$1");
        // eckS
        message = RegexUpperEndX.Replace(message, "ECKS$1");

        // Corvax-Localization-Start
        message = _regexLowerC.Replace(message, _random.Pick(_replacementsSs));
        message = _regexUpperC.Replace(message, _random.Pick(_replacementsSsUpper));
        message = _regexLowerZ.Replace(message,
            _random.Pick(_replacementsSs)); // для "з+" используются те же замены, что и для "с+"
        message = _regexUpperZ.Replace(message,
            _random.Pick(_replacementsSsUpper)); // для "З+" используются те же замены, что и для "С+"
        message = _regexLowerSh.Replace(message, _random.Pick(_replacementsSh));
        message = _regexUpperSh.Replace(message, _random.Pick(_replacementsShUpper));
        message = _regexLowerCh.Replace(message, _random.Pick(_replacementsCh));
        message = _regexUpperCh.Replace(message, _random.Pick(_replacementsChUpper));
        // Corvax-Localization-End
        args.Message = message;
    }
}
