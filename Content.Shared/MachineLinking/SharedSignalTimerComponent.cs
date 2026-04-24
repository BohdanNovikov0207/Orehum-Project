// SPDX-FileCopyrightText: 2023 CommieFlowers <rasmus.cedergren@hotmail.com>
// SPDX-FileCopyrightText: 2023 Nemanja <98561806+EmoGarbage404@users.noreply.github.com>
// SPDX-FileCopyrightText: 2023 rolfero <45628623+rolfero@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: MIT

using Robust.Shared.Serialization;

namespace Content.Shared.MachineLinking;

[Serializable] [NetSerializable]
public enum SignalTimerUiKey : byte
{
    Key,
}

/// <summary>
/// Represents a SignalTimerComponent state that can be sent to the client
/// </summary>
[Serializable] [NetSerializable]
public sealed class SignalTimerBoundUserInterfaceState : BoundUserInterfaceState
{
    public TimeSpan CurrentDelay; // Mono
    public string CurrentText;
    public bool HasAccess;
    public bool ShowText;
    public bool TimerStarted;
    public TimeSpan TriggerTime;

    public SignalTimerBoundUserInterfaceState(string currentText,
        TimeSpan currentDelay, // Mono
        bool showText,
        TimeSpan triggerTime,
        bool timerStarted,
        bool hasAccess)
    {
        CurrentText = currentText;
        CurrentDelay = currentDelay; // Mono
        ShowText = showText;
        TriggerTime = triggerTime;
        TimerStarted = timerStarted;
        HasAccess = hasAccess;
    }
}

[Serializable] [NetSerializable]
public sealed class SignalTimerTextChangedMessage : BoundUserInterfaceMessage
{
    public SignalTimerTextChangedMessage(string text)
    {
        Text = text;
    }

    public string Text { get; }
}

[Serializable] [NetSerializable]
public sealed class SignalTimerDelayChangedMessage : BoundUserInterfaceMessage
{
    public SignalTimerDelayChangedMessage(TimeSpan delay)
    {
        Delay = delay;
    }

    public TimeSpan Delay { get; }
}

[Serializable] [NetSerializable]
public sealed class SignalTimerStartMessage : BoundUserInterfaceMessage
{
}
