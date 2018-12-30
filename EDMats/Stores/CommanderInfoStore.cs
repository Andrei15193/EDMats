using System.Collections.ObjectModel;
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
                    foreach (var materialQuantity in commanderInfo.CommanderInformation.Materials)
                        _storedMaterials.Add(
                            new StoredMaterial
                            {
                                Id = materialQuantity.Material.Id,
                                Name = materialQuantity.Material.Name,
                                Amount = materialQuantity.Amount
                            }
                        );
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
            if (_storedMaterials != filteredStoredMaterials || _storedMaterials.Count != _filteredStoredMaterials.Count)
            {
                _filteredStoredMaterials.Clear();
                foreach (var filteredStoredMaterial in filteredStoredMaterials)
                    _filteredStoredMaterials.Add(filteredStoredMaterial);
            }
        }
    }
}