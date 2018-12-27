using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EDMats.ActionsData;
using EDMats.Services;

namespace EDMats.Stores
{
    public class MaterialsGoalStore : Store
    {
        private readonly IReadOnlyCollection<StoredMaterial> _materialsGoal;
        private readonly ObservableCollection<StoredMaterial> _filteredMaterialsGoal;

        public MaterialsGoalStore()
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
                    if (_materialsGoal != filteredStoredMaterials)
                    {
                        _filteredMaterialsGoal.Clear();
                        foreach (var filteredStoredMaterial in filteredStoredMaterials)
                            _filteredMaterialsGoal.Add(filteredStoredMaterial);
                    }
                    break;
            }
        }
    }
}