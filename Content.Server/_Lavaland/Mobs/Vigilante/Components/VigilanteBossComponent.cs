namespace Content.Server._Lavaland.Mobs.Vigilante.Components;

[RegisterComponent]
public sealed partial class VigilanteBossComponent : MegafaunaComponent
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
    public bool isStrangle = false;

    [DataField]
    public bool isFireDash = false;

    [DataField]

    // тут тоже на будущее
    public Dictionary<VigilanteAttackType, float> Attacks = new()
    {
        { VigilanteAttackType.Strangle, 0f },
        { VigilanteAttackType.FireDash, 0f },
        //{ VigilanteAttackType.CarpSpawn, 0f },
        { VigilanteAttackType.Blink, 0f }
    };

    /// <summary>
    /// Attack that was done previously, so we don't repeat it over and over.
    /// </summary>
    [DataField]
    public VigilanteAttackType PreviousAttack;
}

public enum VigilanteAttackType
{
    Invalid,
    FireDash,
    Strangle,
    Blink//,
    //CarpSpawn
}
