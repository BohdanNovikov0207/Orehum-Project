using Robust.Shared.Prototypes;

namespace Content.Shared.Mind;

/// <summary>
/// The core properties of Role Types
/// </summary>
[Prototype]
public sealed class RoleTypePrototype : IPrototype
{
    public const string FallbackSymbol = "";

    public static readonly LocId FallbackName = "role-type-crew-aligned-name";
    public static readonly Color FallbackColor = Color.FromHex("#eeeeee");

    /// <summary>
    /// The role's displayed color.
    /// </summary>
    [DataField]
    public Color Color = FallbackColor;

    /// <summary>
    /// The role's name as displayed on the UI.
    /// </summary>
    [DataField]
    public LocId Name = FallbackName;

    /// <summary>
    /// A symbol used to represent the role type.
    /// </summary>
    [DataField]
    public string Symbol = FallbackSymbol;

    [IdDataField]
    public string ID { get; } = default!;
}
