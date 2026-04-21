using Robust.Shared.GameStates;
using Robust.Shared.Utility;

namespace Content.Shared._Orehum;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class OComponent : Component
{
    [DataField, AutoNetworkedField]
    public SpriteSpecifier? Icon;
}
