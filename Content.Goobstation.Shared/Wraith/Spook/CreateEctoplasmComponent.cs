using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Wraith.Spook;

[RegisterComponent] [NetworkedComponent]
public sealed partial class CreateEctoplasmComponent : Component
{
    /// <summary>
    /// Minimum and Maximum amounts of ectoplasm to spawn
    /// </summary>
    [DataField]
    public Vector2i AmountMinMax = new(3, 6);

    [DataField]
    public EntProtoId EctoplasmProto = "EctoplasmGoon";

    [DataField]
    public int SearchRange = 10;
}
