// SPDX-FileCopyrightText: 2024 Ed <96445749+TheShuEd@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2024 metalgearsloth <31366439+metalgearsloth@users.noreply.github.com>
// SPDX-FileCopyrightText: 2024 metalgearsloth <comedian_vs_clown@hotmail.com>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Shared.UserInterface;

public sealed class ActivatableUIOpenAttemptEvent : CancellableEntityEventArgs
{
    public ActivatableUIOpenAttemptEvent(EntityUid who)
    {
        User = who;
    }

    public EntityUid User { get; }
}

public sealed class
    UserOpenActivatableUIAttemptEvent : CancellableEntityEventArgs //have to one-up the already stroke-inducing name
{
    public UserOpenActivatableUIAttemptEvent(EntityUid who, EntityUid target)
    {
        User = who;
        Target = target;
    }

    public EntityUid User { get; }
    public EntityUid Target { get; }
}

public sealed class AfterActivatableUIOpenEvent : EntityEventArgs
{
    public readonly EntityUid Actor;

    public AfterActivatableUIOpenEvent(EntityUid who, EntityUid actor)
    {
        User = who;
        Actor = actor;
    }

    public EntityUid User { get; }
}

/// <summary>
/// This is after it's decided the user can open the UI,
/// but before the UI actually opens.
/// Use this if you need to prepare the UI itself
/// </summary>
public sealed class BeforeActivatableUIOpenEvent : EntityEventArgs
{
    public BeforeActivatableUIOpenEvent(EntityUid who)
    {
        User = who;
    }

    public EntityUid User { get; }
}

public sealed class ActivatableUIPlayerChangedEvent : EntityEventArgs
{
}
