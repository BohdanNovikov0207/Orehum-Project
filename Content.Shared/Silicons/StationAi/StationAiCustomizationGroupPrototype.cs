using Robust.Shared.Prototypes;

namespace Content.Shared.Silicons.StationAi;

/// <summary>
/// Holds data for customizing the appearance of station AIs.
/// </summary>
[Prototype]
public sealed class StationAiCustomizationGroupPrototype : IPrototype
{
    /// <summary>
    /// The type of customization that is associated with this group.
    /// </summary>
    [DataField]
    public StationAiCustomizationType Category = StationAiCustomizationType.CoreIconography;

    /// <summary>
    /// The localized name of the customization.
    /// </summary>
    [DataField(required: true)]
    public LocId Name;

    /// <summary>
    /// The list of prototypes associated with the customization group.
    /// </summary>
    [DataField(required: true)]
    public List<ProtoId<StationAiCustomizationPrototype>> ProtoIds = new();

    [IdDataField]
    public string ID { get; } = string.Empty;
}
