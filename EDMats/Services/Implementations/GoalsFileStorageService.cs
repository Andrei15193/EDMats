using System.Threading;
using System.Threading.Tasks;

namespace EDMats.Services.Implementations
{
    public class GoalsFileStorageService : IGoalsFileStorageService
    {
        private readonly IGoalsStorageService _goalsStorageService;
        private readonly IFileSystemService _fileSystemService;

        public GoalsFileStorageService(IGoalsStorageService goalsStorageService, IFileSystemService fileSystemService)
        {
            _goalsStorageService = goalsStorageService;
            _fileSystemService = fileSystemService;
        }

        public Task<CommanderGoalsData> ReadGoalsAsync(string fileName)
            => ReadGoalsAsync(fileName, CancellationToken.None);

        public async Task<CommanderGoalsData> ReadGoalsAsync(string fileName, CancellationToken cancellationToken)
        {
            using (var fileReader = _fileSystemService.OpenRead(fileName))
                return await _goalsStorageService.ReadGoalsAsync(fileReader, cancellationToken).ConfigureAwait(false);
        }

        public Task WriteGoalsAsync(string fileName, CommanderGoalsData commanderGoalsData)
            => WriteGoalsAsync(fileName, commanderGoalsData, CancellationToken.None);

        public async Task WriteGoalsAsync(string fileName, CommanderGoalsData commanderGoalsData, CancellationToken cancellationToken)
        {
            using (var fileWriter = _fileSystemService.OpenWrite(fileName))
                await _goalsStorageService.WriteGoalsAsync(fileWriter, commanderGoalsData, cancellationToken).ConfigureAwait(false);
        }
    }
}