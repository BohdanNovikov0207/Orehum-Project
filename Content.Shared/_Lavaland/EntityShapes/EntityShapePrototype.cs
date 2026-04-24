using Content.Shared._Lavaland.EntityShapes.Shapes;
using Robust.Shared.Prototypes;

namespace Content.Shared._Lavaland.EntityShapes;

/// <summary>
/// Contains one or multiple EntityShapes to create a pattern.
/// </summary>
[Prototype]
public sealed class EntityShapePrototype : IPrototype
{
    [DataField(required: true)]
    public EntityShape Shape = default!;

    /// <inheritdoc />
    [IdDataField]
    public string ID { get; } = default!;
}
