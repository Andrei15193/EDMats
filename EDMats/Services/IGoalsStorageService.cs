using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EDMats.Services
{
    public interface IGoalsStorageService
    {
        Task WriteGoalsAsync(TextWriter textWriter, CommanderGoalsData commanderGoalsData);

        Task WriteGoalsAsync(TextWriter textWriter, CommanderGoalsData commanderGoalsData, CancellationToken cancellationToken);

        Task<CommanderGoalsData> ReadGoalsAsync(TextReader textReader);

        Task<CommanderGoalsData> ReadGoalsAsync(TextReader textReader, CancellationToken cancellationToken);
    }
}