namespace Content.Goobstation.Shared.MisandryBox.Thunderdome;

[RegisterComponent]
public sealed partial class ThunderdomePlayerComponent : Component
{
    [DataField]
    public int CurrentStreak;

    [DataField]
    public int Deaths;

    [DataField]
    public int Kills;

    public EntityUid? LastAttacker;

    [DataField]
    public EntityUid? RuleEntity;

    [DataField]
    public int WeaponSelection;
}
