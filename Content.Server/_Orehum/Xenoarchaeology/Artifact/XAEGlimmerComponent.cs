namespace Content.Server._Orehum.Xenoarchaeology.Artifact;

[RegisterComponent]
public sealed partial class XAEGlimmerComponent : Component
{
    [DataField("max", required: true)]
    public float Max;

    [DataField("min", required: true)]
    public float Min;
}
