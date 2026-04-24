// SPDX-FileCopyrightText: 2024 Aidenkrz <aiden@djkraz.com>
// SPDX-FileCopyrightText: 2024 Piras314 <p1r4s@proton.me>
// SPDX-FileCopyrightText: 2025 Aiden <28298836+Aidenkrz@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Server.Abilities.Felinid;

[RegisterComponent]
public sealed partial class FelinidComponent : Component
{
    [DataField("eatAction")]
    public EntityUid? EatAction;

    [DataField("eatActionId",
        customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string? EatActionId = "ActionEatMouse";

    [DataField("eatActionTarget")]
    public EntityUid? EatActionTarget = null;

    [DataField("hairballAction")]
    public EntityUid? HairballAction;

    //[DataField("hairballAction", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    //public string HairballAction = "ActionHairball";

    [DataField("hairballActionId",
        customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string? HairballActionId = "ActionHairball";

    /// <summary>
    /// The hairball prototype to use.
    /// </summary>
    [DataField("hairballPrototype", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string HairballPrototype = "Hairball";
}
