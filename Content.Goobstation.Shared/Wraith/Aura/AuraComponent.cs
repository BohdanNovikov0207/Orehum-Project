using Robust.Shared.GameStates;

namespace Content.Goobstation.Shared.Wraith.Aura;

/// <summary>
/// Creates an aura around you.
/// </summary>
[RegisterComponent] [NetworkedComponent]
[AutoGenerateComponentState]
public sealed partial class AuraComponent : Component
{
    [DataField] [AutoNetworkedField]
    public Color AuraColor = Color.Black;

    /// <summary>
    /// The intensity of the aura
    /// </summary>
    [DataField] [AutoNetworkedField]
    public float AuraFarm = 0.5f;

    /// <summary>
    /// How much to distort the aura
    /// </summary>
    [DataField] [AutoNetworkedField]
    public float Distortion = 0.05f;
}
