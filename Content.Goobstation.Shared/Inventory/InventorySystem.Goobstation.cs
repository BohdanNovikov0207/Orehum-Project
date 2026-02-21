using Content.Shared.Inventory;
using Content.Shared.Inventory.Events;
using Content.Goobstation.Shared._Orehum.MoreGoggles;

namespace Content.Shared.Inventory;

public partial class InventorySystem
{
    private void InitializeGoobVisions()
    {
        SubscribeLocalEvent<InventoryComponent, RefreshEquipmentHudEvent<MinerVisionComponent>>(RelayInventoryEvent);
        SubscribeLocalEvent<InventoryComponent, RefreshEquipmentHudEvent<MesonVisionComponent>>(RelayInventoryEvent);
    }
}
