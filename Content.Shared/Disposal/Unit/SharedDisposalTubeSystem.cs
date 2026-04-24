using Content.Shared.Disposal.Components;
using Content.Shared.Disposal.Tube;

namespace Content.Shared.Disposal.Unit;

public abstract class SharedDisposalTubeSystem : EntitySystem
{
    public virtual bool TryInsert(EntityUid uid,
        DisposalUnitComponent from,
        IEnumerable<string>? tags = default,
        DisposalEntryComponent? entry = null) =>
        false;
}
