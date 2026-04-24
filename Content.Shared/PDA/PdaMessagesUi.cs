// SPDX-FileCopyrightText: 2023 0x6273 <0x40@keemail.me>
// SPDX-FileCopyrightText: 2023 MishaUnity <81403616+MishaUnity@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Serialization;

namespace Content.Shared.PDA;

[Serializable] [NetSerializable]
public sealed class PdaToggleFlashlightMessage : BoundUserInterfaceMessage
{
}

[Serializable] [NetSerializable]
public sealed class PdaShowRingtoneMessage : BoundUserInterfaceMessage
{
}

[Serializable] [NetSerializable]
public sealed class PdaShowUplinkMessage : BoundUserInterfaceMessage
{
}

[Serializable] [NetSerializable]
public sealed class PdaLockUplinkMessage : BoundUserInterfaceMessage
{
}

[Serializable] [NetSerializable]
public sealed class PdaShowMusicMessage : BoundUserInterfaceMessage
{
}

[Serializable] [NetSerializable]
public sealed class PdaRequestUpdateInterfaceMessage : BoundUserInterfaceMessage
{
}
