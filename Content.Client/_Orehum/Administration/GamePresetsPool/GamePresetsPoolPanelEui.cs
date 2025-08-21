using Content.Client.Eui;
using Content.Shared._Orehum.Administration;
using Content.Shared.Eui;

namespace Content.Client._Orehum.Administration.GamePresetsPool;

public sealed class GamePresetsPoolPanelEui : BaseEui
{
    public GamePresetsPoolPanel GamePresetsPoolPanel { get; }

    public GamePresetsPoolPanelEui()
    {
        GamePresetsPoolPanel = new();
        GamePresetsPoolPanel.OnEnablePreset += id => SendMessage(new GamePresetPoolAddMessage(id));
        GamePresetsPoolPanel.OnDisablePreset += id => SendMessage(new GamePresetPoolRemoveMessage(id));
        GamePresetsPoolPanel.OnUpdateRequest += () => SendMessage(new GamePresetPoolRequestInfoMessage());
    }

    public override void Opened() => GamePresetsPoolPanel.OpenCentered();

    public override void Closed() => GamePresetsPoolPanel.Close();

    public override void HandleMessage(EuiMessageBase msg)
    {
        base.HandleMessage(msg);

        if (msg is not GamePresetsPoolSyncMessage cast)
            return;

        GamePresetsPoolPanel.UpdateState(cast);
    }
}
