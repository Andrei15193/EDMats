using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDMats.Services.Implementations
{
    public class JournalFileImportService : IJournalFileImportService
    {
        private readonly IJournalImportService _journalImportService;
        private readonly IFileSystemService _fileSystemService;

        public JournalFileImportService(IJournalImportService journalReaderService, IFileSystemService fileSystemService)
        {
            _journalImportService = journalReaderService;
            _fileSystemService = fileSystemService;
        }

        public Task<JournalCommanderInformation> ImportAsync(string journalFilePath)
            => GetInventoryAsync(journalFilePath, CancellationToken.None);

        public async Task<JournalCommanderInformation> GetInventoryAsync(string journalFilePath, CancellationToken cancellationToken)
        {
            using (var fileReader = _fileSystemService.OpenRead(journalFilePath))
                return await _journalImportService.ImportJournalAsync(fileReader, cancellationToken).ConfigureAwait(false);
        }
    }
}