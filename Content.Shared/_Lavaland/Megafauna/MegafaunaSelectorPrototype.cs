using Content.Shared._Lavaland.Megafauna.Selectors;
using Robust.Shared.Prototypes;

namespace Content.Shared._Lavaland.Megafauna;

/// <summary>
/// Contains one or multiple EntityShapes to create a pattern.
/// </summary>
[Prototype]
public sealed class MegafaunaSelectorPrototype : IPrototype
{
    [DataField(required: true)]
    public MegafaunaSelector Selector = default!;

    /// <inheritdoc />
    [IdDataField]
    public string ID { get; } = default!;
}
