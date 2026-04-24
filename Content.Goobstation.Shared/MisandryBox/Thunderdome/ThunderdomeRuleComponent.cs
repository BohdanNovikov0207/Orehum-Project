using Robust.Shared.Map;
using Robust.Shared.Network;

namespace Content.Goobstation.Shared.MisandryBox.Thunderdome;

[DataDefinition]
public sealed partial class ThunderdomeWeaponLoadout
{
    [DataField(required: true)]
    public string Category = string.Empty;

    [DataField]
    public string Description = string.Empty;

    [DataField(required: true)]
    public string Gear = string.Empty;

    [DataField(required: true)]
    public string Name = string.Empty;

    [DataField(required: true)]
    public string Sprite = string.Empty;
}

[RegisterComponent]
public sealed partial class ThunderdomeRuleComponent : Component
{
    [DataField]
    public bool Active;

    [DataField]
    public HashSet<EntityUid> ArenaGrids = new();

    [DataField]
    public MapId? ArenaMap;

    [DataField]
    public TimeSpan CleanupInterval = TimeSpan.FromSeconds(25);

    [DataField]
    public Dictionary<NetUserId, int> Deaths = new();

    [DataField]
    public string Gear = "ThunderdomeBaseGear";

    [DataField]
    public Dictionary<NetUserId, int> Kills = new();

    [DataField]
    public TimeSpan NextCleanup;

    [DataField]
    public HashSet<NetEntity> Players = new();

    [DataField]
    public float SweepDespawnTime = 10f;

    [DataField]
    public List<ThunderdomeWeaponLoadout> WeaponLoadouts = new();
}
