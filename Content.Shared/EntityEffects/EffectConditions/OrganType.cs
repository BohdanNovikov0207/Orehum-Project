// using Content.Server.Body.Components;

using Content.Shared.Body.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Shared.EntityEffects.EffectConditions;

/// <summary>
/// Requires that the metabolizing organ is or is not tagged with a certain MetabolizerType
/// </summary>
public sealed partial class OrganType : EventEntityEffectCondition<OrganType>
{
    /// <summary>
    /// Does this condition pass when the organ has the type, or when it doesn't have the type?
    /// </summary>
    [DataField]
    public bool ShouldHave = true;

    [DataField(required: true, customTypeSerializer: typeof(PrototypeIdSerializer<MetabolizerTypePrototype>))]
    public string Type = default!;

    public override string GuidebookExplanation(IPrototypeManager prototype) =>
        Loc.GetString("reagent-effect-condition-guidebook-organ-type",
            ("name", prototype.Index<MetabolizerTypePrototype>(Type).LocalizedName),
            ("shouldhave", ShouldHave));
}
