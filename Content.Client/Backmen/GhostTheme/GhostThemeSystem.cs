using Content.Shared.Backmen.GhostTheme;
using Robust.Client.GameObjects;
using Robust.Shared.Prototypes;

namespace Content.Client.Backmen.GhostTheme;

public sealed class GhostThemeSystem : EntitySystem
{
    [ValidatePrototypeId<EntityPrototype>]
    private const string MobObserver = "MobObserver";

    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
    [Dependency] private readonly SpriteSystem _spriteSystem = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<GhostThemeComponent, AfterAutoHandleStateEvent>(OnInit);
    }

    private void OnInit(EntityUid uid, GhostThemeComponent component, ref AfterAutoHandleStateEvent args)
    {
        if (component.GhostTheme == null
            || !_prototypeManager.TryIndex(component.GhostTheme, out var ghostThemePrototype))
            return;

        Apply(uid, ghostThemePrototype);
    }

    public void Apply(EntityUid uid, GhostThemePrototype ghostThemePrototype)
    {
        var rendered = Spawn(MobObserver, ghostThemePrototype.Components);
        _spriteSystem.CopySprite(rendered, uid);
        QueueDel(rendered);
    }
}
