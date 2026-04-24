using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Changeling;

/// <summary>
/// Holds the necessary information about the starting evolutions of a changeling.
/// </summary>
[Prototype]
public sealed class ChangelingStartingEvolutionPrototype : IPrototype
{
    /// <summary>
    /// The components that the changeling starts with on MapInit
    /// </summary>
    [DataField]
    public ComponentRegistry Components { get; private set; } = default!;

    [ViewVariables]
    [IdDataField]
    public string ID { get; } = default!;
}
