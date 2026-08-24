using Robust.Shared.Serialization;

namespace Content.Shared._Orehum.VendingMachines;

[Serializable, NetSerializable]
public enum VendingMachineKeypadSound : byte
{
    beep,
    success,
    error,
    timeout
}

[Serializable, NetSerializable]
public sealed class VendingMachineKeypadAudioMessage(VendingMachineKeypadSound soundType, float pitch = 1f) : BoundUserInterfaceMessage
{
    public readonly VendingMachineKeypadSound SoundType = soundType;
    public readonly float Pitch = pitch;
}
