using Robust.Shared.GameStates;

namespace Content.Shared._CorvaxGoob.OfferItem;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState(true)]
[Access(typeof(SharedOfferItemSystem))]
public sealed partial class OfferItemComponent : Component
{
    [DataField] [AutoNetworkedField]
    public string? Hand;

    [ViewVariables(VVAccess.ReadWrite)] [DataField] [AutoNetworkedField]
    public bool IsInOfferMode;

    [DataField] [AutoNetworkedField]
    public bool IsInReceiveMode;

    [DataField] [AutoNetworkedField]
    public EntityUid? Item;

    [DataField]
    public float MaxOfferDistance = 2f;

    [DataField] [AutoNetworkedField]
    public EntityUid? Target;
}
