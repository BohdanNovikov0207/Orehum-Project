using System.Linq;
using System.Threading.Tasks;
using Content.Server._Lavaland.Mobs.Vigilante.Components;
using Robust.Shared.Map.Components;
using Timer = Robust.Shared.Timing.Timer;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

namespace Content.Server._Lavaland.Mobs.Vigilante;

public sealed class VigilanteTelepadSystem : EntitySystem
{
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    [Dependency] private readonly SharedMapSystem _map = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<VigilanteTelepadComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<VigilanteTelepadComponent, EntityTerminatingEvent>(OnTerminating);
    }

    private void OnMapInit(Entity<VigilanteTelepadComponent> ent, ref MapInitEvent args)
    {
        var xform = Transform(ent).Coordinates;
        var vigilante = Spawn(ent.Comp.VigilantePrototype, xform);

        if (!TryComp<VigilanteBossComponent>(vigilante, out var vigilanteComp))
            return;

        ent.Comp.ConnectedVigilante = vigilante;
        vigilanteComp.ConnectedTelepad = ent;
    }

    private void OnTerminating(Entity<VigilanteTelepadComponent> ent, ref EntityTerminatingEvent args)
    {
        if (ent.Comp.ConnectedVigilante != null &&
            TryComp<VigilanteBossComponent>(ent.Comp.ConnectedVigilante.Value, out var vigilanteComp))
            vigilanteComp.ConnectedTelepad = null;

        DeleteVigilanteFieldImmediatly(ent);
    }

    public void ActivateField(Entity<VigilanteTelepadComponent> ent)
    {
        if (ent.Comp.Enabled)
            return; // how?

        SpawnVigilanteField(ent);
        ent.Comp.Enabled = true;
    }

    public void DeactivateField(Entity<VigilanteTelepadComponent> ent)
    {
        if (!ent.Comp.Enabled)
            return; // how?

        DeleteVigilanteField(ent);
        ent.Comp.Enabled = false;
    }

    public void DeleteVigilanteFieldImmediatly(Entity<VigilanteTelepadComponent> ent)
    {
        var walls = ent.Comp.Walls.Where(x => !TerminatingOrDeleted(x));
        foreach (var wall in walls)
        {
            QueueDel(wall);
        }
    }

    private async Task SpawnVigilanteField(Entity<VigilanteTelepadComponent> ent)
    {
        var xform = Transform(ent);

        if (!TryComp<MapGridComponent>(xform.GridUid, out var grid))
            return;

        var gridEnt = (xform.GridUid.Value, grid);
        var range = ent.Comp.Radius;
        var center = xform.Coordinates.Position;

        // get tile position of our entity
        if (!_transform.TryGetGridTilePosition((ent, xform), out var tilePos))
            return;

        var pos = _map.TileCenterToVector(gridEnt, tilePos);
        var confines = new Box2(center, center).Enlarged(ent.Comp.Radius);
        var box = _map.GetLocalTilesIntersecting(ent, grid, confines).ToList();

        var confinesS = new Box2(pos, pos).Enlarged(Math.Max(range - 1, 0));
        var boxS = _map.GetLocalTilesIntersecting(ent, grid, confinesS).ToList();
        box = box.Where(b => !boxS.Contains(b)).ToList();

        // fill the box
        //Timer.Spawn(5000, () =>
        //{
            foreach (var tile in box)
            {
                var wall = Spawn(ent.Comp.WallPrototype, _map.GridTileToWorld(xform.GridUid.Value, grid, tile.GridIndices));
                ent.Comp.Walls.Add(wall);
            }
        //});
    }

    private async Task DeleteVigilanteField(Entity<VigilanteTelepadComponent> ent)
    {
        var walls = ent.Comp.Walls.Where(x => !TerminatingOrDeleted(x));
        foreach (var wall in walls)
        {
            QueueDel(wall);
        }
    }
}
