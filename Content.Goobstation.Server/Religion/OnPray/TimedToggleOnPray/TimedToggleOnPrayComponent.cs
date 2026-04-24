using Robust.Shared.Audio;

namespace Content.Goobstation.Server.Religion.OnPray.TimedToggleOnPray;

[RegisterComponent]
public sealed partial class TimedToggleOnPrayComponent : Component
{
    [DataField] [AutoNetworkedField]
    public bool Activated = false;

    [DataField]
    public float Duration = 1f;

    [ViewVariables(VVAccess.ReadWrite)] [DataField] [AutoNetworkedField]
    public bool Predictable = true;

    [ViewVariables(VVAccess.ReadWrite)] [DataField] [AutoNetworkedField]
    public SoundSpecifier? SoundActivate;

    [ViewVariables(VVAccess.ReadWrite)] [DataField] [AutoNetworkedField]
    public SoundSpecifier? SoundDeactivate;

    [DataField]
    public TimeSpan Time;

    [DataField]
    public bool TimerRun = false;

    [DataField]
    public bool UseDelayOnPray = true;
}
