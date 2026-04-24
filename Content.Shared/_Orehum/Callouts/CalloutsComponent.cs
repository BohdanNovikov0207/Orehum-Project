using Content.Shared.Radio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Orehum.Callouts;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class CalloutsComponent : Component
{
    [DataField] [AutoNetworkedField]
    public EntProtoId AttackAction = "OrehumActionCalloutAttack";

    [AutoNetworkedField]
    public EntityUid? AttackActionEntity;

    [DataField] [AutoNetworkedField]
    public EntProtoId AttackEffectOnAction = "OrehumEffectAttackOrder";

    [DataField] [AutoNetworkedField]
    public List<LocId> AttackOrderSays = new()
    {
        "attack-order-callout-1", "attack-order-callout-2", "attack-order-callout-3", "attack-order-callout-4",
        "attack-order-callout-5", "attack-order-callout-6", "attack-order-callout-7",
    };

    [DataField] [AutoNetworkedField]
    public TimeSpan Cooldown = TimeSpan.FromSeconds(30);

    [DataField] [AutoNetworkedField]
    public ProtoId<RadioChannelPrototype> DefaultFallbackChannel = "Security";

    [DataField] [AutoNetworkedField]
    public EntProtoId DefendAction = "OrehumActionCalloutDefend";

    [AutoNetworkedField]
    public EntityUid? DefendActionEntity;

    [DataField] [AutoNetworkedField]
    public EntProtoId DefendEffectOnAction = "OrehumEffectDefendOrder";

    [DataField] [AutoNetworkedField]
    public List<LocId> DefendOrderSays = new()
    {
        "defend-order-callout-1", "defend-order-callout-2", "defend-order-callout-3", "defend-order-callout-4",
        "defend-order-callout-5", "defend-order-callout-6", "defend-order-callout-7", "defend-order-callout-8",
        "defend-order-callout-9", "defend-order-callout-10",
    };

    [DataField] [AutoNetworkedField]
    public EntProtoId RallyAction = "OrehumActionCalloutRally";

    [AutoNetworkedField]
    public EntityUid? RallyActionEntity;

    [DataField] [AutoNetworkedField]
    public EntProtoId RallyEffectOnAction = "OrehumEffectRallyOrder";

    [DataField] [AutoNetworkedField]
    public List<LocId> RallyOrderSays = new()
    {
        "rally-order-callout-1", "rally-order-callout-2", "rally-order-callout-3",
        "rally-order-callout-4", "rally-order-callout-5",
    };

    [DataField] [AutoNetworkedField]
    public EntProtoId RetreatAction = "OrehumActionCalloutRetreat";

    [AutoNetworkedField]
    public EntityUid? RetreatActionEntity;

    [DataField] [AutoNetworkedField]
    public EntProtoId RetreatEffectOnAction = "OrehumEffectRetreatOrder";

    [DataField] [AutoNetworkedField]
    public List<LocId> RetreatOrderSays = new()
    {
        "retreat-order-callout-1", "retreat-order-callout-2", "retreat-order-callout-3", "retreat-order-callout-4",
        "retreat-order-callout-5", "retreat-order-callout-6",
    };
}
