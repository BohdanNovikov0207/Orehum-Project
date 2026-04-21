using Content.Shared.Damage.Prototypes;
using Content.Goobstation.Maths.FixedPoint;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;
using Robust.Shared.Utility;
using static Robust.Shared.Utility.SpriteSpecifier;

namespace Content.Shared._Orehum.Orders;

/// <summary>
/// Component for effect of the Hold Order.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class HoldOrderComponent : Component, IOrderComponent
{
    [DataField, AutoNetworkedField]
    public SpriteSpecifier Icon = new Rsi(new ResPath("/Textures/_Orehum/Interface/marine_orders.rsi"), "hold");

    /// <summary>
    /// Resistance to damage.
    /// </summary>
    [DataField, AutoNetworkedField]
    public FixedPoint2 DamageModifier = 0.1;

    [DataField]
    public List<ProtoId<DamageTypePrototype>> DamageTypes = new() {"Slash", "Blunt"};

    /// <summary>
    /// Resistance to pain.
    /// </summary>
    /// <remarks>
    /// I am unsure of when pain will be implemented but I am putting this here for the future.
    /// </remarks>
    /// CM14 TODO Make this do something meaningful when pain is actually a thing.
    [DataField, AutoNetworkedField]
    public FixedPoint2 PainModifier = 0.1;

    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer)), AutoNetworkedField]
    public TimeSpan Duration { get; set; }

    public void AssignMultiplier(FixedPoint2 multiplier)
    {
        DamageModifier *= multiplier;
        PainModifier *= multiplier;
    }
    public override bool SessionSpecific => true;
}
