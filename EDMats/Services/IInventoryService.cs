using System.Threading;
using System.Threading.Tasks;

namespace EDMats.Services
{
    public interface IInventoryService
    {
        Task<CommanderInventory> GetInventoryAsync(string journalFilePath);

        Task<CommanderInventory> GetInventoryAsync(string journalFilePath, CancellationToken cancellationToken);
    }
}