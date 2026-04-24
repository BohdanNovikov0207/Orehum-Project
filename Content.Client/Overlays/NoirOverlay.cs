using Robust.Client.Graphics;
using Robust.Shared.Enums;
using Robust.Shared.Prototypes;

namespace Content.Client.Overlays;

public sealed class NoirOverlay : Overlay
{
    private static readonly ProtoId<ShaderPrototype> Shader = "Noir";
    private readonly ShaderInstance _noirShader;

    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    public NoirOverlay()
    {
        IoCManager.InjectDependencies(this);
        _noirShader = _prototypeManager.Index(Shader).InstanceUnique();
        ZIndex = 9; // draw this over the DamageOverlay, RainbowOverlay etc, but before the black and white shader
    }

    public override OverlaySpace Space => OverlaySpace.WorldSpace;
    public override bool RequestScreenTexture => true;

    protected override void Draw(in OverlayDrawArgs args)
    {
        if (ScreenTexture == null)
            return;

        var handle = args.WorldHandle;
        _noirShader.SetParameter("SCREEN_TEXTURE", ScreenTexture);
        handle.UseShader(_noirShader);
        handle.DrawRect(args.WorldBounds, Color.White);
        handle.UseShader(null);
    }
}
