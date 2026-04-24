using Robust.Shared.Prototypes;

namespace Content.Shared.Backmen.GhostTheme;

[Prototype("ghostTheme", -2)]
public sealed class GhostThemePrototype : IPrototype
{
    [DataField]
    public LocId Name;

    [DataField("components")]
    [AlwaysPushInheritance]
    public ComponentRegistry Components { get; } = new();

    /// <inheritdoc />
    [IdDataField]
    public string ID { get; } = default!;
}
