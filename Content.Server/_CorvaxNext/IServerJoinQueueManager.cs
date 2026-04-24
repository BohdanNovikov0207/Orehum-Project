namespace Content.Corvax.Interfaces.Server;

public interface IServerJoinQueueManager
{
    bool IsEnabled { get; }
    int PlayerInQueueCount { get; }
    int ActualPlayersCount { get; }
    void Initialize();
    void PostInitialize();
}
