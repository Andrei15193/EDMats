using System;
using System.Threading;
using System.Threading.Tasks;
using EDMats.ActionsData;
using EDMats.Services;

namespace EDMats.Actions
{
    public class SettingsActions : ActionSet
    {
        private readonly IInventoryService _inventoryService;

        public SettingsActions(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public Task LoadJournalFileAsync(string journalFilePath)
            => LoadJournalFileAsync(journalFilePath, CancellationToken.None);

        public async Task LoadJournalFileAsync(string journalFilePath, CancellationToken cancellationToken)
        {
            Dispatch(new OpeningJournalFileActionData(journalFilePath));
            var inventory = await _inventoryService.GetInventoryAsync(journalFilePath, cancellationToken);
            Dispatch(new InventoryActionData(inventory));
        }

        public void FilterMaterials(string filterText)
        {
            Dispatch(new FilterMaterialsActionData(filterText));
        }
    }
}