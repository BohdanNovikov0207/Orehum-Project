// SPDX-FileCopyrightText: 2024 Hannah Giovanna Dawson <karakkaraz@gmail.com>
// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 Your Name <you@example.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Chat.V2;
using Content.Shared.Radio;

namespace Content.Server.Chat.V2;

/// <summary>
/// Raised locally when a comms announcement is made.
/// </summary>
public sealed class CommsAnnouncementCreatedEvent(EntityUid sender, EntityUid console, string message) : IChatEvent
{
    public EntityUid Console = console;
    public uint Id { get; set; }
    public EntityUid Sender { get; set; } = sender;
    public string Message { get; set; } = message;
    public MessageType Type => MessageType.Announcement;
}

/// <summary>
/// Raised locally when a character speaks in Dead Chat.
/// </summary>
public sealed class DeadChatCreatedEvent(EntityUid speaker, string message, bool isAdmin) : IChatEvent
{
    public bool IsAdmin = isAdmin;
    public uint Id { get; set; }
    public EntityUid Sender { get; set; } = speaker;
    public string Message { get; set; } = message;
    public MessageType Type => MessageType.DeadChat;
}

/// <summary>
/// Raised locally when a character emotes.
/// </summary>
public sealed class EmoteCreatedEvent(EntityUid sender, string message, float range) : IChatEvent
{
    public float Range = range;
    public uint Id { get; set; }
    public EntityUid Sender { get; set; } = sender;
    public string Message { get; set; } = message;
    public MessageType Type => MessageType.Emote;
}

/// <summary>
/// Raised locally when a character talks in local.
/// </summary>
public sealed class LocalChatCreatedEvent(EntityUid speaker, string message, float range) : IChatEvent
{
    public float Range = range;
    public uint Id { get; set; }
    public EntityUid Sender { get; set; } = speaker;
    public string Message { get; set; } = message;
    public MessageType Type => MessageType.Local;
}

/// <summary>
/// Raised locally when a character speaks in LOOC.
/// </summary>
public sealed class LoocCreatedEvent(EntityUid speaker, string message) : IChatEvent
{
    public uint Id { get; set; }
    public EntityUid Sender { get; set; } = speaker;
    public string Message { get; set; } = message;
    public MessageType Type => MessageType.Looc;
}

/// <summary>
/// Raised locally when a character speaks on the radio.
/// </summary>
public sealed class RadioCreatedEvent(
    EntityUid speaker,
    string message,
    RadioChannelPrototype channel)
    : IChatEvent
{
    public RadioChannelPrototype Channel = channel;
    public uint Id { get; set; }
    public EntityUid Sender { get; set; } = speaker;
    public string Message { get; set; } = message;
    public MessageType Type => MessageType.Radio;
}

/// <summary>
/// Raised locally when a character whispers.
/// </summary>
public sealed class WhisperCreatedEvent(EntityUid speaker, string message, float minRange, float maxRange) : IChatEvent
{
    public float MaxRange = maxRange;
    public float MinRange = minRange;
    public uint Id { get; set; }
    public EntityUid Sender { get; set; } = speaker;
    public string Message { get; set; } = message;
    public MessageType Type => MessageType.Whisper;
}
