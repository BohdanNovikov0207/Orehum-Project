using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype.Array;

namespace Content.Goobstation.Shared.Medical;

/// <summary>
/// Prototype that holds a list of disability components.
/// </summary>
[Prototype]
public sealed class DisabilityListPrototype : IPrototype, IInheritingPrototype
{
    /// <summary>
    /// The relevant disability components.
    /// </summary>
    [DataField] [AlwaysPushInheritance]
    public ComponentRegistry Components { get; private set; } = default!;

    /// <inheritdoc />
    [ParentDataField(typeof(AbstractPrototypeIdArraySerializer<DisabilityListPrototype>))]
    public string[]? Parents { get; private set; }

    /// <inheritdoc />
    [AbstractDataField] [NeverPushInheritance]
    public bool Abstract { get; private set; }

    [ViewVariables] [IdDataField]
    public string ID { get; } = default!;
}
