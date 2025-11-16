namespace Content.Server._Lavaland.Mobs.AshDrake.Components;

[RegisterComponent]
public sealed partial class AshDrakeBossComponent : MegafaunaComponent
{
    public const float TileDamageDelay = 0.8f;

    [ViewVariables]
    public EntityUid? ConnectedTelepad;

    [DataField]
    public float AttackCooldown = 6f * TileDamageDelay;

    [ViewVariables]
    public float AttackTimer = 10f * TileDamageDelay;

    [DataField]
    public float MinAttackCooldown = 2f * TileDamageDelay;

    [DataField]
    public float InterActionDelay = 3 * TileDamageDelay * 1000f;

    [DataField]
    public bool IsBreathingFireCircle = false;

    [DataField]
    public bool IsFireJump = false;

    [DataField]
    public bool IsFireArena = false;

    [DataField]

    // на будущее
    public Dictionary<AshDrakeAttackType, float> Attacks = new()
    {
        { AshDrakeAttackType.BreathingFire, 0f },
        { AshDrakeAttackType.BreathingFireCircle, 0f },
        { AshDrakeAttackType.FireJump, 0f },
        { AshDrakeAttackType.FireArea, 0f }
    };

    /// <summary>
    /// Attack that was done previously, so we don't repeat it over and over.
    /// </summary>
    [DataField]
    public AshDrakeAttackType PreviousAttack;
}

public enum AshDrakeAttackType
{
    Invalid,
    BreathingFire,
    BreathingFireCircle,
    FireJump,
    FireArea
}
