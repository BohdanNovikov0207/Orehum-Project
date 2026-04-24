using Robust.Shared.Prototypes;

namespace Content.Shared._Lavaland.Procedural.Prototypes;

/// <summary>
/// A simple wrapper that contains information about the planet, its static grid layout and a random ruin pool.
/// </summary>
[Prototype]
public sealed class LavalandMapPrototype : IPrototype
{
    [DataField]
    public ProtoId<LavalandLayoutPrototype>? Layout;

    [DataField(required: true)]
    public ProtoId<LavalandPlanetPrototype> Planet = "Lavaland";

    [DataField]
    public ProtoId<LavalandRuinPoolPrototype>? Ruins;

    /// <inheritdoc />
    [IdDataField]
    public string ID { get; } = default!;
}
