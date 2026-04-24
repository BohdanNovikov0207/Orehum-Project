using Content.Goobstation.Shared.Factory.Filters;
using Content.Shared.DeviceLinking;
using Robust.Shared.Containers;

namespace Content.Goobstation.Shared.Factory;

public sealed class StorageBinSystem : EntitySystem
{
    public const string ContainerId = "storagebase";
    [Dependency] private readonly SharedDeviceLinkSystem _device = default!;
    [Dependency] private readonly AutomationFilterSystem _filter = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<StorageBinComponent, ContainerIsInsertingAttemptEvent>(OnInsertAttempt);
        SubscribeLocalEvent<StorageBinComponent, EntInsertedIntoContainerMessage>(OnEntInserted);
        SubscribeLocalEvent<StorageBinComponent, EntRemovedFromContainerMessage>(OnEntRemoved);
    }

    private void OnInsertAttempt(Entity<StorageBinComponent> ent, ref ContainerIsInsertingAttemptEvent args)
    {
        if (args.Container.ID != ContainerId)
            return;

        if (_filter.IsBlocked(_filter.GetSlot(ent), args.EntityUid))
            args.Cancel();
    }

    private void OnEntInserted(Entity<StorageBinComponent> ent, ref EntInsertedIntoContainerMessage args)
    {
        if (args.Container.ID != ContainerId)
            return;

        _device.InvokePort(ent, ent.Comp.InsertedPort);
    }

    private void OnEntRemoved(Entity<StorageBinComponent> ent, ref EntRemovedFromContainerMessage args)
    {
        if (args.Container.ID != ContainerId)
            return;

        _device.InvokePort(ent, ent.Comp.RemovedPort);
    }
}
