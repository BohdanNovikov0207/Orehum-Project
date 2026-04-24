using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.StatusEffect;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Wraith.Components;

[RegisterComponent] [NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class HauntComponent : Component
{
    [DataField]
    public EntityUid? ActionEnt;

    [DataField]
    public EntProtoId ActionId = "ActionHaunt";

    /// <summary>
    /// Is the action active?
    /// </summary>
    [ViewVariables] [AutoNetworkedField]
    public bool Active;

    /// <summary>
    /// The status effect to make the Wraith corporeal upon using haunt.
    /// </summary>
    [DataField]
    public ProtoId<StatusEffectPrototype> CorporealEffect = "Corporeal";

    /// <summary>
    /// The status effect to flash anyone who gets haunted.
    /// </summary>
    [DataField]
    public ProtoId<StatusEffectPrototype> FlashedId = "Flashed";

    /// <summary>
    /// How much the Wp regeneration gets boosted per witness.
    /// </summary>
    [DataField]
    public TimeSpan HauntCorporealDuration = TimeSpan.FromSeconds(30);

    /// <summary>
    /// How long the haunt lasts
    /// </summary>
    [DataField]
    public TimeSpan HauntDuration = TimeSpan.FromSeconds(30);

    /// <summary>
    /// How long the flash effect lasts when someone gets haunted.
    /// </summary>
    [DataField]
    public TimeSpan HauntFlashDuration = TimeSpan.FromSeconds(2);

    /// <summary>
    /// How long the Wp regen boost lasts.
    /// </summary>
    [DataField]
    public TimeSpan HauntWpRegenDuration = TimeSpan.FromSeconds(30);

    /// <summary>
    /// How much the Wp regeneration gets boosted per witness.
    /// </summary>
    [DataField]
    public FixedPoint2 HauntWpRegenPerWitness = 0.5;

    [ViewVariables] [AutoNetworkedField]
    public TimeSpan NextHauntUpdate;

    [DataField] [AutoNetworkedField]
    public TimeSpan NextHauntWpRegenUpdate = TimeSpan.Zero;

    [ViewVariables]
    public FixedPoint2 OriginalWpRegen;

    [ViewVariables] [AutoNetworkedField]
    public TimeSpan WitnessNextUpdate;

    [DataField]
    public TimeSpan WitnessUpdate = TimeSpan.FromSeconds(0.75f);

    /// <summary>
    /// Is the wp boost active?
    /// </summary>
    [ViewVariables] [AutoNetworkedField]
    public bool WpBoostActive;
}
