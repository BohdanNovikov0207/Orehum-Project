using Content.Goobstation.Maths.FixedPoint;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;
using Robust.Shared.Utility;
using static Robust.Shared.Utility.SpriteSpecifier;

namespace Content.Shared._Orehum.Orders;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class MoveOrderComponent : Component, IOrderComponent
{
    [DataField, AutoNetworkedField]
    public SpriteSpecifier Icon = new Rsi(new ResPath("/Textures/_Orehum/Interface/marine_orders.rsi"), "move");

    [DataField, AutoNetworkedField]
    public FixedPoint2 MoveSpeedModifier = 0.5;

    [DataField, AutoNetworkedField]
    public FixedPoint2 DodgeModifier = 5;

    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer)), AutoNetworkedField]
    public TimeSpan Duration { get; set; }

    public void AssignMultiplier(FixedPoint2 multiplier)
    {
        MoveSpeedModifier *= multiplier;
        DodgeModifier *= multiplier;
    }

    public override bool SessionSpecific => true;
}
