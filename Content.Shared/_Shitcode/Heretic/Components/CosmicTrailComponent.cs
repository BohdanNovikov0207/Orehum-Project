using Robust.Shared.GameStates;

namespace Content.Shared._Shitcode.Heretic.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentPause]
public sealed partial class CosmicTrailComponent : Component
{
    [DataField]
    public float CosmicFieldLifetime = 5f;

    [DataField]
    public TimeSpan CosmicFieldPeriod = TimeSpan.FromSeconds(0.1f);

    [DataField]
    public float CosmicFieldRadius = 0.5f;

    [DataField] [AutoPausedField]
    public TimeSpan NextCosmicFieldTime = TimeSpan.Zero;

    [DataField]
    public int Strength;
}
