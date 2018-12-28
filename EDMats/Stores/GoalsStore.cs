using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EDMats.ActionsData;
using EDMats.Services;

namespace EDMats.Stores
{
    public class GoalsStore : Store
    {
        private readonly IReadOnlyCollection<StoredMaterial> _materialsGoal;
        private readonly ObservableCollection<StoredMaterial> _filteredMaterialsGoal;

        public GoalsStore()
        {
            _materialsGoal = Materials
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
                )
                .ToList();
            _filteredMaterialsGoal = new ObservableCollection<StoredMaterial>(_materialsGoal);
            FilteredMaterialsGoal = new ReadOnlyObservableCollection<StoredMaterial>(_filteredMaterialsGoal);
        }

        public ReadOnlyObservableCollection<StoredMaterial> FilteredMaterialsGoal { get; }

        protected override void Handle(ActionData actionData)
        {
            switch (actionData)
            {
                case FilterMaterialsActionData filterMaterialsActionData:
                    var filteredStoredMaterials = _materialsGoal.ApplyFilter(filterMaterialsActionData.FilterText);
                    if (_materialsGoal != filteredStoredMaterials || _materialsGoal.Count != _filteredMaterialsGoal.Count)
                    {
                        _filteredMaterialsGoal.Clear();
                        foreach (var filteredStoredMaterial in filteredStoredMaterials)
                            _filteredMaterialsGoal.Add(filteredStoredMaterial);
                    }
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
            }
        }
    }
}