using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EDMats.ActionsData;
using EDMats.Data.Materials;
using EDMats.Services;
using EDMats.Stores;
using FluxBase;

namespace EDMats.Actions
{
    public class GoalActions
    {
        private readonly Dispatcher _dispatcher;
        private readonly ITradeSolutionService _tradeSolutionService;
        private readonly IGoalsFileStorageService _goalsFileStorageService;

        public GoalActions(Dispatcher dispatcher, ITradeSolutionService tradeSolutionService, IGoalsFileStorageService goalsFileStorageService)
        {
            _dispatcher = dispatcher;
            _tradeSolutionService = tradeSolutionService;
            _goalsFileStorageService = goalsFileStorageService;
        }

        public Task TryFindGoalTradeSolution(IEnumerable<StoredMaterial> desiredMaterials, IEnumerable<StoredMaterial> availableMaterials)
            => TryFindGoalTradeSolution(desiredMaterials, availableMaterials, CancellationToken.None);

        public async Task TryFindGoalTradeSolution(IEnumerable<StoredMaterial> desiredMaterials, IEnumerable<StoredMaterial> availableMaterials, CancellationToken cancellationToken)
        {
            _dispatcher.Dispatch(new TradeSolutionSearchStartedActionData { NotificationText = "Searching for trade solution" });
            var tradeSolution = await _tradeSolutionService.TryFindSolutionAsync(
                desiredMaterials
                    .Select(materialGoal => new MaterialQuantity(Material.FindById(materialGoal.Id), materialGoal.Amount)),
                availableMaterials
                    .Select(storedMaterial => new MaterialQuantity(Material.FindById(storedMaterial.Id), storedMaterial.Amount)),
                _GetAllowedTrades(),
                cancellationToken
            );
            _dispatcher.Dispatch(
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
            _dispatcher.Dispatch(
                new LoadingCommanderGoalsActionData(fileName)
                {
                    NotificationText = $"Loading commander goals from \"{fileName}\""
                }
            );
            var commanderGoals = await _goalsFileStorageService.ReadGoalsAsync(fileName, cancellationToken);
            _dispatcher.Dispatch(
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
            _dispatcher.Dispatch(
                new SavingCommanderGoalsActionData(fileName)
                {
                    NotificationText = $"Saving commander goals to \"{fileName}\""
                }
            );
            await _goalsFileStorageService.WriteGoalsAsync(fileName, commanderGoalsData, cancellationToken);
            _dispatcher.Dispatch(new NotificationActionData("Commander goals saved"));
        }

        public void UpdateMaterialAmountGoal(string materialId, int amountGoal)
            => _dispatcher.Dispatch(new UpdateMaterialGoalActionData(materialId, amountGoal));

        private IEnumerable<AllowedTrade> _GetAllowedTrades()
            => _NoGrade3To5DowngradesTrades(Material.Encoded)
                .Concat(_NoGrade3To5DowngradesTrades(Material.Manufactured))
                .Concat(_NoGrade3To5DowngradesTrades(Material.Raw));

        private IEnumerable<AllowedTrade> _NoGrade3To5DowngradesTrades(MaterialType materialType)
        {
            var materials = materialType.Categories.SelectMany(category => category.Materials);
            foreach (var offer in materials)
                foreach (var demand in materials)
                    if (demand != offer)
                        if (demand.Grade > offer.Grade || (demand.Grade <= MaterialGrade.Common && offer.Grade <= MaterialGrade.Common))
                            yield return new AllowedTrade(demand, offer);
        }
    }
}