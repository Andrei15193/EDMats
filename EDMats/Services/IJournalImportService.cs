using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EDMats.Services
{
    public interface IJournalImportService
    {
        Task<JournalCommanderInformation> ImportJournalAsync(TextReader journalFileReader);

        Task<JournalCommanderInformation> ImportJournalAsync(TextReader journalFileReader, CancellationToken cancellationToken);
    }
}