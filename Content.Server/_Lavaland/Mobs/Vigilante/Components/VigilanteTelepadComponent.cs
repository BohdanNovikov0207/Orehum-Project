using Robust.Shared.Prototypes;

namespace Content.Server._Lavaland.Mobs.Vigilante.Components;

[RegisterComponent]
public sealed partial class VigilanteTelepadComponent : Component
{
    [ViewVariables]
    public bool Enabled;

    [ViewVariables]
    public List<EntityUid> Walls = new();

    [DataField]
    public int Radius = 0;

    [DataField]
    public EntProtoId VigilantePrototype = "LavalandBossVigilante";

    [DataField]
    public EntProtoId WallPrototype = "LavalandAshDrakeWallBasaltCobblebrick";

    [DataField]
    public EntityUid? ConnectedVigilante;
}
