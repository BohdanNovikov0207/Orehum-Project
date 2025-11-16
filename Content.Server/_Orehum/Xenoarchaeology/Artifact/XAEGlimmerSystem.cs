using Content.Shared.Psionics.Glimmer;
using Content.Shared.Xenoarchaeology.Artifact;
using Content.Shared.Xenoarchaeology.Artifact.XAE;
using Robust.Shared.Random;


namespace Content.Server._Orehum.Xenoarchaeology.Artifact;

public sealed class XAEGlimmerSystem : BaseXAESystem<XAEGlimmerComponent>
{
    [Dependency] private readonly GlimmerSystem _glimmer = default!;
    [Dependency] private readonly IRobustRandom _random = default!;

    protected override void OnActivated(Entity<XAEGlimmerComponent> ent, ref XenoArtifactNodeActivatedEvent args)
    {
        _glimmer.SetGlimmerOutput((float)_glimmer.GlimmerOutput + _random.NextFloat(ent.Comp.Min, ent.Comp.Max));
    }
}
