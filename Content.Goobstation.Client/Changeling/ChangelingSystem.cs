using Content.Client.Alerts;
using Content.Client.UserInterface.Systems.Alerts.Controls;
using Content.Goobstation.Shared.Changeling.Components;
using Content.Goobstation.Shared.Changeling.Systems;
using Content.Shared.StatusIcon.Components;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Client.Changeling;

public sealed class ChangelingSystem : SharedChangelingSystem
{

    private const int MaxChemicalsNormalizer = 18;
    private const int MaxBiomassNormalizer = 16;
    [Dependency] private readonly IPrototypeManager _prototype = default!;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ChangelingIdentityComponent, UpdateAlertSpriteEvent>(OnUpdateAlert);
        SubscribeLocalEvent<ChangelingIdentityComponent, GetStatusIconsEvent>(GetChanglingIcon);
    }

    private void OnUpdateAlert(EntityUid uid, ChangelingIdentityComponent comp, ref UpdateAlertSpriteEvent args)
    {
        var stateNormalized = 0f;

        // hardcoded because uhh umm i don't know. send help.
        switch (args.Alert.AlertKey.AlertType)
        {
            case "ChangelingChemicals":
                stateNormalized = (int) (comp.Chemicals / comp.MaxChemicals * MaxChemicalsNormalizer);
                break;

            case "ChangelingBiomass":
                stateNormalized = (int) (comp.Biomass / comp.MaxBiomass * MaxBiomassNormalizer);
                break;
            default:
                return;
        }
        var sprite = args.SpriteViewEnt.Comp;
        sprite.LayerSetState(AlertVisualLayers.Base, $"{stateNormalized}");
    }

    private void GetChanglingIcon(Entity<ChangelingIdentityComponent> ent, ref GetStatusIconsEvent args)
    {
        if (HasComp<HivemindComponent>(ent) && _prototype.TryIndex(ent.Comp.StatusIcon, out var iconPrototype))
            args.StatusIcons.Add(iconPrototype);
    }
}
