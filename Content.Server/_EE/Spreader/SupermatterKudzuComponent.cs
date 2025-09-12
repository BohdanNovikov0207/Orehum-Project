namespace Content.Server._EE.Spreader;

[RegisterComponent]
public sealed partial class SupermatterKudzuComponent : Component
{
    [DataField]
    public int GrowthLevel = 1;

    [DataField]
    public float SpreadChance = 1f;
    [DataField]
    public float GrowthTickChance = 1f;

    [DataField]
    public int SpriteVariants = 3;
}
