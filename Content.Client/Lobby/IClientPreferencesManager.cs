using Content.Shared.Construction.Prototypes;
using Content.Shared.Preferences;
using Robust.Shared.Prototypes;

namespace Content.Client.Lobby;

public interface IClientPreferencesManager
{
    bool ServerDataLoaded => Settings != null;

    GameSettings? Settings { get; }
    PlayerPreferences? Preferences { get; }
    event Action OnServerDataLoaded;
    void Initialize();
    void SelectCharacter(ICharacterProfile profile);
    void SelectCharacter(int slot);
    void UpdateCharacter(ICharacterProfile profile, int slot);
    void CreateCharacter(ICharacterProfile profile);
    void DeleteCharacter(ICharacterProfile profile);
    void DeleteCharacter(int slot);
    void UpdateConstructionFavorites(List<ProtoId<ConstructionPrototype>> favorites);
}
