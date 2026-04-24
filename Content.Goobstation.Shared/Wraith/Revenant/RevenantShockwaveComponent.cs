using Content.Shared.Damage;
using Content.Shared.Tag;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Goobstation.Shared.Wraith.Revenant;

[RegisterComponent] [NetworkedComponent]
public sealed partial class RevenantShockwaveComponent : Component
{
    /// <summary>
    /// How long to knockdown people
    /// </summary>
    [DataField]
    public TimeSpan KnockdownDuration = TimeSpan.FromSeconds(10f);

    /// <summary>
    /// Search range of shockwave
    /// </summary>
    [DataField]
    public float SearchRange = 8f;

    [DataField]
    public SoundSpecifier? ShockSound = new SoundPathSpecifier("/Audio/_Goobstation/Wraith/revshock.ogg");

    /// <summary>
    /// Damage dealt to windows and walls
    /// </summary>
    [DataField]
    public DamageSpecifier? StructureDamage = new();

    /// <summary>
    /// How many tiles to pry
    /// </summary>
    [DataField]
    public float TilesToPry = 10;

    [ViewVariables]
    public ProtoId<TagPrototype> WallTag = "Wall";

    [ViewVariables]
    public ProtoId<TagPrototype> WindowTag = "Window";
}
