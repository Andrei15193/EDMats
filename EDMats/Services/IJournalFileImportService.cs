using System.Threading;
using System.Threading.Tasks;

namespace EDMats.Services
{
    public interface IJournalFileImportService
    {
        Task<JournalCommanderInformation> ImportAsync(string journalFilePath);

        Task<JournalCommanderInformation> GetInventoryAsync(string journalFilePath, CancellationToken cancellationToken);
    }
}