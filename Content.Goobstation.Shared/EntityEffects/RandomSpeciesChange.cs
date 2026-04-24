using System.Linq;
using Content.Shared.EntityEffects;
using Content.Shared.Humanoid.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Goobstation.Shared.EntityEffects;

public sealed partial class RandomSpeciesChange : EntityEffect
{
    [DataField] public List<ProtoId<SpeciesPrototype>>? SpeciesBlacklist;

    [DataField] public List<ProtoId<SpeciesPrototype>>? SpeciesWhitelist;

    protected override string? ReagentEffectGuidebookText(IPrototypeManager prototype, IEntitySystemManager entSys)
        => Loc.GetString("reagent-effect-guidebook-change-species-random");

    public override void Effect(EntityEffectBaseArgs args)
    {
        var protMan = IoCManager.Resolve<IPrototypeManager>();
        var random = IoCManager.Resolve<IRobustRandom>();
        var entityEffects = args.EntityManager.System<SharedEntityEffectSystem>();

        // whatever, go my rngesus
        var species = protMan.EnumeratePrototypes<SpeciesPrototype>();

        if (SpeciesWhitelist != null && SpeciesWhitelist.Count > 0)
            species = species.Where(q => SpeciesWhitelist.Any(w => q.ID == w));

        if (SpeciesBlacklist != null && SpeciesBlacklist.Count > 0)
            species = species.Where(q => !SpeciesBlacklist.Any(w => q.ID == w));

        var sce = new SpeciesChange
        {
            NewSpecies = random.Pick(species.ToList()).ID,
        };

        entityEffects.Effect(sce, args);
    }
}
