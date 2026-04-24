using Content.Shared.EntityEffects;

namespace Content.Goobstation.Shared.Disease.Components;

/// <summary>
/// A disease effect that executes reagent effects.
/// Severity from DiseaseEffectComponent automatically scales the effect strength.
/// </summary>
[RegisterComponent]
public sealed partial class DiseaseReagentEffectComponent : ScalingDiseaseEffect
{
    /// <summary>
    /// The reagent effects to execute when Rthis disease effect triggers
    /// </summary>
    [DataField(required: true, serverOnly: true)]
    public List<EntityEffect> Effects = [];
}
