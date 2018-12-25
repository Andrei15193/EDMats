using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EDMats.ActionsData;

namespace EDMats.Stores
{
    public class InventoryStore : Store
    {
        private IReadOnlyList<StoredMaterial> _allStoredMaterials;
        private readonly ObservableCollection<StoredMaterial> _filteredStoredMaterials;

        public InventoryStore()
        {
            _filteredStoredMaterials = new ObservableCollection<StoredMaterial>();
            FilteredStoredMaterials = new ReadOnlyObservableCollection<StoredMaterial>(_filteredStoredMaterials);
        }

        public string JournalFilePath { get; private set; }

        public string FilterText { get; private set; }

        public ReadOnlyObservableCollection<StoredMaterial> FilteredStoredMaterials { get; }

        protected override void Handle(ActionData actionData)
        {
            switch (actionData)
            {
                case OpeningJournalFileActionData openingJournalFile:
                    _filteredStoredMaterials.Clear();
                    SetProperty(() => JournalFilePath, openingJournalFile.FilePath);
                    break;

                case InventoryActionData inventory:
                    _allStoredMaterials = inventory
                        .Inventory
                        .Materials
                        .Select(
                            material => new StoredMaterial
                            {
                                Name = material.Key.Name,
                                Amount = material.Value
                            }
                        )
                        .ToList();
                    _FilterItems();
                    break;

                case FilterMaterialsActionData filterMaterials:
                    SetProperty(() => FilterText, filterMaterials.FilterText);
                    _FilterItems();
                    break;
            }
        }

        private void _FilterItems()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                _filteredStoredMaterials.Clear();
                foreach (var storedMaterial in _allStoredMaterials)
                    _filteredStoredMaterials.Add(storedMaterial);
            }
            else
            {
                var searchItems = FilterText.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                _filteredStoredMaterials.Clear();
                foreach (var storedMaterial in _allStoredMaterials)
                    if (searchItems.Any(searchItem => storedMaterial.Name.IndexOf(searchItem, StringComparison.OrdinalIgnoreCase) >= 0))
                        _filteredStoredMaterials.Add(storedMaterial);
            }
        }
    }
}