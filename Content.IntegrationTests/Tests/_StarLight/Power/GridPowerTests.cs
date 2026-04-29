using System.Collections.Generic;
using System.Linq;
using Content.Server.GameTicking;
using Content.Server.Maps;
using Content.Server.Power.Components;
using Content.Server.Power.NodeGroups;
using Content.Server.Power.Pow3r;
using Content.Shared.Maps;
using Content.Shared.NodeContainer;
using Content.Shared.Power.Components;
using Robust.Server.GameObjects;
using Robust.Shared.EntitySerialization;
using Robust.Shared.EntitySerialization.Systems;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.IntegrationTests.Tests._Starlight.Power;

public sealed class GridPowerTests
{
    private const string EmptyMap = "Empty";

    private static readonly ResPath[] GridPaths =
    [
        new("/Maps/_Starlight/Shuttles/pts.yml"),
        new("/Maps/_Starlight/Shuttles/barge.yml"),
        new("/Maps/_Starlight/Shuttles/prospector.yml")
    ];

    [Test, TestCaseSource(nameof(GridPaths))]
    public async Task TestGridApcLoad(ResPath gridFilePath)
    {
        await using var pair = await PoolManager.GetServerClient(new PoolSettings { });
        var server = pair.Server;

        var entMan = server.EntMan;
        var protoMan = server.ProtoMan;
        var ticker = entMan.System<GameTicker>();
        var xform = entMan.System<TransformSystem>();
        var loader = entMan.System<MapLoaderSystem>();
        var mapSystem = entMan.System<MapSystem>();

        MapId mapId = MapId.Nullspace;

        // Load the map and grid
        await server.WaitAssertion(() =>
        {
            Assert.That(protoMan.TryIndex<GameMapPrototype>(EmptyMap, out var mapProto));
            var opts = DeserializationOptions.Default with { InitializeMaps = true };
            ticker.LoadGameMap(mapProto, out mapId, opts);
            var loadedGrid = loader.TryLoadGrid(mapId, gridFilePath, out var grid);
            Assert.That(loadedGrid, "Failed to load grid");
        });

        // Wait long enough for power to ramp up, but before anything can trip
        await pair.RunSeconds(2);

        // Check that no APCs start overloaded
        var apcQuery = entMan.EntityQueryEnumerator<ApcComponent, PowerNetworkBatteryComponent>();
        Assert.Multiple(() =>
        {
            while (apcQuery.MoveNext(out var uid, out var apc, out var battery))
            {
                // Uncomment the following line to log starting APC load to the console
                //Console.WriteLine($"ApcLoad:{gridFilePath}:{uid}:{battery.CurrentSupply}");
                if (xform.TryGetMapOrGridCoordinates(uid, out var coord))
                {
                    Assert.That(apc.MaxLoad, Is.GreaterThanOrEqualTo(battery.CurrentSupply),
                            $"APC {uid} on {gridFilePath} ({coord.Value.X}, {coord.Value.Y}) is overloaded {battery.CurrentSupply} / {apc.MaxLoad}");
                }
                else
                {
                    Assert.That(apc.MaxLoad, Is.GreaterThanOrEqualTo(battery.CurrentSupply),
                            $"APC {uid} on {gridFilePath} is overloaded {battery.CurrentSupply} / {apc.MaxLoad}");
                }
            }
        });

        await server.WaitAssertion(() =>
        {
            if (mapId != MapId.Nullspace)
                mapSystem.DeleteMap(mapId!);
        });

        await pair.CleanReturnAsync();
    }
}
