using Robust.Shared.GameStates;

namespace Content.Shared._Orehum.Generic;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class FadeAndDeleteComponent : Component
{
    [DataField, AutoNetworkedField,]
    public float RemoveAlphaPerSecond = 0.08f;

    [DataField, AutoNetworkedField,]
    public float Accumulator = 0f;

    [DataField, AutoNetworkedField,]
    public float ServerSideAlpha = 1f;

    [DataField, AutoNetworkedField,]
    public Color Modulate = Color.White;
}
