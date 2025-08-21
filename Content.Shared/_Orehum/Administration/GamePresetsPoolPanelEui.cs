using Content.Shared.Eui;
using Robust.Shared.Serialization;

namespace Content.Shared._Orehum.Administration;

[Serializable, NetSerializable]
public sealed class GamePresetsPoolSyncMessage(Dictionary<string, (string name, bool enabled)> presets) : EuiMessageBase
{
    public Dictionary<string, (string name, bool enabled)> Presets { get; } = presets;
}

[Serializable, NetSerializable]
public sealed class GamePresetPoolRemoveMessage(string presetId) : EuiMessageBase
{
    public string PresetId { get; } = presetId;
}

[Serializable, NetSerializable]
public sealed class GamePresetPoolAddMessage(string presetId) : EuiMessageBase
{
    public string PresetId { get; } = presetId;
}

[Serializable, NetSerializable]
public sealed class GamePresetPoolRequestInfoMessage() : EuiMessageBase;
