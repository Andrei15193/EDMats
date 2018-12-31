using System;
using System.Collections.Generic;
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
            => ImportAsync(journalFilePath, CancellationToken.None);

        public async Task<JournalCommanderInformation> ImportAsync(string journalFilePath, CancellationToken cancellationToken)
        {
            using (var fileReader = _fileSystemService.OpenRead(journalFilePath))
                return await _journalImportService.ImportJournalAsync(fileReader, cancellationToken).ConfigureAwait(false);
        }

        public Task<IReadOnlyList<JournalUpdate>> ImportLatestJournalUpdatesAsync(string journalFilePath, DateTime latestEntry)
            => ImportLatestJournalUpdatesAsync(journalFilePath, latestEntry, CancellationToken.None);

        public async Task<IReadOnlyList<JournalUpdate>> ImportLatestJournalUpdatesAsync(string journalFilePath, DateTime latestEntry, CancellationToken cancellationToken)
        {
            using (var fileReader = _fileSystemService.OpenRead(journalFilePath))
                return await _journalImportService.ImportLatestJournalUpdatesAsync(fileReader, latestEntry, cancellationToken).ConfigureAwait(false);
        }
    }
}