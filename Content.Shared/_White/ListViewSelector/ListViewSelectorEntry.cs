using Robust.Shared.Serialization;

namespace Content.Shared._White.ListViewSelector;

[Serializable] [NetSerializable]
public record ListViewSelectorEntry(string Id, string Name = "", string Description = "");

[Serializable] [NetSerializable]
public enum ListViewSelectorUiKey
{
    Key,
}

[Serializable] [NetSerializable]
public sealed class ListViewSelectorState(
    List<ListViewSelectorEntry> items,
    Dictionary<string, object>? metaData = null) : BoundUserInterfaceState
{
    public Dictionary<string, object> MetaData = metaData ?? new Dictionary<string, object>();
    public List<ListViewSelectorEntry> Items { get; } = items;
}

[Serializable] [NetSerializable]
public sealed class ListViewItemSelectedMessage(
    ListViewSelectorEntry selectedItem,
    int index,
    Dictionary<string, object> metaData = default!)
    : BoundUserInterfaceMessage
{
    public Dictionary<string, object> MetaData = metaData;
    public ListViewSelectorEntry SelectedItem { get; private set; } = selectedItem;
    public int Index { get; private set; } = index;
}
