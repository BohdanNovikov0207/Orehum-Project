using System.Numerics;
using Content.Shared.Whitelist;
using Robust.Shared.GameStates;

namespace Content.Shared._Shitcode.Heretic.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class VelocityModifierContactsComponent : Component
{
    [DataField]
    public EntityWhitelist? Blacklist;

    [DataField] [AutoNetworkedField]
    public bool IsActive = true;

    [DataField] [AutoNetworkedField]
    public float Modifier = 1.0f;

    [DataField]
    public EntityWhitelist? Whitelist;
}

[NetworkedComponent] [RegisterComponent] [AutoGenerateComponentState]
public sealed partial class VelocityModifiedByContactComponent : Component
{
    [DataField] [AutoNetworkedField]
    public Vector2? OriginalVelocity;
}
