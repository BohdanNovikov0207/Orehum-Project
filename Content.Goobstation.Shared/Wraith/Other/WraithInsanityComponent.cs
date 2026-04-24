using Robust.Shared.GameStates;

namespace Content.Goobstation.Shared.Wraith.Other;

[RegisterComponent] [NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class WraithInsanityComponent : Component
{
    [DataField] [AutoNetworkedField]
    public Color Color = Color.Red;

    [DataField] [AutoNetworkedField]
    public float Radius = 1f;

    [DataField] [AutoNetworkedField]
    public float Speed = 1f;
}
