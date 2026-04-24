using Content.Goobstation.Shared.Security.ContrabandIcons;
using Content.Goobstation.Shared.Security.ContrabandIcons.Components;

namespace Content.Goobstation.Server.Security.ContrabandIcons;

public sealed class ContrabandIconsSystem : SharedContrabandIconsSystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<VisibleContrabandComponent, MapInitEvent>(OnMapInit);
    }

    private void OnMapInit(EntityUid uid, VisibleContrabandComponent component, MapInitEvent args) =>
        ContrabandDetect(uid, component);
}
