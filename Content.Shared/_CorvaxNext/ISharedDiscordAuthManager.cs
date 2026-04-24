namespace Content.Corvax.Interfaces.Shared;

public interface ISharedDiscordAuthManager
{
    bool IsOpt { get; }
    bool IsEnabled { get; }
    void Initialize();
}
