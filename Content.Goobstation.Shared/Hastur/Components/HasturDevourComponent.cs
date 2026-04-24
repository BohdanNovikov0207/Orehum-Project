using Content.Shared.Damage;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Goobstation.Shared.Hastur.Components;

[RegisterComponent] [NetworkedComponent]
public sealed partial class HasturDevourComponent : Component
{
    /// <summary>
    /// How long the DoAfter delay before devour executes
    /// </summary>
    [DataField]
    public TimeSpan DevourDuration = TimeSpan.FromSeconds(1.7);

    [DataField]
    public string Devouring = "hastur_devour";

    [DataField]
    public SoundSpecifier? DevourSound = new SoundCollectionSpecifier("HasturDevour");

    /// <summary>
    /// Healing from devouring an entity.
    /// </summary>
    [DataField]
    public DamageSpecifier Healing = new();

    [DataField]
    public string Normal = "hasturM";

    [DataField]
    public TimeSpan StunDuration = TimeSpan.FromSeconds(1);
}
