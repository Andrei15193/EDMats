using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EDMats.Services
{
    public interface IJournalImportService
    {
        Task<JournalCommanderInformation> ImportJournalAsync(TextReader journalFileReader);

        Task<JournalCommanderInformation> ImportJournalAsync(TextReader journalFileReader, CancellationToken cancellationToken);

        Task<IReadOnlyList<JournalUpdate>> ImportLatestJournalUpdatesAsync(TextReader journalFileReader, DateTime latestJournalEntryTimestamp);

        Task<IReadOnlyList<JournalUpdate>> ImportLatestJournalUpdatesAsync(TextReader journalFileReader, DateTime latestJournalEntryTimestamp, CancellationToken cancellationToken);
    }
}