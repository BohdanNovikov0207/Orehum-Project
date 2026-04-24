using Content.Shared.StatusEffect;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Goobstation.Shared.Wraith.SpiritCandle;

[RegisterComponent] [NetworkedComponent]
public sealed partial class SpiritCandleComponent : Component
{
    /// <summary>
    /// Whether the candle has been lit or not
    /// </summary>
    [ViewVariables]
    public bool Active;

    [ViewVariables]
    public EntityUid? AreaUid;

    [ViewVariables]
    public ProtoId<StatusEffectPrototype> Corporeal = "Corporeal";

    [DataField]
    public TimeSpan CorporealDuration = TimeSpan.FromSeconds(15);

    /// <summary>
    /// The entity that holds the area
    /// </summary>
    [ViewVariables]
    public EntityUid? Holder;

    [DataField]
    public EntProtoId SpiritArea = "SpiritCandleRevealArea";

    [DataField]
    public SoundSpecifier SuccessSound = new SoundPathSpecifier("/Audio/_Goobstation/Wraith/wraithwhisper1.ogg");

    [ViewVariables]
    public EntProtoId Weakened = "StatusEffectWeakenedWraith";

    [DataField]
    public TimeSpan WeakenedDuration = TimeSpan.FromSeconds(15);

    #region Visuals

    [DataField] public string OneCharge = "eye";
    [DataField] public string TwoCharge = "eyes";

    #endregion
}

[Serializable] [NetSerializable]
public enum SpiritCandleVisuals : byte
{
    Layer,
}
