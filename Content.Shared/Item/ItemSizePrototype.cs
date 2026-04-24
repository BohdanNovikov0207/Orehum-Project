// SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Tayrtahn <tayrtahn@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;

namespace Content.Shared.Item;

/// <summary>
/// This is a prototype for a category of an item's size.
/// </summary>
[Prototype]
public sealed class ItemSizePrototype : IPrototype, IComparable<ItemSizePrototype>
{
    /// <summary>
    /// The default inventory shape associated with this item size.
    /// </summary>
    [DataField(required: true)]
    public IReadOnlyList<Box2i> DefaultShape = new List<Box2i>();

    /// <summary>
    /// A player-facing name used to describe this size.
    /// </summary>
    [DataField]
    public LocId Name;

    /// <summary>
    /// The amount of space in a bag an item of this size takes.
    /// </summary>
    [DataField]
    public int Weight = 1;

    public int CompareTo(ItemSizePrototype? other)
    {
        if (other is not { } otherItemSize)
            return 0;
        return Weight.CompareTo(otherItemSize.Weight);
    }

    /// <inheritdoc />
    [IdDataField]
    public string ID { get; } = default!;

    public static bool operator <(ItemSizePrototype a, ItemSizePrototype b) => a.Weight < b.Weight;

    public static bool operator >(ItemSizePrototype a, ItemSizePrototype b) => a.Weight > b.Weight;

    public static bool operator <=(ItemSizePrototype a, ItemSizePrototype b) => a.Weight <= b.Weight;

    public static bool operator >=(ItemSizePrototype a, ItemSizePrototype b) => a.Weight >= b.Weight;
}
