using Content.Shared.Tag;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Shared._Lavaland.Weapons.Crusher.Upgrades.Components;

[RegisterComponent]
public sealed partial class CrusherUpgradeMagmaWatcherComponent : Component
{
    [DataField("proto", required: true, customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string Proto = "BulletChargeDamage";
    //[DataField("originalProto", required: true, customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    //public string OriginalProto = "BulletCharge";
}


[RegisterComponent]
public sealed partial class CrusherUpgradeOriginalProtoComponent : Component
{
    [DataField]
    public string? OriginalProto;
}

