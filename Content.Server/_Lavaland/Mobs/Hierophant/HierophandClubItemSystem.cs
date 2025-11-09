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
using Content.Shared.Damage;
using Content.Shared.Mobs.Components;
using Content.Shared.Mobs;
using Content.Shared.Mobs.Components;
using Robust.Shared.Utility;
using Content.Server.Damage.Systems;

using Robust.Shared.Prototypes;
using Content.Server._Lavaland.Mobs.Hierophant.Components;

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

namespace Content.Server._Lavaland.Mobs.Hierophant;

public sealed class HierophandClubItemSystem : EntitySystem
{
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly ActionsSystem _actions = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly HandsSystem _hands = default!;
    [Dependency] private readonly HierophantSystem _hierophant = default!;
    [Dependency] private readonly IMapManager _mapMan = default!;
    [Dependency] private readonly IGameTiming _timing = default!;

    [Dependency] private readonly DamageableSystem _damageableSystem = default!;

    private readonly EntProtoId _chaserPrototype = "LavalandHierophantChaser";

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<HierophantClubItemComponent, MapInitEvent>(OnClubInit);
        SubscribeLocalEvent<HierophantClubItemComponent, GetItemActionsEvent>(OnGetActions);

        SubscribeLocalEvent<HierophantClubItemComponent, HierophantClubActivateCrossEvent>(OnCreateCross);
        SubscribeLocalEvent<HierophantClubItemComponent, HierophantClubActivateDamageAreaEvent>(OnCreateDamageArea);
        SubscribeLocalEvent<HierophantClubItemComponent, HierophantClubActivateChaserEvent>(OnCreateChaser);

        //SubscribeLocalEvent<HierophantClubItemComponent, HierophantClubPlaceMarkerEvent>(OnPlaceMarker);
        //SubscribeLocalEvent<HierophantClubItemComponent, HierophantClubTeleportToMarkerEvent>(OnTeleport);
    }

    private void OnClubInit(Entity<HierophantClubItemComponent> ent, ref MapInitEvent args)
    {
        _actions.AddAction(ent, ref ent.Comp.CreateCrossActionEntity, ent.Comp.CreateCrossActionId);
        _actions.AddAction(ent, ref ent.Comp.CreateDamageAreaActionEntity, ent.Comp.CreateDamageAreaActionId);
        _actions.AddAction(ent, ref ent.Comp.CreateChaserActionEntity, ent.Comp.CreateChaserActionId);
        //_actions.AddAction(ent, ref ent.Comp.PlaceMarkerActionEntity, ent.Comp.PlaceMarkerActionId);
        //_actions.AddAction(ent, ref ent.Comp.TeleportToMarkerActionEntity, ent.Comp.TeleportToMarkerActionId);
    }

    private void OnGetActions(Entity<HierophantClubItemComponent> ent, ref GetItemActionsEvent args)
    {
        args.AddAction(ref ent.Comp.CreateCrossActionEntity, ent.Comp.CreateCrossActionId);
        args.AddAction(ref ent.Comp.CreateDamageAreaActionEntity, ent.Comp.CreateDamageAreaActionId);
        args.AddAction(ref ent.Comp.CreateChaserActionEntity, ent.Comp.CreateChaserActionId);
        //args.AddAction(ref ent.Comp.PlaceMarkerActionEntity, ent.Comp.PlaceMarkerActionId);
        //args.AddAction(ref ent.Comp.TeleportToMarkerActionEntity, ent.Comp.TeleportToMarkerActionId);
    }

    private void OnCreateCross(Entity<HierophantClubItemComponent> ent, ref HierophantClubActivateCrossEvent args)
    {
        if (args.Handled || !args.Target.IsValid(EntityManager))
            return;

        var uid = ent.Owner;
        var user = args.Performer;

        if (!_hands.IsHolding(user, uid, out _))
        {
            _popup.PopupEntity(Loc.GetString("dash-ability-not-held", ("item", uid)), user, user);
            return;
        }

        var targetCoords = args.Target.SnapToGrid(EntityManager, _mapMan);

        SpawnHierophantCross(user, targetCoords, ent.Comp);

        args.Handled = true;
    }

    private void OnCreateDamageArea(Entity<HierophantClubItemComponent> ent, ref HierophantClubActivateDamageAreaEvent args)
    {
        if (args.Handled || !args.Target.IsValid(EntityManager))
            return;

        var uid = ent.Owner;
        var user = args.Performer;

        if (!_hands.IsHolding(user, uid, out _))
        {
            _popup.PopupEntity(Loc.GetString("dash-ability-not-held", ("item", uid)), user, user);
            return;
        }

        var targetCoords = args.Target.SnapToGrid(EntityManager, _mapMan);

        SpawnHierophantDamageArea(user, targetCoords, ent.Comp);

        args.Handled = true;
    }

    private void OnCreateChaser(Entity<HierophantClubItemComponent> ent, ref HierophantClubActivateChaserEvent args)
    {
        if (args.Handled || !args.Target.IsValid(EntityManager))
            return;

        var uid = ent.Owner;
        var user = args.Performer;

        if (!_hands.IsHolding(user, uid, out _))
        {
            _popup.PopupEntity(Loc.GetString("dash-ability-not-held", ("item", uid)), user, user);
            return;
        }

        var targetCoords = args.Target.SnapToGrid(EntityManager, _mapMan);

        SpawnHierophantChaser(user, targetCoords, ent.Comp);

        args.Handled = true;
    }

    /*
    private void OnPlaceMarker(Entity<HierophantClubItemComponent> ent, ref HierophantClubPlaceMarkerEvent args)
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

        AddImmunity(user);
        _hierophant.SpawnDamageBox(user, 1, false);

        args.Handled = true;
    }

    private void OnTeleport(Entity<HierophantClubItemComponent> ent, ref HierophantClubTeleportToMarkerEvent args)
    {
        if (args.Handled)
            return;

        if (ent.Comp.TeleportMarker == null)
        {
            _popup.PopupClient("Marker is not placed!", args.Performer, PopupType.MediumCaution);
            return;
        }

        var user = args.Performer;

        AddImmunity(user);
        _hierophant.Blink(user, ent.Comp.TeleportMarker);

        args.Handled = true;
    }
    */

    private void SpawnHierophantCross(EntityUid owner, EntityCoordinates coords, HierophantClubItemComponent club)
    {
        AddImmunity(owner);

        if (!TryComp<DamageableComponent>(owner, out var damageable))
        {
            Logger.Warning($"Owner {owner} missing damage or threshold components.");
            return;
        }

        var damage = (int)Math.Round(damageable.TotalDamage.Float() / 10.0);
        var finalRange = club.CrossRange + damage;

        var dummy = Spawn(null, coords);
        _hierophant.SpawnCross(dummy, finalRange, 0f);
        _audio.PlayPvs(club.DamageSound, coords, AudioParams.Default.WithMaxDistance(10f));
        QueueDel(dummy);
    }

    private void SpawnHierophantDamageArea(EntityUid owner, EntityCoordinates coords, HierophantClubItemComponent club)
    {
        AddImmunity(owner);

        if (!TryComp<DamageableComponent>(owner, out var damageable))
        {
            Logger.Warning($"Owner {owner} missing damage or threshold components.");
            return;
        }

        var damage = (int) Math.Round((damageable.TotalDamage.Float() / 2) / 10.0);
        var finalRange = club.DamageAreaRange + damage;

        var dummy = Spawn(null, coords);
        var delay = 0;
        for (int i = 0; i <= finalRange; i++)
        {
            var rangeCopy = i;
            Timer.Spawn(delay,
                () =>
                {
                    _hierophant.SpawnDamageBox(dummy, rangeCopy, true);
                });
            delay +=(int)(100 - damage * 0.2);
        }

        _audio.PlayPvs(club.DamageSound, coords, AudioParams.Default.WithMaxDistance(10f));
        Timer.Spawn(delay + 10000,
            () =>
            {
                QueueDel(dummy); // cleanup after attack is done
            });
    }


    private void SpawnHierophantChaser(EntityUid owner, EntityCoordinates coords, HierophantClubItemComponent club)
    {


        if (!TryComp<DamageableComponent>(owner, out var damageable))
        {
            Logger.Warning($"Owner {owner} missing damage or threshold components.");
            return;
        }

        var damage = (int) Math.Round(damageable.TotalDamage.Float() / 10.0);
        var finalMaxSteps = club.ChaserMaxSteps + damage;

        AddImmunity(owner, finalMaxSteps + 5);

        var dummy = Spawn(null, coords);
        /*
        var delay = 0;
        
        Timer.Spawn(delay,
            () =>
            {*/
        var chaser = Spawn(_chaserPrototype, Transform(owner).Coordinates);
        if (TryComp<HierophantChaserComponent>(chaser, out var chasercomp))
        {
            chasercomp.Target = dummy;
            chasercomp.MaxSteps *= finalMaxSteps;
            chasercomp.Speed += damage * 0.5f;
        }
        //});

        _audio.PlayPvs(club.DamageSound, coords, AudioParams.Default.WithMaxDistance(10f));
        Timer.Spawn((int)((finalMaxSteps + 5) * 10000), () => {
            QueueDel(dummy);
        });
    }

    private void AddImmunity(EntityUid uid, float time = 3f)
    {
        EnsureComp<DamageSquareImmunityComponent>(uid).HasImmunityUntil = _timing.CurTime + TimeSpan.FromSeconds(time);
    }
}
