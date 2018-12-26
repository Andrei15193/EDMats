using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Services.LogEntries;

namespace EDMats.Services
{
    public interface IJournalReaderService
    {
        Task<IReadOnlyList<LogEntry>> ReadAsync(TextReader textReader);

        Task<IReadOnlyList<LogEntry>> ReadAsync(TextReader textReader, CancellationToken cancellationToken);
    }
}