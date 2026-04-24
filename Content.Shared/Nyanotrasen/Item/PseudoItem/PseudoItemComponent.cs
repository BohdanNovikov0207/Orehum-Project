// SPDX-FileCopyrightText: 2024 Aidenkrz <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Item;
using Robust.Shared.Prototypes;

namespace Content.Shared.Nyanotrasen.Item.PseudoItem;

/// <summary>
/// For entities that behave like an item under certain conditions,
/// but not under most conditions.
/// </summary>
[RegisterComponent] [AutoGenerateComponentState]
public sealed partial class PseudoItemComponent : Component
{
    public bool Active = false;

    /// <summary>
    /// An optional override for the shape of the item within the grid storage.
    /// If null, a default shape will be used based on <see cref="Size" />.
    /// </summary>
    [DataField] [AutoNetworkedField]
    public List<Box2i>? Shape;

    [DataField("size")]
    public ProtoId<ItemSizePrototype> Size = "Huge";

    /// <summary>
    /// Action for sleeping while inside a container with <see cref="AllowsSleepInsideComponent" />.
    /// </summary>
    [DataField]
    public EntityUid? SleepAction;

    [DataField] [AutoNetworkedField]
    public Vector2i StoredOffset;
}
