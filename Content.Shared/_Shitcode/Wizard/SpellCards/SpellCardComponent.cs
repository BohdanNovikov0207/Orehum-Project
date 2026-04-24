// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Aviu00 <93730715+Aviu00@users.noreply.github.com>
// SPDX-FileCopyrightText: 2025 Misandry <mary@thughunt.ing>
// SPDX-FileCopyrightText: 2025 gus <august.eymann@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameStates;
using Robust.Shared.Serialization;

namespace Content.Shared._Goobstation.Wizard.SpellCards;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class SpellCardComponent : Component
{
    [ViewVariables(VVAccess.ReadOnly)]
    public float FlipAccumulator;

    [AutoNetworkedField]
    public bool Flipped;

    [DataField]
    public Color FlippedTrailColor = Color.White;

    [DataField]
    public float FlipTime = 0.4f;

    [ViewVariables(VVAccess.ReadOnly)]
    public float RotateAccumulator;

    [DataField]
    public float RotateTime = 0.1f;

    [AutoNetworkedField]
    public EntityUid? Target;

    [AutoNetworkedField]
    public bool Targeted;

    [DataField]
    public float TargetedSpeed = 20f;

    [DataField]
    public float Tolerance = 0.1f;
}

[Serializable] [NetSerializable]
public enum SpellCardVisuals : byte
{
    State, // 0 - not flipped, 1 - flipping, 2 - flipped
}
