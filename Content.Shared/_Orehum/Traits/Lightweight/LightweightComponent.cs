namespace Content.Shared._Orehum.Traits.Lightweight;


/// <summary>
/// Makes it easier to carry an entity with this component.
/// </summary>
[RegisterComponent]
public sealed partial class LightweightComponent : Component
{
    [DataField] public float PickupSpeedMultiplier = 1.5f;
}
