using Content.Shared.Damage;

namespace Content.Shared._White.Xenomorphs.Acid.Components;

[RegisterComponent]
public sealed partial class AcidCorrodingComponent : Component
{
    [ViewVariables]
    public EntityUid Acid;

    [ViewVariables]
    public TimeSpan AcidExpiresAt;

    [DataField]
    public DamageSpecifier DamagePerSecond;

    [ViewVariables]
    public TimeSpan NextDamageAt;
}
