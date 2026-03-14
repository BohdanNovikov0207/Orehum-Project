using Content.Shared._Orehum.AC;

namespace Content.Client._Orehum.AC;

public sealed class ACSystem : EntitySystem
{
    #if !DEBUG
    public override void Initialize()
    {
        base.Initialize();

        var ver = GetLoader();
        RaiseNetworkEvent(new ACEvent(ver, NiceBro()));
    }

    private string GetLoader()
    {
        var versions = new string[] { "33.0", "32.1", "32.0", "31.0", "30.2", "30.1", "30.0", "29.1", "29.0", "28.1", "28.0" };

        foreach (var ver in versions)
        {
            var ty = Type.GetType($"SS14.Launcher.Utility.ZStd, SS14.Loader, Version=0.{ver}.0, Culture=neutral, PublicKeyToken=null", false);
            if (ty != null)
                return ver;
        }

        return "unknown";
    }

    private string NiceBro()
    {
        var hmm = string.Empty;

        var niceHmm = DetectNiceHmm();
        if (!string.IsNullOrEmpty(niceHmm))
            hmm += niceHmm;

        return hmm;
    }

    private string DetectNiceHmm()
    {
        var versions = new string[] { "2.4.1.0", "2.4.0.0", "2.3.7.0", "2.3.6.0", "2.3.5.0", "2.3.4.0", "2.3.3.0", "2.3.2.0", "2.3.1.1", "2.3.1.0", "2.3.0.1", "2.3.0.0", "2.2.2.0", "2.2.1.0", "2.2.0.0" };

        foreach (var ver in versions)
        {
            var ty = Type.GetType($"HarmonyLib.Harmony, 0Harmony, Version={ver}, Culture=neutral, PublicKeyToken=null", false);
            if (ty != null)
                return "Harmony v" + ver;
        }

        return string.Empty;
    }
    #endif
}
