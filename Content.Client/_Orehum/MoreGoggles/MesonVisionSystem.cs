
using Content.Client.Overlays;
using Content.Shared._Orehum.MoreGoggles;
using Content.Shared.Inventory;
using Content.Shared.Inventory.Events;
using Content.Shared.Overlays.Switchable;
using Robust.Client.Graphics;


namespace Content.Client._Orehum.MoreGoggles;

public sealed class MesonVisionSystem : EquipmentHudSystem<MesonVisionComponent>
{
    [Dependency] private readonly IOverlayManager _overlayMan = default!;

    private MesonVisionOverlay _mesonOverlay = null!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MesonVisionComponent, SwitchableOverlayToggledEvent>(OnToggle);

        _mesonOverlay = new();
    }

    protected override void OnRefreshComponentHud(
        EntityUid uid,
        MesonVisionComponent component,
        RefreshEquipmentHudEvent<MesonVisionComponent> args
    )
    {
        if (component.IsEquipment)
            return;

        base.OnRefreshComponentHud(uid, component, args);
    }

    protected override void OnRefreshEquipmentHud(
        EntityUid uid,
        MesonVisionComponent component,
        InventoryRelayedEvent<RefreshEquipmentHudEvent<MesonVisionComponent>> args
    )
    {
        if (!component.IsEquipment)
            return;

        base.OnRefreshEquipmentHud(uid, component, args);
    }

    private void OnToggle(Entity<MesonVisionComponent> ent, ref SwitchableOverlayToggledEvent args)
    {
        RefreshOverlay(args.User);
    }

    protected override void UpdateInternal(RefreshEquipmentHudEvent<MesonVisionComponent> args)
    {
        base.UpdateInternal(args);

        var active = false;
        MesonVisionComponent? mvComp = null;
        foreach (var comp in args.Components)
        {
            if (comp.IsActive || comp.PulseTime > 0f && comp.PulseAccumulator < comp.PulseTime)
                active = true;
            else
                continue;
            if (comp.DrawOverlay)
            {
                if (mvComp == null)
                    mvComp = comp;
                else if (mvComp.PulseTime > 0f && comp.PulseTime <= 0f)
                    mvComp = comp;
            }

            if (active && mvComp is { PulseTime: <= 0 })
                break;
        }

        UpdateOverlay(mvComp);
    }

    protected override void DeactivateInternal()
    {
        base.DeactivateInternal();

        UpdateOverlay(null);
    }

    private void UpdateOverlay(MesonVisionComponent? tvComp)
    {
        _mesonOverlay.Comp = tvComp;

        switch (tvComp)
        {
            case not null when !_overlayMan.HasOverlay<MesonVisionOverlay>():
                _overlayMan.AddOverlay(_mesonOverlay);
                break;
            case null:
                _overlayMan.RemoveOverlay(_mesonOverlay);
                break;
        }
    }
}
