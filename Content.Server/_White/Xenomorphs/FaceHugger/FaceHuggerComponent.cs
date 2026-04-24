using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.Body.Part;
using Content.Shared.Damage;
using Content.Shared.Whitelist;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Server._White.Xenomorphs.FaceHugger;

[RegisterComponent]
public sealed partial class FaceHuggerComponent : Component
{
    // Goobstation end

    [ViewVariables]
    public bool Active = true;

    [DataField]
    public TimeSpan AttachAttemptDelay = TimeSpan.FromSeconds(5);

    [DataField]
    public EntityWhitelist? Blacklist;

    [DataField]
    public string BlockingSlot = "head";

    [DataField]
    public DamageSpecifier DamageOnImpact = new();

    [DataField]
    public DamageSpecifier DamageOnInfect = new();

    [ViewVariables]
    public TimeSpan InfectIn = TimeSpan.Zero;

    [DataField]
    public (BodyPartType Type, BodyPartSymmetry Symmetry) InfectionBodyPart =
        (BodyPartType.Chest, BodyPartSymmetry.None);

    [DataField]
    public EntProtoId? InfectionPrototype = "XenomorphInfection";

    [DataField]
    public string InfectionSlotId = "xenomorph_larva";

    [DataField]
    public TimeSpan InitialInjectionDelay = TimeSpan.FromSeconds(5); // Delay before the first injection

    [DataField]
    public TimeSpan InjectionInterval = TimeSpan.FromSeconds(5); // How often to inject chemicals

    [DataField]
    public TimeSpan KnockdownTime = TimeSpan.FromSeconds(5);

    [DataField]
    public DamageSpecifier MaskBlockDamage = new()
    {
        DamageDict = new Dictionary<string, FixedPoint2>
        {
            ["Slash"] = 5,
        },
    };

    [DataField]
    public SoundSpecifier MaskBlockSound = new SoundCollectionSpecifier("MetalThud");

    [DataField]
    public TimeSpan MaxInfectTime = TimeSpan.FromSeconds(20);

    [DataField]
    public TimeSpan
        MaxRestTime =
            TimeSpan.FromSeconds(5); // Goobstation - 20 to 5. Facehuggers shouldn't take that long to recover.

    [DataField]
    public float MinChemicalThreshold = 0f; // Minimum amount of the chemical required to prevent additional injections

    [DataField]
    public TimeSpan MinInfectTime = TimeSpan.FromSeconds(10);

    [DataField]
    public TimeSpan
        MinRestTime =
            TimeSpan.FromSeconds(3); // Must be less than MaxRestTime (makes facehugger jump randomly between max & min)

    [ViewVariables]
    public TimeSpan NextInjectionTime = TimeSpan.Zero; // Saves the time of the next injection

    [ViewVariables]
    public TimeSpan RestIn = TimeSpan.Zero;

    // Goobstation start
    [DataField]
    public string SleepChem = "Nocturine";

    [DataField]
    public float SleepChemAmount = 10f;

    [DataField]
    public string Slot = "mask";

    [DataField]
    public SoundSpecifier SoundOnImpact = new SoundCollectionSpecifier("MetalThud");
}
