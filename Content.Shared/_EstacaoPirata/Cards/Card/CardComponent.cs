// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 RadsammyT <32146976+RadsammyT@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 coderabbitai[bot] <136622811+coderabbitai[bot]@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameStates;
using Robust.Shared.Serialization;
using Robust.Shared.Utility;

namespace Content.Shared._EstacaoPirata.Cards.Card;

/// <summary>
/// This is used for...
/// </summary>
[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class CardComponent : Component
{
    /// <summary>
    /// The back of the card
    /// </summary>
    [DataField("backSpriteLayers", true)]
    public List<SpriteSpecifier> BackSprite = [];

    /// <summary>
    /// If it is currently flipped. This is used to update sprite and name.
    /// </summary>
    [DataField("flipped", true)] [AutoNetworkedField]
    public bool Flipped = false;

    /// <summary>
    /// The front of the card
    /// </summary>
    [DataField("frontSpriteLayers", true)]
    public List<SpriteSpecifier> FrontSprite = [];


    /// <summary>
    /// The name of the card.
    /// </summary>
    [DataField("name", true)] [AutoNetworkedField]
    public string Name = "";
}

[Serializable] [NetSerializable]
public sealed class CardFlipUpdatedEvent(NetEntity card) : EntityEventArgs
{
    public NetEntity Card = card;
}
