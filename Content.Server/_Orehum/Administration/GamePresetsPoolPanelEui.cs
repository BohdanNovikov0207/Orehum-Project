using System.Linq;
using System.Threading.Tasks;
using Content.Server.EUI;
using Content.Server.GameTicking.Presets;
using Content.Server.Voting.Managers;
using Content.Shared._Orehum.Administration;
using Content.Shared.Eui;
using Robust.Shared.Prototypes;

namespace Content.Server._Orehum.Administration;

public sealed class GamePresetsPoolPanelEui : BaseEui
{
    [Dependency] private readonly ILogManager _log = default!;
    [Dependency] private readonly IVoteManager _voteManager = default!;
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    private readonly List<(string presetId, string name)> _cachedAllGamePresets;
    private readonly Dictionary<string, (string name, bool enabled)> _state;

    public GamePresetsPoolPanelEui()
    {
        IoCManager.InjectDependencies(this);
        _cachedAllGamePresets = _prototypeManager.EnumeratePrototypes<GamePresetPrototype>()
            .Where(p => p.ShowInVote) // показывать ток доступные для голосования
            .Select(p => (p.ID, p.ModeTitle))
            .ToList();
        _state = new(_cachedAllGamePresets.Count);
    }

    public override void HandleMessage(EuiMessageBase msg)
    {
        base.HandleMessage(msg);

        if (msg is GamePresetPoolRemoveMessage remove)
        {
            _voteManager.RemoveWhitelistedPreset(remove.PresetId);
        }
        else if (msg is GamePresetPoolAddMessage add)
        {
            _voteManager.AddWhitelistedPreset(add.PresetId);
        }
        else if (msg is GamePresetPoolRequestInfoMessage)
        {
            _state.Clear();
            var enabled = _voteManager.GetWhitelistedPresets();
            foreach (var (presetId, name) in _cachedAllGamePresets)
            {
                _state.Add(presetId, (name, enabled.Contains(presetId)));
            }
            SendMessage(new GamePresetsPoolSyncMessage(_state));
        }
    }
}
