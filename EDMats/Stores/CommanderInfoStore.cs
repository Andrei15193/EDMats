using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EDMats.ActionsData;

namespace EDMats.Stores
{
    public class CommanderInfoStore : Store
    {
        private IReadOnlyList<StoredMaterial> _storedMaterials;
        private readonly ObservableCollection<StoredMaterial> _filteredStoredMaterials;

        public CommanderInfoStore()
        {
            _storedMaterials = new List<StoredMaterial>();
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

                case JournalImportedActionData commanderInfo:
                    _storedMaterials = commanderInfo
                        .CommanderInformation
                        .Materials
                        .Select(
                            material => new StoredMaterial
                            {
                                Name = material.Material.Name,
                                Amount = material.Amount
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
            var filteredStoredMaterials = _storedMaterials.ApplyFilter(FilterText);
            if (_storedMaterials != filteredStoredMaterials)
            {
                _filteredStoredMaterials.Clear();
                foreach (var filteredStoredMaterial in filteredStoredMaterials)
                    _filteredStoredMaterials.Add(filteredStoredMaterial);
            }
        }
    }
}