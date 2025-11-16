namespace Content.Server._Orehum.AntiMeteorShield;

[RegisterComponent]
public sealed partial class AntiMeteorShieldGeneratorComponent : Component
{
    [DataField]
    public bool Powered = false;

    [DataField]
    public bool Shutdown = false;

    [DataField]
    public float ShutdownOnHealth = 10f;

    [DataField]
    public float EnableOnHealth = 200f;

    [DataField]
    public float Health = 400f; // TODO: Health должен быть FixedPoint

    [DataField]
    public float MaxHealth = 400f; // TODO: MaxHealth должен быть FixedPoint

    [DataField]
    public float DamageReducerModifier = 23f;

    [DataField]
    public float IgnoreDamage = 2f;

    [DataField]
    public float Accumulated = 0f;

    [DataField]
    public float SecondsToHealOneHp = 4.5f;
}
