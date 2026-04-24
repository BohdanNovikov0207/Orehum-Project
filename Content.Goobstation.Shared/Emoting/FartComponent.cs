// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 JohnOakman <sremy2012@hotmail.fr>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
// SPDX-FileCopyrightText: 2025 jellygato <aly.jellygato@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Atmos;
using Content.Shared.Chat.Prototypes;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Goobstation.Shared.Emoting;

// use as a template
//[Serializable, NetSerializable, DataDefinition] public sealed partial class AnimationNameEmoteEvent : EntityEventArgs { }

[RegisterComponent] [NetworkedComponent]
public sealed partial class FartComponent : Component
{
    /// <summary>
    /// Path to the sound when you get bible smited
    /// </summary>
    [DataField]
    [Access(Other = AccessPermissions.ReadWriteExecute)]
    public SoundSpecifier BibleSmiteSnd = new SoundPathSpecifier("/Audio/_Goobstation/Effects/thunder_clap.ogg");

    [DataField]
    public ProtoId<EmotePrototype>? Emote;

    [DataField]
    public bool FartInhale;

    [DataField]
    public bool FartTimeout;

    [DataField]
    public Gas GasToFart = Gas.Ammonia;

    [DataField]
    public float MolesAmmoniaPerFart = 5f;

    [DataField]
    public bool SuperFarted;
}

[Serializable] [NetSerializable]
public sealed class FartComponentState : ComponentState
{
    public ProtoId<EmotePrototype>? Emote;
    public bool FartInhale;
    public bool FartTimeout;
    public bool SuperFarted;

    public FartComponentState(ProtoId<EmotePrototype>? emote, bool fartTimeout, bool fartInhale, bool superFarted)
    {
        Emote = emote;
        FartTimeout = fartTimeout;
        FartInhale = fartInhale;
        SuperFarted = superFarted;
    }
}

/// <summary>
/// Triggers after a fart 🦍💨
/// </summary>
public sealed class PostFartEvent : EntityEventArgs
{
    public readonly bool SuperFart;
    public readonly EntityUid Uid;

    public PostFartEvent(EntityUid uid, bool IsSuperFart = false)
    {
        Uid = uid;
        SuperFart = IsSuperFart;
    }
}

[Serializable] [NetSerializable]
public sealed class BibleFartSmiteEvent(NetEntity uid) : EntityEventArgs
{
    public NetEntity Bible = uid;
}
