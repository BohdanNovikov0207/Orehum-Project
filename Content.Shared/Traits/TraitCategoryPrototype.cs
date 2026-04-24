// SPDX-FileCopyrightText: 2024 Ed <96445749+TheShuEd@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;

namespace Content.Shared.Traits;

/// <summary>
/// Traits category with general settings. Allows you to limit the number of taken traits in one category
/// </summary>
[Prototype]
public sealed class TraitCategoryPrototype : IPrototype
{
    public const string Default = "Default";

    /// <summary>
    /// The maximum number of traits that can be taken in this category.
    /// </summary>
    [DataField]
    public int? MaxTraitPoints;

    /// <summary>
    /// Name of the trait category displayed in the UI
    /// </summary>
    [DataField]
    public LocId Name { get; private set; } = string.Empty;

    [ViewVariables]
    [IdDataField]
    public string ID { get; } = default!;
}
