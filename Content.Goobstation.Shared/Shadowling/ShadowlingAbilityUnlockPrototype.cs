using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Shadowling;

[Prototype]
public sealed class ShadowlingAbilityUnlockPrototype : IPrototype
{
    [DataField]
    public ComponentRegistry? AddComponents;

    [DataField]
    public ComponentRegistry? RemoveComponents;

    [DataField("count")]
    public int UnlockAtThralls;

    /// <inheritdoc />
    [IdDataField]
    public string ID { get; } = default!;
}
