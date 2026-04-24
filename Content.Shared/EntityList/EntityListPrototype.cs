// SPDX-FileCopyrightText: 2022 Paul Ritter <ritter.paul1@googlemail.com>
// SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Visne <39844191+Visne@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using System.Collections.Immutable;
using Robust.Shared.Prototypes;

namespace Content.Shared.EntityList;

[Prototype]
public sealed class EntityListPrototype : IPrototype
{
    [DataField]
    public ImmutableList<EntProtoId> Entities { get; } = ImmutableList<EntProtoId>.Empty;

    [ViewVariables]
    [IdDataField]
    public string ID { get; } = default!;

    public IEnumerable<EntityPrototype> GetEntities(IPrototypeManager? prototypeManager = null)
    {
        prototypeManager ??= IoCManager.Resolve<IPrototypeManager>();

        foreach (var entityId in Entities)
        {
            yield return prototypeManager.Index<EntityPrototype>(entityId);
        }
    }
}
