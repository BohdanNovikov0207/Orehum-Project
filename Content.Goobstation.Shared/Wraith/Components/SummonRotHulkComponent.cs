using Content.Shared.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Wraith.Components;

[RegisterComponent] [NetworkedComponent]
public sealed partial class SummonRotHulkComponent : Component
{
    [DataField]
    public EntProtoId BuffRotHulkProto = "MobRotHulkBuff";

    /// <summary>
    /// The amount of trash needed to spawn a buffed hulk
    /// </summary>
    [DataField]
    public int BuffThreshold = 30;

    /// <summary>
    /// The maximum amount of trash needed to spawn a hulk
    /// </summary>
    [DataField]
    public int MaxTrash = 40;

    /// <summary>
    /// The minimum amount of trash needed to spawn a hulk
    /// </summary>
    [DataField]
    public int MinTrash = 10;

    [DataField]
    public EntProtoId RotHulkProto = "MobRotHulk";

    /// <summary>
    /// The search radius of this ability
    /// </summary>
    [DataField]
    public float SearchRadius = 3f;

    /// <summary>
    /// Unique tag that works only for entities from Trash Spawner
    /// </summary>
    [DataField]
    public ProtoId<TagPrototype> TrashTag = "RotHulkTrash";
}
