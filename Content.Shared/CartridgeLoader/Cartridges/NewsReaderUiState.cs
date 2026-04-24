// SPDX-FileCopyrightText: 2023 MishaUnity <81403616+MishaUnity@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Julian Giebel <juliangiebel@live.de>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Content.Shared.MassMedia.Systems;
using Robust.Shared.Serialization;

namespace Content.Shared.CartridgeLoader.Cartridges;

[Serializable] [NetSerializable]
public sealed class NewsReaderBoundUserInterfaceState : BoundUserInterfaceState
{
    public NewsArticle Article;
    public bool NotificationOn;
    public int TargetNum;
    public int TotalNum;

    public NewsReaderBoundUserInterfaceState(NewsArticle article, int targetNum, int totalNum, bool notificationOn)
    {
        Article = article;
        TargetNum = targetNum;
        TotalNum = totalNum;
        NotificationOn = notificationOn;
    }
}

[Serializable] [NetSerializable]
public sealed class NewsReaderEmptyBoundUserInterfaceState : BoundUserInterfaceState
{
    public bool NotificationOn;

    public NewsReaderEmptyBoundUserInterfaceState(bool notificationOn)
    {
        NotificationOn = notificationOn;
    }
}
