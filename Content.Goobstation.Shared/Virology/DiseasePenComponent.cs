using Robust.Shared.Audio;

namespace Content.Goobstation.Shared.Virology;

[RegisterComponent]
public sealed partial class DiseasePenComponent : Component
{
    [ViewVariables]
    public EntityUid? DiseaseUid;

    [ViewVariables]
    public int? Genotype;

    [DataField] [ViewVariables]
    public SoundSpecifier InjectSound = new SoundPathSpecifier("/Audio/Items/hypospray.ogg");

    [DataField]
    public TimeSpan InjectTime = TimeSpan.FromSeconds(8);

    [ViewVariables]
    public bool Used = false;

    [DataField]
    public bool Vaccine = true;
}
