using Robust.Shared.GameStates;

namespace Content.Shared._Shitcode.Heretic.Components;

[RegisterComponent] [NetworkedComponent]
public sealed partial class ShadowCloakEntityComponent : Component
{
    [ViewVariables]
    public float? DeletionAccumulator;

    [DataField]
    public float Lifetime = 3.2f;
}
