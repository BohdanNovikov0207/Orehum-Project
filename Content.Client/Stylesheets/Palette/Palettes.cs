namespace Content.Client.Stylesheets.Palette;

/// <summary>
///     Stores all style palettes in one accessible location
/// </summary>
/// <remarks>
///     Technically not limited to only colors, can store like, standard padding amounts, and font sizes, maybe?
/// </remarks>
/// Orehum Project fully changed
public static class Palettes
{
    // muted tones
    public static readonly ColorPalette Navy = ColorPalette.FromHexBase("#363636", element: Color.FromHex("#363636"), background: Color.FromHex("#1E1E1E"), text: Color.FromHex("#BDBDBD")) with
    {
        HoveredElement = Color.FromHex("#878787"),
        PressedElement = Color.FromHex("#212121"),
        DisabledElement = Color.FromHex("#1B1D1F"),
        BackgroundLight = Color.FromHex("#363636"),
        BackgroundDark = Color.FromHex("#1B1D1F"),
        TextDark = Color.FromHex("#5A5A5A")
    };
    public static readonly ColorPalette Cyan = ColorPalette.FromHexBase("#42586a", lightnessShift: 0.05f, chromaShift: 0.0045f);
    public static readonly ColorPalette Slate = ColorPalette.FromHexBase("#363636", element: Color.FromHex("#363636"), background: Color.FromHex("#1E1E1E"), text: Color.FromHex("#BDBDBD")) with
    {
        HoveredElement = Color.FromHex("#878787"),
        PressedElement = Color.FromHex("#212121"),
        DisabledElement = Color.FromHex("#1B1D1F"),
        BackgroundLight = Color.FromHex("#363636"),
        BackgroundDark = Color.FromHex("#1B1D1F"),
        TextDark = Color.FromHex("#5A5A5A")
    };
    public static readonly ColorPalette Neutral = ColorPalette.FromHexBase("#555555");

    // status tones
    public static readonly ColorPalette Red = ColorPalette.FromHexBase("#BB3232", element: Color.FromHex("#AB3232"), background: Color.FromHex("#602A2A"), text: Color.FromHex("#BB3232")) with
    {
        HoveredElement = Color.FromHex("#CF2F2F"),
        PressedElement = Color.FromHex("#212121"),
        DisabledElement = Color.FromHex("#602A2A"),
        BackgroundLight = Color.FromHex("#AB3232"),
        BackgroundDark = Color.FromHex("#212121"),
        TextDark = Color.FromHex("#602A2A")
    };
    public static readonly ColorPalette Amber = ColorPalette.FromHexBase("#A5762F");
    public static readonly ColorPalette Green = ColorPalette.FromHexBase("#31843E", element: Color.FromHex("#28B03D"), background: Color.FromHex("#0B541B"), text: Color.FromHex("#31843E")) with
    {
        HoveredElement = Color.FromHex("#1CB834"),
        PressedElement = Color.FromHex("#212121"),
        DisabledElement = Color.FromHex("#0B541B"),
        BackgroundLight = Color.FromHex("#28B03D"),
        BackgroundDark = Color.FromHex("#212121"),
        TextDark = Color.FromHex("#0B541B")
    };
    public static readonly StatusPalette Status = new([Red.Base, Amber.Base, Green.Base]);

    // highlight tones
    public static readonly ColorPalette Gold = ColorPalette.FromHexBase("#BDBDBD", element: Color.FromHex("#BDBDBD"), background: Color.FromHex("#1E1E1E"), text: Color.FromHex("#BDBDBD")) with
    {
        BackgroundLight = Color.FromHex("#363636"),
        BackgroundDark = Color.FromHex("#1B1D1F"),
        TextDark = Color.FromHex("#5A5A5A")
    };
    public static readonly ColorPalette Maroon = ColorPalette.FromHexBase("#9b2236");

    // Intended to be used with `ModulateSelf` to darken / lighten something
    public static readonly ColorPalette AlphaModulate = ColorPalette.FromHexBase("#ffffff");

}
