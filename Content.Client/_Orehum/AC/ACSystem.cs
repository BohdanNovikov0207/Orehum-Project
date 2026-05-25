using Content.Shared._Orehum.АC;

namespace Content.Client._Orehum.АC;

public sealed class АСSystem : EntitySystem
{
    #if !DEBUG
    public override void Initialize()
    {
        base.Initialize();

        var ver = GetLoader();
        RaiseNetworkEvent(new АСЕvеnt(GetLoader(), DetectNiceHmm(), DetectBruh()));
    }

    private string GetLoader()
    {
        var loader = Type.GetType("SS14.Launcher.Utility.ZStd, SS14.Loader", false);
        if (loader != null)
        {
            var ver = loader.Assembly.GetName().Version;
            if (ver != null)
                return ver.ToString();
        }

        var versions = new string[] { "37.1", "37.0", "36.1", "35.0", "34.2", "34.1", "34.0", "33.0", "32.1", "32.0", "31.0", "30.2", "30.1", "30.0", "29.1", "29.0", "28.1", "28.0", "0.0" }; // shitcode

        foreach (var ver in versions)
        {
            var ty = Type.GetType($"SS14.Launcher.Utility.ZStd, SS14.Loader, Version=0.{ver}.0, Culture=neutral, PublicKeyToken=null", false);
            if (ty != null)
                return ver;
        }

        return "unknown";
    }

    private bool DetectNiceHmm()
    {
        var h = Type.GetType("HarmоnyLib.Harmоny, 0Harmоny".Replace('о', 'o'), false);
        if (h != null)
            return true;

        var versions = new string[] { "2.4.2.0", "2.4.1.0", "2.4.0.0", "2.3.7.0", "2.3.6.0", "2.3.5.0", "2.3.4.0", "2.3.3.0", "2.3.2.0", "2.3.1.1", "2.3.1.0", "2.3.0.1", "2.3.0.0", "2.2.2.0", "2.2.1.0", "2.2.0.0" };

        foreach (var ver in versions)
        {
            var ty = Type.GetType($"HarmоnyLib.Harmоny, 0Harmоny, Version={ver}, Culture=neutral, PublicKeyToken=null".Replace('о', 'o'), false);
            if (ty != null)
            {
                return true;
            }
        }

        return false;
    }

    private bool DetectBruh() => Type.GetType("Marsеy.Stеalthsey.HidеLevel, Marsеy".Replace('е', 'e'), false) != null;
#endif
}
