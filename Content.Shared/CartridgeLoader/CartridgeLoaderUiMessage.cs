// SPDX-FileCopyrightText: 2022 Julian Giebel <juliangiebel@live.de>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Serialization;

namespace Content.Shared.CartridgeLoader;

[Serializable] [NetSerializable]
public sealed class CartridgeLoaderUiMessage : BoundUserInterfaceMessage
{
    public readonly CartridgeUiMessageAction Action;
    public readonly NetEntity CartridgeUid;

    public CartridgeLoaderUiMessage(NetEntity cartridgeUid, CartridgeUiMessageAction action)
    {
        CartridgeUid = cartridgeUid;
        Action = action;
    }
}

[Serializable] [NetSerializable]
public enum CartridgeUiMessageAction
{
    Activate,
    Deactivate,
    Install,
    Uninstall,
    UIReady,
}
