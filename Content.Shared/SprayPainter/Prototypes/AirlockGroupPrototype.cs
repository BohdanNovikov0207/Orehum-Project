// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 c4llv07e <38111072+c4llv07e@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Prototypes;

namespace Content.Shared.SprayPainter.Prototypes;

[Prototype("AirlockGroup")]
public sealed class AirlockGroupPrototype : IPrototype
{
    // The priority determines, which sprite is used when showing
    // the icon for a style in the SprayPainter UI. The highest priority
    // gets shown.
    [DataField("iconPriority")]
    public int IconPriority = 0;

    [DataField("stylePaths")]
    public Dictionary<string, string> StylePaths = default!;

    [IdDataField]
    public string ID { get; } = default!;
}
