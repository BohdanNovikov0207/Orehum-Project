using Content.Shared.Emag.Systems;
using Robust.Shared.GameStates;

namespace Content.Goobstation.Shared.Wraith.Components;

[RegisterComponent] [NetworkedComponent]
public sealed partial class DecayComponent : Component
{
    /// <summary>
    /// What emag interaction to use
    /// </summary>
    [DataField]
    public EmagType Emag = EmagType.All;

    /// <summary>
    /// How much stamina damage to apply over time.
    /// </summary>
    [DataField]
    public float StaminaDamageAmount = 150f;
}
