using Content.Client.Overlays;
using Content.Shared._Orehum.MoreGoggles;
using Content.Shared.Inventory;
using Content.Shared.Inventory.Events;
using Content.Shared.Overlays.Switchable;
using Robust.Client.Graphics;

namespace Content.Client._Orehum.MoreGoggles;

public sealed class MinerVisionSystem : EquipmentHudSystem<MinerVisionComponent>
{
    [Dependency] private readonly IOverlayManager _overlayMan = default!;

    private MinerVisionOverlay _minerOverlay = null!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MinerVisionComponent, SwitchableOverlayToggledEvent>(OnToggle);

        _minerOverlay = new();
    }

    protected override void OnRefreshComponentHud(
        EntityUid uid,
        MinerVisionComponent component,
        RefreshEquipmentHudEvent<MinerVisionComponent> args
    )
    {
        if (component.IsEquipment)
            return;

        base.OnRefreshComponentHud(uid, component, args);
    }

    protected override void OnRefreshEquipmentHud(
        EntityUid uid,
        MinerVisionComponent component,
        InventoryRelayedEvent<RefreshEquipmentHudEvent<MinerVisionComponent>> args
    )
    {
        if (!component.IsEquipment)
            return;

        base.OnRefreshEquipmentHud(uid, component, args);
    }

    private void OnToggle(Entity<MinerVisionComponent> ent, ref SwitchableOverlayToggledEvent args)
    {
        RefreshOverlay(args.User);
    }

    protected override void UpdateInternal(RefreshEquipmentHudEvent<MinerVisionComponent> args)
    {
        base.UpdateInternal(args);
        MinerVisionComponent? mvComp = null;
        foreach (var comp in args.Components)
        {
            if (!comp.IsActive && (comp.PulseTime <= 0f || comp.PulseAccumulator >= comp.PulseTime))
                continue;

            if (mvComp == null)
                mvComp = comp;
            else if (!mvComp.DrawOverlay && comp.DrawOverlay)
                mvComp = comp;
            else if (mvComp.DrawOverlay == comp.DrawOverlay && mvComp.PulseTime > 0f && comp.PulseTime <= 0f)
                mvComp = comp;
        }

        UpdateOverlay(mvComp);
    }

    protected override void DeactivateInternal()
    {
        base.DeactivateInternal();

        UpdateOverlay(null);
    }

    private void UpdateOverlay(MinerVisionComponent? tvComp)
    {
        _minerOverlay.Comp = tvComp;

        switch (tvComp)
        {
            case not null when !_overlayMan.HasOverlay<MinerVisionOverlay>():
                _overlayMan.AddOverlay(_minerOverlay);
                break;
            case null:
                _overlayMan.RemoveOverlay(_minerOverlay);
                break;
        }
    }
}
