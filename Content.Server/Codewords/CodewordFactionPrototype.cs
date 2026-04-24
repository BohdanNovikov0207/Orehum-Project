using Robust.Shared.Prototypes;

namespace Content.Server.Codewords;

/// <summary>
/// This is a prototype for easy access to codewords using identifiers instead of magic strings.
/// </summary>
[Prototype]
public sealed class CodewordFactionPrototype : IPrototype
{
    /// <summary>
    /// The generator to use for this faction.
    /// </summary>
    [DataField(required: true)]
    public ProtoId<CodewordGeneratorPrototype> Generator { get; } = default!;

    /// <inheritdoc />
    [IdDataField]
    public string ID { get; } = default!;
}
