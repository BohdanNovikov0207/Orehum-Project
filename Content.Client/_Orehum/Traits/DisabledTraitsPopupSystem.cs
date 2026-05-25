using Content.Client._Orehum.Traits.UI;
using Content.Shared.Traits;
using Content.Shared._Orehum.CCVars;
using Content.Shared._Orehum.Traits;
using Robust.Shared.Configuration;
using Robust.Shared.Prototypes;

namespace Content.Client._Orehum.Traits;

/// <summary>
/// Client system that shows a popup when traits are disabled due to unmet conditions.
/// </summary>
public sealed class DisabledTraitsPopupSystem : EntitySystem
{
    [Dependency] private readonly IConfigurationManager _cfg = default!;

    private DisabledTraitsPopup? _window;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeNetworkEvent<DisabledTraitsEvent>(OnDisabledTraits);
    }

    private void OnDisabledTraits(DisabledTraitsEvent ev)
    {
        if (_cfg.GetCVar(TraitsCCVars.SkipDisabledTraitsPopup))
            return;

        if (ev.DisabledTraits.Count == 0)
            return;

        OpenDisabledTraitsPopup(ev.DisabledTraits);
    }

    private void OpenDisabledTraitsPopup(Dictionary<ProtoId<TraitPrototype>, List<string>> disabledTraits)
    {
        if (_window != null)
        {
            _window.Close();
            _window = null;
        }

        _window = new DisabledTraitsPopup(disabledTraits);
        _window.OpenCentered();
        _window.OnClose += () => _window = null;
    }
}
