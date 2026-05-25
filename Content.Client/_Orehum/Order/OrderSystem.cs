using Content.Shared._Orehum.Orders;
using Robust.Client.Graphics;

namespace Content.Client._Orehum.Orders;

public sealed class OrdersSystem : SharedOrdersSystem
{

    [Dependency] private readonly IOverlayManager _overlays = default!;

    public override void Initialize()
    {
        base.Initialize();

        _overlays.AddOverlay(new OrdersOverlay());
    }

    public override void Shutdown()
    {
        _overlays.RemoveOverlay<OrdersOverlay>();

        base.Shutdown();
    }
}
