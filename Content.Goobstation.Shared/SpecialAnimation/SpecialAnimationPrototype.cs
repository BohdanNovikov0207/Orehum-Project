// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Roudenn <romabond091@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.SpecialAnimation;

/// <summary>
/// Prototype for custom SpecialAnimationData.
/// </summary>
[Prototype]
public sealed class SpecialAnimationPrototype : IPrototype
{
    [DataField]
    public SpecialAnimationData Animation = SpecialAnimationData.DefaultAnimation;

    /// <inheritdoc />
    [IdDataField]
    public string ID { get; } = default!;
}
