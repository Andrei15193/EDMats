using System.Threading;
using System.Threading.Tasks;

namespace EDMats.Services
{
    public interface IGoalsFileStorageService
    {
        Task WriteGoalsAsync(string fileName, CommanderGoalsData commanderGoalsData);

        Task WriteGoalsAsync(string fileName, CommanderGoalsData commanderGoalsData, CancellationToken cancellationToken);

        Task<CommanderGoalsData> ReadGoalsAsync(string fileName);

        Task<CommanderGoalsData> ReadGoalsAsync(string fileName, CancellationToken cancellationToken);
    }
}