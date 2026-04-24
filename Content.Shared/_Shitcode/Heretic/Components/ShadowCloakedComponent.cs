using System.Numerics;
using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.Chat.Prototypes;
using Content.Shared.Speech;
using Content.Shared.StatusEffect;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Shitcode.Heretic.Components;

[RegisterComponent] [NetworkedComponent]
public sealed partial class ShadowCloakedComponent : Component
{
    [DataField]
    public FixedPoint2 DamageBeforeReveal = 25;

    [DataField]
    public bool DebuffOnEarlyReveal;

    [DataField]
    public float DoAfterSlowdown = 3f;

    [DataField]
    public Vector2 EarlyRemoveMoveSpeedModifiers = new(0.75f, 0.75f);

    [DataField]
    public ProtoId<EmoteSoundsPrototype> EmoteSounds = "ShadowCloakEmoteSounds";

    [DataField]
    public TimeSpan ForceRevealCooldown = TimeSpan.FromMinutes(2f);

    [DataField]
    public TimeSpan KnockdownTime = TimeSpan.FromSeconds(0.5f);

    [DataField]
    public Vector2 MoveSpeedModifiers = new(1.25f, 1.25f);

    [DataField]
    public TimeSpan RevealCooldown = TimeSpan.FromMinutes(1f);

    [DataField]
    public ProtoId<StatusEffectPrototype> ShadowCloakAlert = "ShadowCloakAlertSE"; //todo goob migrate

    [DataField]
    public EntProtoId ShadowCloakEntity = "ShadowCloakEntity";

    [DataField]
    public TimeSpan SlowdownTime = TimeSpan.FromSeconds(10f);

    [DataField]
    public SoundSpecifier Sound = new SoundCollectionSpecifier("Curse");

    [DataField]
    public ProtoId<SpeechSoundsPrototype> SpeechSounds = "ShadowCloakSpeechSounds";

    [DataField]
    public ProtoId<SpeechVerbPrototype> SpeechVerb = "Hiss";

    [DataField]
    public FixedPoint2 SustainedDamage = 0f;

    [DataField]
    public FixedPoint2 SustainedDamageReductionRate = 1;

    [ViewVariables]
    public bool WasVisible = true;
}
