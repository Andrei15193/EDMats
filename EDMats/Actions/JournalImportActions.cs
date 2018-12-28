using System.Threading;
using System.Threading.Tasks;
using EDMats.ActionsData;
using EDMats.Services;

namespace EDMats.Actions
{
    public class JournalImportActions : ActionSet
    {
        private readonly IJournalFileImportService _journalFileImportService;

        public JournalImportActions(IJournalFileImportService journalFileImportService)
        {
            _journalFileImportService = journalFileImportService;
        }

        public Task LoadJournalFileAsync(string journalFilePath)
            => LoadJournalFileAsync(journalFilePath, CancellationToken.None);

        public async Task LoadJournalFileAsync(string journalFilePath, CancellationToken cancellationToken)
        {
            Dispatch(new OpeningJournalFileActionData(journalFilePath)
            {
                NotificationText = $"Loading journal file \"{journalFilePath}\""
            });
            var commanderInformation = await _journalFileImportService.ImportAsync(journalFilePath, cancellationToken);
            Dispatch(new JournalImportedActionData(commanderInformation)
            {
                NotificationText = $"Journal file loaded, latest entry on {commanderInformation.LatestUpdate:f}"
            });
        }

        public void FilterMaterials(string filterText)
            => Dispatch(new FilterMaterialsActionData(filterText));
    }
}