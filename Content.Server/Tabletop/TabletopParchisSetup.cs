// SPDX-FileCopyrightText: 2021 Fishfish458 <47410468+Fishfish458@users.noreply.github.com>
// SPDX-FileCopyrightText: 2021 Vera Aguilera Puerto <6766154+Zumorica@users.noreply.github.com>
// SPDX-FileCopyrightText: 2021 Vera Aguilera Puerto <gradientvera@outlook.com>
// SPDX-FileCopyrightText: 2021 Visne <39844191+Visne@users.noreply.github.com>
// SPDX-FileCopyrightText: 2021 Wrexbe <wrexbe@protonmail.com>
// SPDX-FileCopyrightText: 2021 fishfish458 <fishfish458>
// SPDX-FileCopyrightText: 2021 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 eclips_e <67359748+Just-a-Unity-Dev@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using JetBrains.Annotations;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Server.Tabletop;

[UsedImplicitly]
public sealed partial class TabletopParchisSetup : TabletopSetup
{
    [DataField("redPiecePrototype", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string RedPiecePrototype { get; private set; } = "RedTabletopPiece";

    [DataField("greenPiecePrototype", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string GreenPiecePrototype { get; private set; } = "GreenTabletopPiece";

    [DataField("yellowPiecePrototype", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string YellowPiecePrototype { get; private set; } = "YellowTabletopPiece";

    [DataField("bluePiecePrototype", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string BluePiecePrototype { get; private set; } = "BlueTabletopPiece";

    public override void SetupTabletop(TabletopSession session, IEntityManager entityManager)
    {
        var board = entityManager.SpawnEntity(BoardPrototype, session.Position);

        const float x1 = 6.25f;
        const float x2 = 4.25f;

        const float y1 = 6.25f;
        const float y2 = 4.25f;

        var center = session.Position;

        // Red pieces.
        var tempQualifier = entityManager.SpawnEntity(RedPiecePrototype, center.Offset(-x1, -y1));
        session.Entities.Add(tempQualifier);
        var tempQualifier1 = entityManager.SpawnEntity(RedPiecePrototype, center.Offset(-x1, -y2));
        session.Entities.Add(tempQualifier1);
        var tempQualifier2 = entityManager.SpawnEntity(RedPiecePrototype, center.Offset(-x2, -y1));
        session.Entities.Add(tempQualifier2);
        var tempQualifier3 = entityManager.SpawnEntity(RedPiecePrototype, center.Offset(-x2, -y2));
        session.Entities.Add(tempQualifier3);

        // Green pieces.
        var tempQualifier4 = entityManager.SpawnEntity(GreenPiecePrototype, center.Offset(x1, -y1));
        session.Entities.Add(tempQualifier4);
        var tempQualifier5 = entityManager.SpawnEntity(GreenPiecePrototype, center.Offset(x1, -y2));
        session.Entities.Add(tempQualifier5);
        var tempQualifier6 = entityManager.SpawnEntity(GreenPiecePrototype, center.Offset(x2, -y1));
        session.Entities.Add(tempQualifier6);
        var tempQualifier7 = entityManager.SpawnEntity(GreenPiecePrototype, center.Offset(x2, -y2));
        session.Entities.Add(tempQualifier7);

        // Yellow pieces.
        var tempQualifier8 = entityManager.SpawnEntity(YellowPiecePrototype, center.Offset(x1, y1));
        session.Entities.Add(tempQualifier8);
        var tempQualifier9 = entityManager.SpawnEntity(YellowPiecePrototype, center.Offset(x1, y2));
        session.Entities.Add(tempQualifier9);
        var tempQualifier10 = entityManager.SpawnEntity(YellowPiecePrototype, center.Offset(x2, y1));
        session.Entities.Add(tempQualifier10);
        var tempQualifier11 = entityManager.SpawnEntity(YellowPiecePrototype, center.Offset(x2, y2));
        session.Entities.Add(tempQualifier11);

        // Blue pieces.
        var tempQualifier12 = entityManager.SpawnEntity(BluePiecePrototype, center.Offset(-x1, y1));
        session.Entities.Add(tempQualifier12);
        var tempQualifier13 = entityManager.SpawnEntity(BluePiecePrototype, center.Offset(-x1, y2));
        session.Entities.Add(tempQualifier13);
        var tempQualifier14 = entityManager.SpawnEntity(BluePiecePrototype, center.Offset(-x2, y1));
        session.Entities.Add(tempQualifier14);
        var tempQualifier15 = entityManager.SpawnEntity(BluePiecePrototype, center.Offset(-x2, y2));
        session.Entities.Add(tempQualifier15);
    }
}
