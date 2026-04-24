using Content.Goobstation.Maths.FixedPoint;
using Content.Shared.Damage;
using Robust.Shared.Prototypes;

namespace Content.Shared._DV.CosmicCult.Components;

[RegisterComponent]
public sealed partial class CosmicGlyphAstralProjectionComponent : Component
{
    /// <summary>
    /// The duration of the astral projection
    /// </summary>
    [DataField]
    public TimeSpan AstralDuration = TimeSpan.FromSeconds(12);

    [DataField]
    public DamageSpecifier ProjectionDamage = new()
    {
        DamageDict = new Dictionary<string, FixedPoint2>
        {
            { "Asphyxiation", 20 },
        },
    };

    [DataField]
    public EntProtoId SpawnProjection = "MobCosmicAstralProjection";
}
