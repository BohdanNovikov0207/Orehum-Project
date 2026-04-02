using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Backmen.GhostTheme;

[Prototype]
public sealed partial class GhostThemePrototype : IPrototype
{
    /// <inheritdoc/>
    [IdDataField]
    public string ID { get; set; } = default!;

    [DataField]
    public LocId Name;

    [DataField("components")]
    [AlwaysPushInheritance]
    public ComponentRegistry Components { get; set; } = new();
}
