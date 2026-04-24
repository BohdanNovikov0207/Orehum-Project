using Content.Shared.Damage;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Shared._DV.CosmicCult.Components;

[RegisterComponent]
public sealed partial class CosmicGlyphComponent : Component
{
    /// <summary>
    /// Damage dealt on glyph activation.
    /// </summary>
    [DataField]
    public DamageSpecifier ActivationDamage = new();

    [DataField]
    public float ActivationRange = 1.55f;

    [DataField]
    public bool CanBeErased = true;

    [DataField]
    public SoundSpecifier GylphSFX = new SoundPathSpecifier("/Audio/_DV/CosmicCult/glyph_trigger.ogg");

    [DataField]
    public EntProtoId GylphVFX = "CosmicGenericVFX";

    [DataField]
    public int RequiredCultists = 1;
}

public sealed class TryActivateGlyphEvent(EntityUid user, HashSet<Entity<CosmicCultComponent>> cultists)
    : CancellableEntityEventArgs
{
    public HashSet<Entity<CosmicCultComponent>> Cultists = cultists;
    public EntityUid User = user;
}
