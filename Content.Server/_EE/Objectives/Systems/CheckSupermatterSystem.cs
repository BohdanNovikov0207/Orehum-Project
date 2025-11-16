using Content.Shared._EE.Supermatter.Components;
using Content.Server.Station.Systems;

namespace Content.Server._EE.Objectives.Systems;

public sealed class CheckSupermatterSystem : EntitySystem
{
    [Dependency] private readonly StationSystem _stationSystem = default!;
    public bool SupermatterCheck()
    {
        var query = AllEntityQuery<SupermatterComponent, TransformComponent>();
        while (query.MoveNext(out var uid, out _, out var xform))
        {
            var station = _stationSystem.GetOwningStation(uid);
            if (station == EntityUid.Invalid)
                continue;
            return true;
        }
        return false;
    }
}
