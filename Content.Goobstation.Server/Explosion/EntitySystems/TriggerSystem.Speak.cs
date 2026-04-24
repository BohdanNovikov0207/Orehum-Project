// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 GoobBot <uristmchands@proton.me>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Goobstation.Server.Explosion.Components;
using Content.Shared.Speech;
using Content.Shared.Speech.Components;
using Content.Shared.Trigger.Systems;
using Robust.Server.Containers;

namespace Content.Goobstation.Server.Explosion.EntitySystems;

public sealed class TriggerOnSpeakSystem : EntitySystem
{
    [Dependency] private readonly ContainerSystem _containerSystem = default!;
    [Dependency] private readonly TriggerSystem _triggerSystem = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<TriggerOnSpeakComponent, ComponentInit>(OnSpeakInit);
        SubscribeLocalEvent<TriggerOnSpeakComponent, ListenEvent>(OnListen);
    }

    private void OnSpeakInit(EntityUid uid, TriggerOnSpeakComponent component, ComponentInit args)
        => EnsureComp<ActiveListenerComponent>(uid).Range = component.ListenRange;

    private void OnListen(Entity<TriggerOnSpeakComponent> ent, ref ListenEvent args)
    {
        var speaker = args.Source;

        if (speaker == ent.Owner)
        {
            _triggerSystem.Trigger(ent, speaker);
            return;
        }

        if (_containerSystem.TryGetContainingContainer(ent.Owner, out var container) && container.Owner == speaker)
            _triggerSystem.Trigger(ent, speaker);
    }
}
