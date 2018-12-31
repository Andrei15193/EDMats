using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EDMats.Services
{
    public interface IJournalFileImportService
    {
        Task<JournalCommanderInformation> ImportAsync(string journalFilePath);

        Task<JournalCommanderInformation> ImportAsync(string journalFilePath, CancellationToken cancellationToken);

        Task<IReadOnlyList<JournalUpdate>> ImportLatestJournalUpdatesAsync(string journalFilePath, DateTime latestEntry);

        Task<IReadOnlyList<JournalUpdate>> ImportLatestJournalUpdatesAsync(string journalFilePath, DateTime latestEntry, CancellationToken cancellationToken);
    }
}