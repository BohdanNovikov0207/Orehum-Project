using Content.Shared.Damage;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Shared._Shitcode.Heretic.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class FireBlastedComponent : Component
{
    [ViewVariables(VVAccess.ReadWrite)]
    public float Accumulator;

    [DataField]
    public TimeSpan BeamTime = TimeSpan.FromSeconds(2);

    [DataField]
    public EntProtoId BonusEffect = "EffectVolcanoExplosion";

    [DataField]
    public float BonusFireStacks = 3f;

    [DataField]
    public TimeSpan BonusKnockdownTime = TimeSpan.FromSeconds(0.8f);

    [DataField]
    public float BonusRange = 1f;

    [DataField]
    public int BouncesForBonusEffect = 4;

    [DataField]
    public float CollisionFireStacks = 0.5f;

    [DataField]
    public float Damage = 1f;

    [DataField]
    public DamageSpecifier FireBlastBeamCollideDamage = new()
    {
        DamageDict =
        {
            { "Heat", 2.5f },
        },
    };


    [DataField]
    public EntProtoId FireBlastBeamDataId = "fireblast";

    [DataField]
    public SpriteSpecifier FireBlastBeamSprite =
        new SpriteSpecifier.Rsi(new ResPath("/Textures/_Goobstation/Heretic/Effects/effects.rsi"), "solar_beam");

    [DataField]
    public DamageSpecifier FireBlastBonusDamage = new()
    {
        DamageDict =
        {
            { "Heat", 15f },
        },
    };

    [DataField]
    public DamageSpecifier FireBlastDamage = new()
    {
        DamageDict =
        {
            { "Heat", 20f },
        },
    };

    [DataField]
    public float FireBlastRange = 5f;

    [DataField]
    public float FireProtectionPenetration = 0.5f;

    [DataField]
    public float FireStacks = 4f;

    [DataField] [AutoNetworkedField]
    public HashSet<EntityUid> HitEntities = new();

    [DataField]
    public int MaxBounces = 4;

    [DataField]
    public bool ShouldBounce = true;

    [DataField]
    public SoundSpecifier? Sound = new SoundPathSpecifier("/Audio/Magic/fireball.ogg");

    [DataField]
    public SpriteSpecifier Sprite =
        new SpriteSpecifier.Rsi(new ResPath("_Goobstation/Heretic/Effects/effects.rsi"), "blessed");

    [DataField]
    public float StaminaDamageMultiplier = 2f;

    [DataField]
    public float TickInterval = 0.2f;
}

public enum FireBlastedKey : byte
{
    Key,
}
