using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EDMats.ActionsData;
using EDMats.Services;
using FluxBase;

namespace EDMats.Stores
{
    public class GoalsStore : Store
    {
        private string _filterText = string.Empty;
        private readonly ObservableCollection<StoredMaterial> _materialsGoal;
        private readonly ObservableCollection<StoredMaterial> _filteredMaterialsGoal;

        public GoalsStore()
        {
            _materialsGoal = new ObservableCollection<StoredMaterial>(
                Materials
                    .All
                    .Select(
                        material => new StoredMaterial
                        {
                            Id = material.Id,
                            Name = material.Name,
                            Amount = 0
                        }
                    )
            );
            MaterialsGoal = new ReadOnlyObservableCollection<StoredMaterial>(_materialsGoal);
            _filteredMaterialsGoal = new ObservableCollection<StoredMaterial>(_materialsGoal);
            FilteredMaterialsGoal = new ReadOnlyObservableCollection<StoredMaterial>(_filteredMaterialsGoal);
        }

        public TradeSolutionSearchStatus SearchStatus { get; private set; } = TradeSolutionSearchStatus.Idle;

        public ReadOnlyObservableCollection<StoredMaterial> MaterialsGoal { get; }

        public ReadOnlyObservableCollection<StoredMaterial> FilteredMaterialsGoal { get; }

        public IReadOnlyCollection<TradeEntry> TradeEntries { get; private set; } = new List<TradeEntry>();

        private void _Handle(FilterMaterialsActionData filterMaterialsActionData)
        {
            _filterText = filterMaterialsActionData.FilterText;
            _FilterMaterialsGoal();
        }

        private void _Handle(UpdateMaterialGoalActionData updateMaterialGoalActionData)
        {
            var updatedMaterial = _materialsGoal
                .Single(storedMaterial => storedMaterial.Id == updateMaterialGoalActionData.MaterialId);
            updatedMaterial.Amount = updateMaterialGoalActionData.Amount;

            var filteredMaterialIndex = 0;
            while (filteredMaterialIndex < _filteredMaterialsGoal.Count && _filteredMaterialsGoal[filteredMaterialIndex].Id != updateMaterialGoalActionData.MaterialId)
                filteredMaterialIndex++;
            if (filteredMaterialIndex < _filteredMaterialsGoal.Count)
                _filteredMaterialsGoal[filteredMaterialIndex] = updatedMaterial;
        }

        private void _Handle(LoadingCommanderGoalsActionData loadingCommanderGoalsActionData)
        {
            _materialsGoal.Clear();
            _filteredMaterialsGoal.Clear();
        }

        private void _Handle(CommanderGoalsLoadedActionData commanderGoalsLoadedActionData)
        {
            var storedMaterials = Materials.All.ToDictionary(
                material => material.Id,
                material => new StoredMaterial
                {
                    Id = material.Id,
                    Name = material.Name,
                    Amount = 0
                }
            );
            foreach (var materialGoal in commanderGoalsLoadedActionData.CommanderGoals.Materials)
                storedMaterials[materialGoal.MaterialId] = new StoredMaterial
                {
                    Id = materialGoal.MaterialId,
                    Name = materialGoal.Name,
                    Amount = materialGoal.Amount
                };
            foreach (var storedMaterial in storedMaterials.Values.OrderBy(storedMaterial => storedMaterial.Name))
                _materialsGoal.Add(storedMaterial);
            _FilterMaterialsGoal();
        }

        private void _Handle(TradeSolutionSearchStartedActionData tradeSolutionSearchStartedAction)
        {
            SetProperty(() => SearchStatus, TradeSolutionSearchStatus.Searching);
        }

        private void _Handle(TradeSolutionSearchCompletedActionData tradeSolutionSearchCompletedActionData)
        {
            var tradeSolution = tradeSolutionSearchCompletedActionData.TradeSolution;
            if (tradeSolution == null)
                SetProperty(() => SearchStatus, TradeSolutionSearchStatus.SearchFailed);
            else
                SetProperty(() => SearchStatus, TradeSolutionSearchStatus.SearchSucceeded);
            SetProperty(
                () => TradeEntries,
                tradeSolution?.Trades.OrderBy(tradeEntry => tradeEntry.Demand.Material.Type.Name).ToList() ?? new List<TradeEntry>()
            );
        }

        private void _FilterMaterialsGoal()
        {
            var filteredStoredMaterials = _materialsGoal.ApplyFilter(_filterText);
            if (_materialsGoal != filteredStoredMaterials || _materialsGoal.Count != _filteredMaterialsGoal.Count)
            {
                _filteredMaterialsGoal.Clear();
                foreach (var filteredStoredMaterial in filteredStoredMaterials)
                    _filteredMaterialsGoal.Add(filteredStoredMaterial);
            }
        }

        private static IEnumerable<StoredMaterial> _AllStoredMaterials
            => Materials
                .All
                .Select(
                    material => new StoredMaterial
                    {
                        Id = material.Id,
                        Name = material.Name,
                        Amount = 0
                    }
                );
    }
}