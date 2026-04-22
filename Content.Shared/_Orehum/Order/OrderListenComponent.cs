using Robust.Shared.GameStates;
using Robust.Shared.Utility;

namespace Content.Shared._Orehum;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class OrderListenComponent : Component
{
    [DataField, AutoNetworkedField]
    public SpriteSpecifier? Icon;
}
