namespace Content.Server._EE.Spreader;

[RegisterComponent]
public sealed partial class SupermatterSpreaderGridComponent : Component
{
    [DataField]
    public float UpdateAccumulator = SupermatterSpreaderSystem.SpreadCooldownSeconds;
}
