using System.Globalization;

namespace Content.Client.Communications.UI;

/// <summary>
/// Trauma - alert level lock related UI stuff
/// </summary>
public sealed partial class CommunicationsConsoleMenu
{
    public string LockedLevel = string.Empty;
    public TimeSpan? NextUnlock;
}
