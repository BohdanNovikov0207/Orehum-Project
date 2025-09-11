using Content.Server.Buckle.Systems;
using Content.Shared.Buckle.Components;
using Content.Shared.Mobs.Systems;
using Content.Shared.Popups;

namespace Content.Server._Orehum.LavalandExpansion;

public sealed class AshWalkersNestSystem : EntitySystem
{
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly MobStateSystem _mobState = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<AshWalkersNestComponent, StrapAttemptEvent>(OnStrapped);
    }

    private void OnStrapped(EntityUid uid, AshWalkersNestComponent component, ref StrapAttemptEvent args)
    {
        args.Cancelled = true;

        var target = args.Buckle.Owner;
        if (!_mobState.IsDead(target))
        {
            _popup.PopupCoordinates("Шипастые усики отталкивают этот объект!", Transform(target).Coordinates);
            return;
        }

        _popup.PopupCoordinates($"Шипастые усики жадно подтаскивают тело {Name(target)} и разрывают его на куски, окропляя кровью растущие яйца.", Transform(target).Coordinates);
        QueueDel(target);

        Feed(uid, component);
    }

    private void Feed(EntityUid uid, AshWalkersNestComponent component)
    {
        component.Accumulator++;

        if (component.Accumulator >= 2)
        {
            component.Accumulator = 0;
            Spawn("AshWalkerEmptyEgg", Transform(uid).Coordinates);
        }
    }
}
