// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameStates;

namespace Content.Trauma.Shared.GameTicking.Rules.Components;

/// <summary>
/// Game rule component for Morph. Handles round end summary.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class MorphRuleComponent : Component;
