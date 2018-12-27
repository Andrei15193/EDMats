using EDMats.Services;

namespace EDMats.ActionsData
{
    public class InventoryActionData : NotificationActionData
    {
        public InventoryActionData(JournalCommanderInformation inventory)
        {
            Inventory = inventory;
        }

        public JournalCommanderInformation Inventory { get; }
    }
}