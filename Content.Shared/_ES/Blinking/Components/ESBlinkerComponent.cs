using Robust.Shared.GameStates;
using Robust.Shared.Serialization;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared._ES.Blinking.Components;

/// <summary>
/// Makes a character blink. That's it.
/// </summary>
[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState] [AutoGenerateComponentPause]
[Access(typeof(ESSharedBlinkingSystem))]
public sealed partial class ESBlinkerComponent : Component
{
    [DataField] [AutoNetworkedField]
    public bool Enabled = true;

    [DataField]
    public TimeSpan MaxBlinkDelay = TimeSpan.FromSeconds(10);

    [DataField]
    public TimeSpan MinBlinkDelay = TimeSpan.FromSeconds(2);

    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer))] [AutoNetworkedField] [AutoPausedField]
    public TimeSpan NextBlinkTime;
}

[Serializable] [NetSerializable]
public enum ESBlinkVisuals : byte
{
    EyesClosed,
}
