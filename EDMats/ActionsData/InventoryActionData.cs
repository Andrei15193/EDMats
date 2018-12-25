using EDMats.Services;

namespace EDMats.ActionsData
{
    public class InventoryActionData : ActionData
    {
        public InventoryActionData(CommanderInventory inventory)
        {
            Inventory = inventory;
        }

        public CommanderInventory Inventory { get; }
    }
}