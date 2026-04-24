using Robust.Shared.Prototypes;

namespace Content.Server._White.Spawners.Components;

[RegisterComponent]
public sealed partial class AreaSpawnerComponent : Component
{
    [DataField]
    public float MaxTime = 5f;

    [DataField]
    public float MinTime = 1f;

    [DataField]
    public int Radius = 3;

    [ViewVariables]
    public TimeSpan SpawnAt = TimeSpan.Zero;

    [DataField]
    public TimeSpan SpawnDelay = TimeSpan.FromSeconds(3);

    [ViewVariables]
    public List<EntityUid> Spawneds = new();

    [DataField]
    public EntProtoId SpawnPrototype;
}
