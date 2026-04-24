using Content.Shared.Containers.ItemSlots;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Virology;

[RegisterComponent]
public sealed partial class VirologyMachineComponent : Component
{
    [ViewVariables]
    public const string SwabSlotId = "disease_swab_slot";

    [DataField] [ViewVariables]
    public TimeSpan AnalysisDuration = TimeSpan.FromSeconds(5);

    [DataField]
    public SoundSpecifier AnalysisSound = new SoundPathSpecifier("/Audio/Machines/buzz_loop.ogg");

    [DataField]
    public SoundSpecifier AnalyzedSound = new SoundPathSpecifier("/Audio/Machines/diagnoser_printing.ogg");

    [DataField]
    public string? IdleState;

    // vaccine or live injector mode?
    [DataField]
    public bool InjectorMode;

    [DataField]
    public EntProtoId PaperPrototype = "DiagnosisReportPaper";

    [DataField]
    public string? RunningState;

    [ViewVariables]
    public EntityUid? SoundEntity;

    [DataField]
    public ItemSlot SwabSlot = new();

    // is this machine a vaccinator or analyzer?
    // holy fuck goida
    [DataField]
    public bool Vaccinator;

    [DataField]
    public EntProtoId VaccinePrototype = "Vaccine";
}
