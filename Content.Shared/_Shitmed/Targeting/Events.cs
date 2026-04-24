// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 gluesniffler <159397573+gluesniffler@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Serialization;

namespace Content.Shared._Shitmed.Targeting.Events;

[Serializable] [NetSerializable]
public sealed class TargetChangeEvent : EntityEventArgs
{
    public TargetChangeEvent(NetEntity uid, TargetBodyPart bodyPart)
    {
        Uid = uid;
        BodyPart = bodyPart;
    }

    public NetEntity Uid { get; }
    public TargetBodyPart BodyPart { get; }
}

[Serializable] [NetSerializable]
public sealed class TargetIntegrityChangeEvent : EntityEventArgs
{
    public TargetIntegrityChangeEvent(NetEntity uid, bool refreshUi = true)
    {
        Uid = uid;
        RefreshUi = refreshUi;
    }

    public NetEntity Uid { get; }
    public bool RefreshUi { get; }
}
