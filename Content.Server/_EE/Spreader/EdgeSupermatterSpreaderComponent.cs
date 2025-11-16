using Content.Shared._EE.Spreader;
using Robust.Shared.Prototypes;

namespace Content.Server._EE.Spreader;

[RegisterComponent, Access(typeof(SupermatterSpreaderSystem))]
public sealed partial class EdgeSupermatterSpreaderComponent : Component
{
    [DataField(required: true)]
    public EntProtoId<EdgeSupermatterSpreaderComponent> Id;

    [DataField]
    public bool PreventSpreadOnSpaced = true;
}
