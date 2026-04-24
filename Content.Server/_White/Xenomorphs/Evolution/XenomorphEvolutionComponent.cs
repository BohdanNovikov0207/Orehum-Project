using Content.Goobstation.Maths.FixedPoint;
using Content.Shared._White.RadialSelector;
using Content.Shared.Actions.Components;
using Robust.Shared.Prototypes;

namespace Content.Server._White.Xenomorphs.Evolution;

[RegisterComponent]
public sealed partial class XenomorphEvolutionComponent : Component
{
    [ViewVariables]
    public EntityUid? EvolutionAction;

    [DataField]
    public EntProtoId<InstantActionComponent> EvolutionActionId = "ActionEvolution";

    [DataField]
    public TimeSpan EvolutionDelay = TimeSpan.FromSeconds(3);

    [DataField]
    public TimeSpan EvolutionJitterDuration = TimeSpan.FromSeconds(10);

    [DataField(required: true)]
    public List<RadialSelectorEntry> EvolvesTo = new();

    [DataField]
    public FixedPoint2 Max;

    [ViewVariables]
    public TimeSpan NextPointsAt;

    [DataField]
    public FixedPoint2 Points;

    [DataField]
    public FixedPoint2 PointsPerSecond = 2;
}
