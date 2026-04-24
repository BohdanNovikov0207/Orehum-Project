using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._Shitcode.Heretic.Components;

[RegisterComponent] [NetworkedComponent]
public sealed partial class StarTouchedStatusEffectComponent : Component
{
    [DataField]
    public EntProtoId CosmicCloud = "EffectCosmicCloud";

    [DataField]
    public TimeSpan SleepTime = TimeSpan.FromSeconds(8);
}
