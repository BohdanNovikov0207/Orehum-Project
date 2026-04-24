using System.Text.Json.Serialization;
using Content.Goobstation.Maths.FixedPoint;
using Content.Shared._Shitmed.Medical.Surgery.Consciousness;
using Content.Shared._Shitmed.Medical.Surgery.Consciousness.Systems;
using JetBrains.Annotations;
using Robust.Shared.Prototypes;

namespace Content.Shared.EntityEffects.Effects;

[UsedImplicitly]
public sealed partial class AdjustConsciousness : EntityEffect
{
    [DataField]
    [JsonPropertyName("allowNewModifiers")]
    public bool AllowNewModifiers = true;

    [DataField(required: true)]
    [JsonPropertyName("amount")]
    public FixedPoint2 Amount = default!;

    [DataField]
    [JsonPropertyName("identifier")]
    public string Identifier = "ConsciousnessModifier";

    [DataField]
    [JsonPropertyName("modifierType")]
    public ConsciousnessModType ModifierType = ConsciousnessModType.Generic;

    [DataField(required: true)]
    [JsonPropertyName("time")]
    public TimeSpan Time = default!;

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => Loc.GetString("reagent-effect-guidebook-adjust-consciousness");

    public override void Effect(EntityEffectBaseArgs args)
    {
        var scale = FixedPoint2.New(1);

        if (args is EntityEffectReagentArgs reagentArgs)
            scale = reagentArgs.Quantity * reagentArgs.Scale;

        if (!args.EntityManager.System<ConsciousnessSystem>().TryGetNerveSystem(args.TargetEntity, out var nerveSys))
            return;

        if (AllowNewModifiers)
        {
            if (!args.EntityManager.System<ConsciousnessSystem>()
                    .EditConsciousnessModifier(args.TargetEntity,
                        nerveSys.Value.Owner,
                        Amount * scale,
                        Identifier,
                        Time))
            {
                args.EntityManager.System<ConsciousnessSystem>()
                    .AddConsciousnessModifier(args.TargetEntity,
                        nerveSys.Value.Owner,
                        Amount * scale,
                        Identifier,
                        ModifierType,
                        Time);
            }
        }
        else
        {
            args.EntityManager.System<ConsciousnessSystem>()
                .EditConsciousnessModifier(args.TargetEntity, nerveSys.Value.Owner, Amount * scale, Identifier, Time);
        }
    }
}
