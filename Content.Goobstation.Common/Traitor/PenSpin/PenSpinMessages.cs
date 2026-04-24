using Robust.Shared.Serialization;

namespace Content.Goobstation.Common.Traitor.PenSpin;

[Serializable] [NetSerializable]
public sealed class PenSpinSubmitDegreeMessage : BoundUserInterfaceMessage
{
    public PenSpinSubmitDegreeMessage(int degree)
    {
        Degree = degree;
    }

    public int Degree { get; }
}

[Serializable] [NetSerializable]
public sealed class PenSpinResetMessage : BoundUserInterfaceMessage;

[Serializable] [NetSerializable]
public enum PenSpinUiKey : byte
{
    Key,
}
