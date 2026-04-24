using Robust.Shared.Network;

namespace Content.Goobstation.Shared.MisandryBox.Thunderdome;

[RegisterComponent]
public sealed partial class ThunderdomeOriginalBodyComponent : Component
{
    [DataField]
    public NetUserId Owner;
}
