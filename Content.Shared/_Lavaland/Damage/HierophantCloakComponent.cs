using Robust.Shared.Audio;
using Robust.Shared.Prototypes;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;

namespace Content.Shared._Lavaland.Damage;

[RegisterComponent]
public sealed partial class HierophantCloakComponent : Component
{
    [DataField]
    public EntProtoId PlaceMarkerActionId = "ActionHierophantPlaceMarker";

    [DataField]
    public EntProtoId TeleportToMarkerActionId = "ActionHierophantTeleport";

    [DataField]
    public EntityUid? PlaceMarkerActionEntity;

    [DataField]
    public EntityUid? TeleportToMarkerActionEntity;

    [DataField]
    public EntityUid? TeleportMarker;

    [DataField(customTypeSerializer: typeof(TimeOffsetSerializer))]
    public TimeSpan CooldownUntil = TimeSpan.Zero;

    [DataField]
    public TimeSpan CooldownDuration = TimeSpan.FromSeconds(30);
}
