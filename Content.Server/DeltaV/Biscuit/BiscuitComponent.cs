using Content.Shared._DV.Biscuit;

namespace Content.Server.DeltaV.Biscuit;

[RegisterComponent]
public sealed partial class BiscuitComponent : SharedBiscuitComponent
{
    [DataField]
    public bool Cracked { get; set; }
}
