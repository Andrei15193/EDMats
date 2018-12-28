using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EDMats.ActionsData;
using EDMats.Services;

namespace EDMats.Stores
{
    public class GoalsStore : Store
    {
        private string _filterText = string.Empty;
        private readonly ObservableCollection<StoredMaterial> _materialsGoal;
        private readonly ObservableCollection<StoredMaterial> _filteredMaterialsGoal;

        public GoalsStore()
        {
            _materialsGoal = new ObservableCollection<StoredMaterial>(_AllStoredMaterials);
            MaterialsGoal = new ReadOnlyObservableCollection<StoredMaterial>(_materialsGoal);
            _filteredMaterialsGoal = new ObservableCollection<StoredMaterial>(_materialsGoal);
            FilteredMaterialsGoal = new ReadOnlyObservableCollection<StoredMaterial>(_filteredMaterialsGoal);
        }

        public ReadOnlyObservableCollection<StoredMaterial> MaterialsGoal { get; }

        public ReadOnlyObservableCollection<StoredMaterial> FilteredMaterialsGoal { get; }

        protected override void Handle(ActionData actionData)
        {
            switch (actionData)
            {
                case FilterMaterialsActionData filterMaterialsActionData:
                    _filterText = filterMaterialsActionData.FilterText;
                    _FilterMaterialsGoal();
                    break;

                case UpdateMaterialGoalActionData updateMaterialGoalActionData:
                    var updatedMaterial = _materialsGoal
                        .Single(storedMaterial => storedMaterial.Id == updateMaterialGoalActionData.MaterialId);
                    updatedMaterial.Amount = updateMaterialGoalActionData.Amount;

                    var filteredMaterialIndex = 0;
                    while (filteredMaterialIndex < _filteredMaterialsGoal.Count && _filteredMaterialsGoal[filteredMaterialIndex].Id != updateMaterialGoalActionData.MaterialId)
                        filteredMaterialIndex++;
                    if (filteredMaterialIndex < _filteredMaterialsGoal.Count)
                        _filteredMaterialsGoal[filteredMaterialIndex] = updatedMaterial;
                    break;

                case LoadingCommanderGoalsActionData loadingCommanderGoalsActionData:
                    _materialsGoal.Clear();
                    _filteredMaterialsGoal.Clear();
                    break;

                case CommanderGoalsLoadedActionData commanderGoalsLoadedActionData:
                    var storedMaterials = _AllStoredMaterials.ToDictionary(storedMaterial => storedMaterial.Id);
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
                    break;
            }
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
                .Encoded
                .Categories
                .Concat(Materials.Manufactured.Categories)
                .Concat(Materials.Raw.Categories)
                .SelectMany(category => category.Materials)
                .OrderBy(material => material.Name)
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