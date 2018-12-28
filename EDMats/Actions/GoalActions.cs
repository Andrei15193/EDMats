using System.Threading;
using System.Threading.Tasks;
using EDMats.ActionsData;
using EDMats.Services;

namespace EDMats.Actions
{
    public class GoalActions : ActionSet
    {
        private readonly IGoalsFileStorageService _goalsFileStorageService;

        public GoalActions(IGoalsFileStorageService goalsFileStorageService)
        {
            _goalsFileStorageService = goalsFileStorageService;
        }

        public Task LoadCommanderGoalsAsync(string fileName)
            => LoadCommanderGoalsAsync(fileName, CancellationToken.None);

        public async Task LoadCommanderGoalsAsync(string fileName, CancellationToken cancellationToken)
        {
            Dispatch(
                new LoadingCommanderGoalsActionData(fileName)
                {
                    NotificationText = $"Loading commander goals from \"{fileName}\""
                }
            );
            var commanderGoals = await _goalsFileStorageService.ReadGoalsAsync(fileName, cancellationToken);
            Dispatch(
                new CommanderGoalsLoadedActionData(commanderGoals)
                {
                    NotificationText = $"Commander goals loaded"
                }
            );
        }

        public Task SaveCommanderGoalsAsync(string fileName, CommanderGoalsData commanderGoalsData)
            => SaveCommanderGoalsAsync(fileName, commanderGoalsData, CancellationToken.None);

        public async Task SaveCommanderGoalsAsync(string fileName, CommanderGoalsData commanderGoalsData, CancellationToken cancellationToken)
        {
            Dispatch(
                new SavingCommanderGoalsActionData(fileName)
                {
                    NotificationText = $"Saving commander goals to \"{fileName}\""
                }
            );
            await _goalsFileStorageService.WriteGoalsAsync(fileName, commanderGoalsData, cancellationToken);
            Dispatch(new NotificationActionData("Commander goals saved"));
        }

        public void UpdateMaterialAmountGoal(string materialId, int amountGoal)
            => Dispatch(new UpdateMaterialGoalActionData(materialId, amountGoal));
    }
}