using Robust.Shared.GameStates;

namespace Content.Shared._Shitcode.Heretic.Components;

[RegisterComponent] [NetworkedComponent]
public sealed partial class StarTouchedComponent : Component
{
    [ViewVariables(VVAccess.ReadWrite)]
    public float Accumulator;

    [DataField]
    public bool ApplyEffects;

    [DataField]
    public float Range = 8f;

    [DataField]
    public float TickInterval = 0.2f;
}
