using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EDMats.ActionsData;
using EDMats.Services;
using EDMats.Stores;

namespace EDMats.Actions
{
    public class GoalActions : ActionSet
    {
        private readonly ITradeSolutionService _tradeSolutionService;
        private readonly IGoalsFileStorageService _goalsFileStorageService;

        public GoalActions(ITradeSolutionService tradeSolutionService, IGoalsFileStorageService goalsFileStorageService)
        {
            _tradeSolutionService = tradeSolutionService;
            _goalsFileStorageService = goalsFileStorageService;
        }

        public Task TryFindGoalTradeSolution(IEnumerable<StoredMaterial> desiredMaterials, IEnumerable<StoredMaterial> availableMaterials)
            => TryFindGoalTradeSolution(desiredMaterials, availableMaterials, CancellationToken.None);

        public async Task TryFindGoalTradeSolution(IEnumerable<StoredMaterial> desiredMaterials, IEnumerable<StoredMaterial> availableMaterials, CancellationToken cancellationToken)
        {
            Dispatch(new TradeSolutionSearchStartedActionData { NotificationText = "Searching for trade solution" });
            var tradeSolution = await _tradeSolutionService.TryFindSolutionAsync(
                desiredMaterials
                    .Select(materialGoal => new MaterialQuantity(Materials.FindById(materialGoal.Id), materialGoal.Amount)),
                availableMaterials
                    .Select(storedMaterial => new MaterialQuantity(Materials.FindById(storedMaterial.Id), storedMaterial.Amount)),
                AllowedTrades.All,
                cancellationToken
            );
            Dispatch(
                new TradeSolutionSearchCompletedActionData(tradeSolution)
                {
                    NotificationText = "Trade solution search completed"
                }
            );
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