using Content.Shared.Body.Part;
using Robust.Shared.GameStates;

namespace Content.Shared._Shitmed.Medical.Surgery.Wounds.Components;

[RegisterComponent] [NetworkedComponent] [AutoGenerateComponentState]
public sealed partial class TourniquetableComponent : Component
{
    public EntityUid? CurrentTourniquetEntity;

    [AutoNetworkedField]
    public BodyPartType SeveredPartType = BodyPartType.Head;

    [AutoNetworkedField]
    public BodyPartSymmetry SeveredSymmetry = BodyPartSymmetry.None;
}
