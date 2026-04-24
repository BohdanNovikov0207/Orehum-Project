// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Solstice <solsticeofthewinter@gmail.com>
// SPDX-FileCopyrightText: 2025 SolsticeOfTheWinter <solsticeofthewinter@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Polymorph;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Devil.Contract;

[Prototype("clause")]
public sealed class DevilClausePrototype : IPrototype
{
    [DataField]
    public ComponentRegistry? AddedComponents;

    [DataField(required: true)]
    public int ClauseWeight;

    [DataField]
    public string? DamageModifierSet;

    [DataField]
    public BaseDevilContractEvent? Event;

    [DataField]
    public List<EntProtoId>? Implants;

    // CorvaxGoob Devil fix; Without localized name clauses don't work
    [DataField(required: true)]
    public string? Name;

    [DataField]
    public ProtoId<PolymorphPrototype>? Polymorph;

    [DataField]
    public ComponentRegistry? RemovedComponents;

    [DataField]
    public List<EntProtoId>? SpawnedItems;

    [IdDataField]
    public string ID { get; } = default!;
}

public enum SpecialCase : byte
{
    SoulOwnership,
    RemoveHand,
    RemoveLeg,
    RemoveOrgan,
}
