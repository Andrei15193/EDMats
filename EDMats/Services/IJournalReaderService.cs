using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Data.JournalEntries;

namespace EDMats.Services
{
    public interface IJournalReaderService
    {
        Task<IReadOnlyList<JournalEntry>> ReadAsync(TextReader textReader);

        Task<IReadOnlyList<JournalEntry>> ReadAsync(TextReader textReader, CancellationToken cancellationToken);
    }
}