using Robust.Shared.Serialization;

namespace Content.Shared._Orehum.AC;


[Serializable, NetSerializable]
public sealed class ACEvent(string version, string modifications) : EntityEventArgs
{
    public string Version = version;
    public string Modifications = modifications;
}
