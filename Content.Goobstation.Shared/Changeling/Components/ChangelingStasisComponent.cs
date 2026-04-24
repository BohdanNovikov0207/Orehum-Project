using Content.Shared.Atmos;
using Content.Shared.Damage.Prototypes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Changeling.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class ChangelingStasisComponent : Component
{
    [DataField]
    public LocId AbsorbedPopup = "changeling-stasis-absorbed";

    // actionsEnts
    [DataField] [AutoNetworkedField]
    public EntityUid? ActionEnt;

    // protoIDs
    [DataField]
    public EntProtoId ActionId = "ActionRegenerativeStasis";

    [DataField]
    public ProtoId<DamageTypePrototype> AliveDamageProto = "Slash";

    [DataField]
    public ProtoId<DamageTypePrototype> CritDamageProto = "Asphyxiation";

    [DataField] [AutoNetworkedField]
    public TimeSpan CritStasisTime = TimeSpan.FromSeconds(45);

    [DataField] [AutoNetworkedField]
    public TimeSpan DeadStasisTime = TimeSpan.FromSeconds(60);

    // the important stuff
    [DataField] [AutoNetworkedField]
    public TimeSpan DefaultStasisTime = TimeSpan.FromSeconds(15);

    [DataField]
    public LocId EnterAlivePopup = "suicide-command-default-text-others"; // suicide message

    [DataField]
    public LocId EnterDamagedPopup = "changeling-stasis-enter-damaged";

    [DataField]
    public LocId EnterDeadPopup = "changeling-stasis-enter-dead";

    // LocIds
    [DataField]
    public LocId EnterPopup = "changeling-stasis-enter";

    [DataField]
    public LocId ExitDefibPopup = "changeling-stasis-defib";

    [DataField]
    public LocId ExitPopup = "changeling-stasis-exit";

    [DataField] [AutoNetworkedField]
    public float IdealTemp = Atmospherics.T37C;

    [DataField] [AutoNetworkedField]
    public bool IsInStasis;

    [DataField]
    public LocId SelfReviveFailPopup = "self-revive-fail";

    [DataField] [AutoNetworkedField]
    public TimeSpan StasisTime = default!;

    [DataField] [AutoNetworkedField]
    public TimeSpan StunTime = TimeSpan.FromSeconds(1);
}
