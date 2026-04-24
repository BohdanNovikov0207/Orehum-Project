using Robust.Shared.GameStates;

namespace Content.Shared._Lavaland.Anger.Components;

/// <summary>
/// Makes action's delay depend on current anger level of the parent entity.
/// </summary>
[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class AngerDelayActionComponent : Component
{
    [DataField] [AutoNetworkedField]
    public bool Inverse;

    [DataField(required: true)] [AutoNetworkedField]
    public TimeSpan MaxDelay;

    [DataField(required: true)] [AutoNetworkedField]
    public TimeSpan MinDelay;
}
