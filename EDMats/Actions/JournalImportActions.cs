using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EDMats.ActionsData;
using EDMats.Services;
using FluxBase;

namespace EDMats.Actions
{
    public class JournalImportActions
    {
        private readonly Dispatcher _dispatcher;
        private readonly IJournalFileImportService _journalFileImportService;

        public JournalImportActions(Dispatcher dispatcher, IJournalFileImportService journalFileImportService)
        {
            _dispatcher = dispatcher;
            _journalFileImportService = journalFileImportService;
        }

        public Task LoadJournalFileAsync(string journalFilePath)
            => LoadJournalFileAsync(journalFilePath, CancellationToken.None);

        public async Task LoadJournalFileAsync(string journalFilePath, CancellationToken cancellationToken)
        {
            App.NotificationsViewModel.AddNotification($"Loading journal file \"{journalFilePath}\"");
            _dispatcher.Dispatch(new OpeningJournalFileActionData(journalFilePath));
            var commanderInformation = await _journalFileImportService.ImportAsync(journalFilePath, cancellationToken);
            _dispatcher.Dispatch(new JournalImportedActionData(commanderInformation));
            App.NotificationsViewModel.AddNotification($"Journal file loaded, latest entry on {commanderInformation.LatestUpdate:f}");
        }

        public Task<bool> LoadJournalFileAsync(string journalFilePath, DateTime latestUpdate)
            => LoadJournalFileAsync(journalFilePath, latestUpdate, CancellationToken.None);

        public async Task<bool> LoadJournalFileAsync(string journalFilePath, DateTime latestUpdate, CancellationToken cancellationToken)
        {
            var updates = await _journalFileImportService.ImportLatestJournalUpdatesAsync(journalFilePath, latestUpdate, cancellationToken);
            foreach (var update in updates.OfType<MaterialCollectedJournalUpdate>())
            {
                _dispatcher.Dispatch(new MaterialCollectedActionData(update.Timestamp, update.CollectedMaterial));
                App.NotificationsViewModel.AddNotification($"Collected {update.CollectedMaterial.Amount} of {update.CollectedMaterial.Material.Name}");
            }
            return updates.Any();
        }

        public void FilterMaterials(string filterText)
            => _dispatcher.Dispatch(new FilterMaterialsActionData(filterText));
    }
}