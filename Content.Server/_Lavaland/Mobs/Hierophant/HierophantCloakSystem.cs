using Content.Server.Actions;
using Content.Server.Hands.Systems;
using Content.Shared._Lavaland.Damage;
using Content.Shared.Actions;
using Content.Shared.Coordinates.Helpers;
using Content.Shared.Popups;
using Robust.Shared.Audio;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Map;
using Robust.Shared.Timing;

#pragma warning disable CS4014

namespace Content.Server._Lavaland.Mobs.Hierophant;

public sealed class HierophantCloakSystem : EntitySystem
{

    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly ActionsSystem _actions = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly HandsSystem _hands = default!;
    [Dependency] private readonly HierophantSystem _hierophant = default!;
    [Dependency] private readonly IMapManager _mapMan = default!;
    [Dependency] private readonly IGameTiming _timing = default!;


    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<HierophantCloakComponent, MapInitEvent>(OnInit);
        SubscribeLocalEvent<HierophantCloakComponent, GetItemActionsEvent>(OnGetActions);

        SubscribeLocalEvent<HierophantCloakComponent, HierophantCloakPlaceMarkerEvent>(OnPlaceMarker);
        SubscribeLocalEvent<HierophantCloakComponent, HierophantCloakTeleportToMarkerEvent>(OnTeleport);
    }

    private void OnInit(Entity<HierophantCloakComponent> ent, ref MapInitEvent args)
    {
        _actions.AddAction(ent, ref ent.Comp.PlaceMarkerActionEntity, ent.Comp.PlaceMarkerActionId);
        _actions.AddAction(ent, ref ent.Comp.TeleportToMarkerActionEntity, ent.Comp.TeleportToMarkerActionId);
    }

    private void OnGetActions(Entity<HierophantCloakComponent> ent, ref GetItemActionsEvent args)
    {
        args.AddAction(ref ent.Comp.PlaceMarkerActionEntity, ent.Comp.PlaceMarkerActionId);
        args.AddAction(ref ent.Comp.TeleportToMarkerActionEntity, ent.Comp.TeleportToMarkerActionId);
    }

    private void OnPlaceMarker(Entity<HierophantCloakComponent> ent, ref HierophantCloakPlaceMarkerEvent args)
    {
        if (args.Handled)
            return;

        var user = args.Performer;

        QueueDel(ent.Comp.TeleportMarker);

        var position = Transform(args.Performer)
            .Coordinates
            .AlignWithClosestGridTile(entityManager: EntityManager, mapManager: _mapMan);
        var dummy = Spawn(null, position);

        ent.Comp.TeleportMarker = dummy;

        _popup.PopupEntity("Teleportation point set.", user);

        //AddImmunity(user);
        _hierophant.SpawnDamageBox(user, 1, false);

        args.Handled = true;
    }

    private void OnTeleport(Entity<HierophantCloakComponent> ent, ref HierophantCloakTeleportToMarkerEvent args)
    {
        if (args.Handled)
            return;

        if (ent.Comp.TeleportMarker == null)
        {
            _popup.PopupClient("Marker is not placed!", args.Performer, PopupType.MediumCaution);
            return;
        }

        var user = args.Performer;

        //AddImmunity(user);
        _hierophant.Blink(user, ent.Comp.TeleportMarker);

        args.Handled = true;
    }
}
