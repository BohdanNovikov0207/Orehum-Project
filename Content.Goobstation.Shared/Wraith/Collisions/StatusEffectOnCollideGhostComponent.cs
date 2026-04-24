using Content.Shared.StatusEffect;
using Content.Shared.Whitelist;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Wraith.Collisions;

[RegisterComponent] [NetworkedComponent]
public sealed partial class StatusEffectOnCollideGhostComponent : Component
{
    [DataField]
    public string Component = "Corporeal";

    [DataField]
    public TimeSpan Duration;

    [ViewVariables]
    public string FixtureId = "statusEffectCollision";

    [DataField]
    public bool Refresh = true;

    [DataField]
    public ProtoId<StatusEffectPrototype> StatusEffect = "Corporeal";

    [DataField]
    public EntityWhitelist? Whitelist;
}

/// <summary>
/// Raised on the entity that collided with the object
/// </summary>
[ByRefEvent]
public record struct StatusEffectOnCollideEvent(TimeSpan EffectTimespan);
