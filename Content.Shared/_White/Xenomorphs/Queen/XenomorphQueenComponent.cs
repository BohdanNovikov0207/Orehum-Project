using Content.Shared._White.Xenomorphs.Caste;
using Robust.Shared.Prototypes;

namespace Content.Shared._White.Xenomorphs.Queen;

[RegisterComponent]
public sealed partial class XenomorphQueenComponent : Component
{
    [DataField]
    public List<ProtoId<XenomorphCastePrototype>> CasteWhitelist = new() { "Drone", "Hunter", "Sentinel" };

    [DataField]
    public TimeSpan EvolutionDelay = TimeSpan.FromSeconds(3);

    [DataField]
    public EntProtoId PromoteTo = "MobXenomorphPraetorian";

    [ViewVariables]
    public EntityUid? PromotionAction;

    [DataField]
    public EntProtoId PromotionActionId = "ActionXenomorphPromotion";
}
