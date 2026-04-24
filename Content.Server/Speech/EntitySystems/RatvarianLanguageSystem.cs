// SPDX-FileCopyrightText: 2023 Vordenburg <114301317+Vordenburg@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 keronshb <54602815+keronshb@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using System.Text;
using System.Text.RegularExpressions;
using Content.Shared.Speech;
using Content.Shared.Speech.Components;
using Content.Shared.Speech.EntitySystems;
using Content.Shared.StatusEffect;
using Robust.Shared.Prototypes;

namespace Content.Server.Speech.EntitySystems;

public sealed class RatvarianLanguageSystem : SharedRatvarianLanguageSystem
{
    private static readonly ProtoId<StatusEffectPrototype> RatvarianKey = "RatvarianLanguage";

    // This is the word of Ratvar and those who speak it shall abide by His rules:
    /*
     * Any time the word "of" occurs, it's linked to the previous word by a hyphen: "I am-of Ratvar"
     * Any time "th", followed by any two letters occurs, you add a grave (`) between those two letters: "Thi`s"
     * In the same vein, any time "ti" followed by one letter occurs, you add a grave (`) between "i" and the letter: "Ti`me"
     * Wherever "te" or "et" appear and there is another letter next to the "e", add a hyphen between "e" and the letter: "M-etal/Greate-r"
     * Where "gua" appears, add a hyphen between "gu" and "a": "Gu-ard"
     * Where the word "and" appears it's linked to all surrounding words by hyphens: "Sword-and-shield"
     * Where the word "to" appears, it's linked to the following word by a hyphen: "to-use"
     * Where the word "my" appears, it's linked to the following word by a hyphen: "my-light"
     * Any Ratvarian proper noun is not translated: Ratvar, Nezbere, Sevtug, Nzcrentr and Inath-neq
     * This only applies if they're being used as a proper noun: armorer/Nezbere
     */

    private static readonly Regex THPattern = new(@"th\w\B", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex ETPattern = new(@"\Bet", RegexOptions.Compiled);
    private static readonly Regex TEPattern = new(@"te\B", RegexOptions.Compiled);
    private static readonly Regex OFPattern = new(@"(\s)(of)");
    private static readonly Regex TIPattern = new(@"ti\B", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex GUAPattern = new(@"(gu)(a)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex ANDPattern = new(@"\b(\s)(and)(\s)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex TOMYPattern = new(@"(to|my)\s", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static readonly Regex ProperNouns = new(@"(ratvar)|(nezbere)|(sevtuq)|(nzcrentr)|(inath-neq)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    [Dependency] private readonly StatusEffectsSystem _statusEffects = default!;

    public override void Initialize() =>
        // Activate before other modifications so translation works properly
        SubscribeLocalEvent<RatvarianLanguageComponent, AccentGetEvent>(OnAccent,
            new[] { typeof(SharedSlurredSystem), typeof(SharedStutteringSystem) });

    public override void DoRatvarian(EntityUid uid, TimeSpan time, bool refresh, StatusEffectsComponent? status = null)
    {
        if (!Resolve(uid, ref status, false))
            return;

        _statusEffects.TryAddStatusEffect<RatvarianLanguageComponent>(uid, RatvarianKey, time, refresh, status);
    }

    private void OnAccent(EntityUid uid, RatvarianLanguageComponent component, AccentGetEvent args) =>
        args.Message = Translate(args.Message);

    private string Translate(string message)
    {
        var ruleTranslation = message;
        var finalMessage = new StringBuilder();
        var newWord = new StringBuilder();

        ruleTranslation = THPattern.Replace(ruleTranslation, "$&`");
        ruleTranslation = TEPattern.Replace(ruleTranslation, "$&-");
        ruleTranslation = ETPattern.Replace(ruleTranslation, "-$&");
        ruleTranslation = OFPattern.Replace(ruleTranslation, "-$2");
        ruleTranslation = TIPattern.Replace(ruleTranslation, "$&`");
        ruleTranslation = GUAPattern.Replace(ruleTranslation, "$1-$2");
        ruleTranslation = ANDPattern.Replace(ruleTranslation, "-$2-");
        ruleTranslation = TOMYPattern.Replace(ruleTranslation, "$1-");

        var temp = ruleTranslation.Split(' ');

        foreach (var word in temp)
        {
            newWord.Clear();

            if (ProperNouns.IsMatch(word))
                newWord.Append(word);

            else
            {
                for (var i = 0; i < word.Length; i++)
                {
                    var letter = word[i];

                    if (letter >= 97 && letter <= 122)
                    {
                        var letterRot = letter + 13;

                        if (letterRot > 122)
                            letterRot -= 26;

                        newWord.Append((char) letterRot);
                    }
                    else if (letter >= 65 && letter <= 90)
                    {
                        var letterRot = letter + 13;

                        if (letterRot > 90)
                            letterRot -= 26;

                        newWord.Append((char) letterRot);
                    }
                    else
                        newWord.Append(word[i]);
                }
            }

            finalMessage.Append(newWord + " ");
        }

        return finalMessage.ToString().Trim();
    }
}
