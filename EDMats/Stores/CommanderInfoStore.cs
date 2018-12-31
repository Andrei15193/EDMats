using System;
using System.Collections.ObjectModel;
using System.Linq;
using EDMats.ActionsData;

namespace EDMats.Stores
{
    public class CommanderInfoStore : Store
    {
        private readonly ObservableCollection<StoredMaterial> _storedMaterials;
        private readonly ObservableCollection<StoredMaterial> _filteredStoredMaterials;

        public CommanderInfoStore()
        {
            _storedMaterials = new ObservableCollection<StoredMaterial>();
            StoredMaterials = new ReadOnlyCollection<StoredMaterial>(_storedMaterials);
            _filteredStoredMaterials = new ObservableCollection<StoredMaterial>();
            FilteredStoredMaterials = new ReadOnlyObservableCollection<StoredMaterial>(_filteredStoredMaterials);
        }

        public string JournalFilePath { get; private set; }

        public string FilterText { get; private set; }

        public DateTime LatestUpdate { get; private set; }

        public ReadOnlyCollection<StoredMaterial> StoredMaterials { get; }

        public ReadOnlyObservableCollection<StoredMaterial> FilteredStoredMaterials { get; }

        protected override void Handle(ActionData actionData)
        {
            switch (actionData)
            {
                case OpeningJournalFileActionData openingJournalFile:
                    _filteredStoredMaterials.Clear();
                    SetProperty(() => JournalFilePath, openingJournalFile.FilePath);
                    break;

                case JournalImportedActionData commanderInfo:
                    _storedMaterials.Clear();
                    foreach (var materialQuantity in commanderInfo.CommanderInformation.Materials.OrderBy(materialQuantity => materialQuantity.Material.Name))
                        _storedMaterials.Add(
                            new StoredMaterial
                            {
                                Id = materialQuantity.Material.Id,
                                Name = materialQuantity.Material.Name,
                                Amount = materialQuantity.Amount
                            }
                        );
                    _FilterItems();
                    SetProperty(() => LatestUpdate, commanderInfo.CommanderInformation.LatestUpdate);
                    break;

                case FilterMaterialsActionData filterMaterials:
                    SetProperty(() => FilterText, filterMaterials.FilterText);
                    _FilterItems();
                    break;

                case MaterialCollectedActionData materialCollectedActionData:
                    var index = 0;
                    var collectedMaterial = new StoredMaterial
                    {
                        Id = materialCollectedActionData.CollectedMaterial.Material.Id,
                        Name = materialCollectedActionData.CollectedMaterial.Material.Name,
                        Amount = materialCollectedActionData.CollectedMaterial.Amount
                    };
                    while (index < _storedMaterials.Count && string.Compare(_storedMaterials[index].Name, collectedMaterial.Name) <= 0)
                        index++;
                    if (index < _storedMaterials.Count && _storedMaterials[index].Name == collectedMaterial.Name)
                    {
                        collectedMaterial.Amount += _storedMaterials[index].Amount;
                        _storedMaterials[index] = collectedMaterial;
                    }
                    else
                        _storedMaterials.Insert(index, collectedMaterial);
                    _FilterItems();
                    SetProperty(() => LatestUpdate, materialCollectedActionData.LatestUpdate);
                    break;
            }
        }

        private void _FilterItems()
        {
            var filteredStoredMaterials = _storedMaterials.ApplyFilter(FilterText);
            if (_storedMaterials != filteredStoredMaterials || _storedMaterials.Count != _filteredStoredMaterials.Count)
            {
                _filteredStoredMaterials.Clear();
                foreach (var filteredStoredMaterial in filteredStoredMaterials)
                    _filteredStoredMaterials.Add(filteredStoredMaterial);
            }
        }
    }
}