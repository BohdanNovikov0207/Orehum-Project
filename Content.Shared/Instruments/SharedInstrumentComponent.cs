// SPDX-FileCopyrightText: 2020 Tyler Young <tyler.young@impromptu.ninja>
// SPDX-FileCopyrightText: 2020 Vera Aguilera Puerto <zddm@outlook.es>
// SPDX-FileCopyrightText: 2020 Víctor Aguilera Puerto <6766154+Zumorica@users.noreply.github.com>
// SPDX-FileCopyrightText: 2020 Víctor Aguilera Puerto <zddm@outlook.es>
// SPDX-FileCopyrightText: 2020 zumorica <zddm@outlook.es>
// SPDX-FileCopyrightText: 2021 Acruid <shatter66@gmail.com>
// SPDX-FileCopyrightText: 2021 DrSmugleaf <DrSmugleaf@users.noreply.github.com>
// SPDX-FileCopyrightText: 2021 Metal Gear Sloth <metalgearsloth@gmail.com>
// SPDX-FileCopyrightText: 2021 Visne <39844191+Visne@users.noreply.github.com>
// SPDX-FileCopyrightText: 2022 Pieter-Jan Briers <pieterjan.briers+git@gmail.com>
// SPDX-FileCopyrightText: 2022 Vera Aguilera Puerto <gradientvera@outlook.com>
// SPDX-FileCopyrightText: 2022 mirrorcult <lunarautomaton6@gmail.com>
// SPDX-FileCopyrightText: 2022 wrexbe <81056464+wrexbe@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 Vera Aguilera Puerto <6766154+Zumorica@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Fildrance <fildrance@gmail.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
// SPDX-FileCopyrightText: 2025 Pieter-Jan Briers <pieterjan.briers@gmail.com>
// SPDX-FileCopyrightText: 2025 SX-7 <sn1.test.preria.2002@gmail.com>
// SPDX-FileCopyrightText: 2025 Simon <63975668+Simyon264@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 krusti <43324723+Topicranger@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 krusti <krusti@fluffytech.xyz>
// SPDX-FileCopyrightText: 2025 slarticodefast <161409025+slarticodefast@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using System.Collections;
using System.Text;
using Robust.Shared.Audio.Midi;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Shared.Instruments;

[NetworkedComponent]
[Access(typeof(SharedInstrumentSystem))]
public abstract partial class SharedInstrumentComponent : Component
{
    [ViewVariables]
    public bool Playing { get; set; }

    [DataField("program")] [ViewVariables(VVAccess.ReadWrite)]
    public byte InstrumentProgram { get; set; }

    [DataField("bank")] [ViewVariables(VVAccess.ReadWrite)]
    public byte InstrumentBank { get; set; }

    [DataField("allowPercussion")] [ViewVariables(VVAccess.ReadWrite)]
    public bool AllowPercussion { get; set; }

    [DataField("allowProgramChange")] [ViewVariables(VVAccess.ReadWrite)]
    public bool AllowProgramChange { get; set; }

    [DataField("respectMidiLimits")] [ViewVariables(VVAccess.ReadWrite)]
    public bool RespectMidiLimits { get; set; } = true;

    [ViewVariables(VVAccess.ReadWrite)]
    public EntityUid? Master { get; set; } = null;

    [ViewVariables]
    public BitArray FilteredChannels { get; set; } = new(RobustMidiEvent.MaxChannels, true);
}

/// <summary>
/// Component that indicates that musical instrument was activated (ui opened).
/// </summary>
[RegisterComponent] [NetworkedComponent]
[AutoGenerateComponentState(true)]
public sealed partial class ActiveInstrumentComponent : Component
{
    [DataField]
    [AutoNetworkedField]
    public MidiTrack?[] Tracks = [];
}

[Serializable] [NetSerializable]
public sealed class InstrumentComponentState : ComponentState
{
    public bool AllowPercussion;

    public bool AllowProgramChange;

    public BitArray FilteredChannels = default!;

    public byte InstrumentBank;

    public byte InstrumentProgram;

    public NetEntity? Master;
    public bool Playing;

    public bool RespectMidiLimits;
}

/// <summary>
/// This message is sent to the client to completely stop midi input and midi playback.
/// </summary>
[Serializable] [NetSerializable]
public sealed class InstrumentStopMidiEvent : EntityEventArgs
{
    public InstrumentStopMidiEvent(NetEntity uid)
    {
        Uid = uid;
    }

    public NetEntity Uid { get; }
}

/// <summary>
/// Send from the client to the server to set a master instrument.
/// </summary>
[Serializable] [NetSerializable]
public sealed class InstrumentSetMasterEvent : EntityEventArgs
{
    public InstrumentSetMasterEvent(NetEntity uid, NetEntity? master)
    {
        Uid = uid;
        Master = master;
    }

    public NetEntity Uid { get; }
    public NetEntity? Master { get; }
}

/// <summary>
/// Send from the client to the server to set a master instrument channel.
/// </summary>
[Serializable] [NetSerializable]
public sealed class InstrumentSetFilteredChannelEvent : EntityEventArgs
{
    public InstrumentSetFilteredChannelEvent(NetEntity uid, int channel, bool value)
    {
        Uid = uid;
        Channel = channel;
        Value = value;
    }

    public NetEntity Uid { get; }
    public int Channel { get; }
    public bool Value { get; }
}

/// <summary>
/// This message is sent to the client to start the synth.
/// </summary>
[Serializable] [NetSerializable]
public sealed class InstrumentStartMidiEvent : EntityEventArgs
{
    public InstrumentStartMidiEvent(NetEntity uid)
    {
        Uid = uid;
    }

    public NetEntity Uid { get; }
}

/// <summary>
/// This message carries a MidiEvent to be played on clients.
/// </summary>
[Serializable] [NetSerializable]
public sealed class InstrumentMidiEventEvent : EntityEventArgs
{
    public InstrumentMidiEventEvent(NetEntity uid, RobustMidiEvent[] midiEvent)
    {
        Uid = uid;
        MidiEvent = midiEvent;
    }

    public NetEntity Uid { get; }
    public RobustMidiEvent[] MidiEvent { get; }
}

[NetSerializable] [Serializable]
public enum InstrumentUiKey
{
    Key,
}

/// <summary>
/// Sets the MIDI channels on an instrument.
/// </summary>
[Serializable] [NetSerializable]
public sealed class InstrumentSetChannelsEvent : EntityEventArgs
{
    public InstrumentSetChannelsEvent(NetEntity uid, MidiTrack?[] tracks)
    {
        Uid = uid;
        Tracks = tracks;
    }

    public NetEntity Uid { get; }
    public MidiTrack?[] Tracks { get; set; }
}

/// <summary>
/// Represents a single midi track with the track name, instrument name and bank instrument name extracted.
/// </summary>
[Serializable] [NetSerializable]
public sealed class MidiTrack
{
    private const string Postfix = "…";

    /// <summary>
    /// The first specified instrument name
    /// </summary>
    public string? InstrumentName;

    /// <summary>
    /// The first program change resolved to the name.
    /// </summary>
    public string? ProgramName;

    /// <summary>
    /// The first specified Track Name
    /// </summary>
    public string? TrackName;

    public override string ToString() =>
        $"Track Name: {TrackName}; Instrument Name: {InstrumentName}; Program Name: {ProgramName}";

    /// <summary>
    /// Truncates the fields based on the limit inputted into this method.
    /// </summary>
    public void TruncateFields(int limit)
    {
        if (InstrumentName != null)
            InstrumentName = Truncate(InstrumentName, limit);

        if (TrackName != null)
            TrackName = Truncate(TrackName, limit);

        if (ProgramName != null)
            ProgramName = Truncate(ProgramName, limit);
    }

    public void SanitizeFields()
    {
        if (InstrumentName != null)
            InstrumentName = Sanitize(InstrumentName);

        if (TrackName != null)
            TrackName = Sanitize(TrackName);

        if (ProgramName != null)
            ProgramName = Sanitize(ProgramName);
    }

    // TODO: Make a general method to use in RT? idk if we have that.
    private string Truncate(string input, int limit)
    {
        if (string.IsNullOrEmpty(input) || limit <= 0 || input.Length <= limit)
            return input;

        var truncatedLength = limit - Postfix.Length;

        return input.Substring(0, truncatedLength) + Postfix;
    }

    private static string Sanitize(string input)
    {
        var sanitized = new StringBuilder(input.Length);

        foreach (var c in input)
        {
            if (!char.IsControl(c) && c <= 127) // no control characters, only ASCII
                sanitized.Append(c);
        }

        return sanitized.ToString();
    }
}
