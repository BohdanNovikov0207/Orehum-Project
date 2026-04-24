using Content.Shared.Guidebook;
using Robust.Shared.Prototypes;

namespace Content.Shared._White.SpeciesDictionary;

[Prototype]
public sealed class SpeciesDictionaryPrototype : IPrototype
{
    [DataField] public ProtoId<GuideEntryPrototype> GuidePrototype { get; private set; } = default!;
    [DataField] public ProtoId<SpeciesDictionaryGroupPrototype> GroupPrototype { get; private set; } = default!;
    [IdDataField] public string ID { get; } = default!;
}

[Prototype]
public sealed class SpeciesDictionaryGroupPrototype : IPrototype
{
    [DataField] public Color? Color { get; private set; }
    [DataField] public int Weight { get; private set; }
    [IdDataField] public string ID { get; } = default!;
}
