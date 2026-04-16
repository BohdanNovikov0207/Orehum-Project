using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Shared._Orehum.Sliding;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class SpeedSlidingComponent : Component
{
    [DataField, AutoNetworkedField]
    public float MinSlideSpeed = 4.6f;

    [DataField, AutoNetworkedField]
    public float SlideDistance = 8.5f;

    [DataField, AutoNetworkedField]
    public float SlideSpeed = 3.5f;

    [DataField, AutoNetworkedField]
    public SoundSpecifier? SlideSound;
}
