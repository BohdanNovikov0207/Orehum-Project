using Robust.Shared.Prototypes;

namespace Content.Server._Lavaland.Mobs.AshDrake.Components;

[RegisterComponent]
public sealed partial class AshDrakeTelepadComponent : Component
{
    [ViewVariables]
    public bool Enabled;

    [ViewVariables]
    public List<EntityUid> Walls = new();

    [DataField]
    public int Radius = 2;

    [DataField]
    public EntProtoId AshDrakePrototype = "LavalandBossAshDrake";

    [DataField]
    public EntProtoId WallPrototype = "LavalandAshDrakeWallBasaltCobblebrick";

    [DataField]
    public EntityUid? ConnectedAshDrake;
}
