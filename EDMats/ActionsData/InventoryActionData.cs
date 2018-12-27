using EDMats.Services;

namespace EDMats.ActionsData
{
    public class InventoryActionData : NotificationActionData
    {
        public InventoryActionData(CommanderInventory inventory)
        {
            Inventory = inventory;
        }

        public CommanderInventory Inventory { get; }
    }
}