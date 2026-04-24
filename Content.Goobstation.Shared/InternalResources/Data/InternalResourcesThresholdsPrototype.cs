using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.InternalResources.Data;

/// <summary>
/// Prototype for an internal resource's thresholds.
/// </summary>
[Prototype]
public sealed class InternalResourcesThresholdsPrototype : IPrototype
{
    /// <summary>
    /// Float is the percentage from 0 to 1.
    /// Bool is if the threshold was met.
    /// </summary>
    [DataField]
    public Dictionary<string, (float, bool)>? Thresholds;

    [IdDataField]
    public string ID { get; } = default!;
}
