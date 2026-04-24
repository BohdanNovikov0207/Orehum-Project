// SPDX-FileCopyrightText: 2025 deltanedas <@deltanedas:kde.org>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Storage;
using Content.Shared.Storage.EntitySystems;

namespace Content.Goobstation.Shared.Factory.Slots;

/// <summary>
/// Abstraction over a <see cref="StorageComponent" /> grid inventory.
/// </summary>
public sealed partial class AutomatedStorage : AutomationSlot
{
    private StorageComponent _comp;
    private SharedStorageSystem _storage;

    public override void Initialize()
    {
        base.Initialize();

        _storage = EntMan.System<SharedStorageSystem>();
        _comp = EntMan.GetComponent<StorageComponent>(Owner);
    }

    public override bool Insert(EntityUid item) =>
        base.Insert(item) &&
        _storage.Insert(Owner, item, out _, storageComp: _comp);

    public override bool CanInsert(EntityUid item) =>
        base.CanInsert(item) &&
        _storage.CanInsert(Owner, item, out _, _comp);

    public override EntityUid? GetItem(EntityUid? filter)
    {
        foreach (var item in _comp.Container.ContainedEntities)
        {
            if (_filter.IsAllowed(filter, item))
                return item;
        }

        return null;
    }
}
