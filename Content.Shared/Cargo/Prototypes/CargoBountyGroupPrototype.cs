using Robust.Shared.Prototypes;

namespace Content.Shared.Cargo.Prototypes;

/// <summary>
/// Used to categorize bounties for different purposes
/// </summary>
[Prototype]
public sealed class CargoBountyGroupPrototype : IPrototype
{
    /// <inheritdoc />
    [IdDataField]
    public string ID { get; } = default!;
}
