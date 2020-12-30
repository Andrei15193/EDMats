using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EDMats.Services;
using EDMats.Stores;

namespace EDMats.ViewModels
{
    public class CommanderViewModel : AsyncViewModel
    {
        private readonly NotificationsViewModel _notificationsViewModel;
        private readonly IJournalFileImportService _journalFileImportService;
        private string _journalFilePath;
        private string _filterString;
        private DateTime? _latestUpdate;
        private ObservableCollection<StoredMaterial> _storedMaterials;
        private readonly ObservableCollection<StoredMaterial> _filteredStoredMaterials;

        public CommanderViewModel()
            : this(App.Resolve<NotificationsViewModel>(), App.Resolve<IJournalFileImportService>())
        {
        }

        public CommanderViewModel(NotificationsViewModel notificationsViewModel, IJournalFileImportService journalFileImportService)
        {
            _notificationsViewModel = notificationsViewModel;
            _journalFileImportService = journalFileImportService;
            _journalFilePath = string.Empty;
            _filterString = string.Empty;
            _latestUpdate = null;
            _storedMaterials = new ObservableCollection<StoredMaterial>();
            StoredMaterials = new ObservableCollection<StoredMaterial>(_storedMaterials);
            _filteredStoredMaterials = new ObservableCollection<StoredMaterial>();
            FilteredStoredMaterials = new ReadOnlyObservableCollection<StoredMaterial>(_filteredStoredMaterials);
        }

        public string JournalFilePath
        {
            get => _journalFilePath;
            set
            {
                if (_journalFilePath != (value ?? string.Empty))
                {
                    _journalFilePath = value ?? string.Empty;
                    NotifyPropertyChanged();
                }
            }
        }

        public string FilterText
        {
            get => _filterString;
            set
            {
                if (_filterString != (value ?? string.Empty))
                {
                    _filterString = value ?? string.Empty;
                    NotifyPropertyChanged();
                    _FilterMaterials();
                }
            }
        }

        public ObservableCollection<StoredMaterial> StoredMaterials { get; }

        public ReadOnlyObservableCollection<StoredMaterial> FilteredStoredMaterials { get; }

        public async Task LoadJournalAsync()
        {
            using (BusySection())
                await _LoadCommanderInfo();
            _FilterMaterials();
        }

        public async Task RefreshJournalAsync()
        {
            using (BusySection())
                await _UpdateCommanderInfoAsync();
            _FilterMaterials();
        }

        private async Task _LoadCommanderInfo()
        {
            _notificationsViewModel.AddNotification($"Loading journal file \"{JournalFilePath}\"");
            var commanderInfo = await _journalFileImportService.ImportAsync(JournalFilePath);
            _latestUpdate = commanderInfo.LatestUpdate;
            _storedMaterials.Clear();
            foreach (var materialQuantity in commanderInfo.Materials.OrderBy(materialQuantity => materialQuantity.Material.Name, StringComparer.Ordinal))
                _storedMaterials.Add(new StoredMaterial
                {
                    Id = materialQuantity.Material.Id,
                    Name = materialQuantity.Material.Name,
                    Amount = materialQuantity.Amount
                });
            _notificationsViewModel.AddNotification($"Journal file loaded, latest entry on {_latestUpdate:f}");
        }

        private async Task _UpdateCommanderInfoAsync()
        {
            var journalUpdates = await _journalFileImportService.ImportLatestJournalUpdatesAsync(JournalFilePath, _latestUpdate.Value);
            foreach (var journalUpdate in journalUpdates.OfType<MaterialCollectedJournalUpdate>())
            {
                _latestUpdate = journalUpdate.Timestamp;

                var index = 0;
                var collectedMaterial = new StoredMaterial
                {
                    Id = journalUpdate.CollectedMaterial.Material.Id,
                    Name = journalUpdate.CollectedMaterial.Material.Name,
                    Amount = journalUpdate.CollectedMaterial.Amount
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

                _notificationsViewModel.AddNotification($"Collected {journalUpdate.CollectedMaterial.Amount} of {journalUpdate.CollectedMaterial.Material.Name}");
            }
        }

        private void _FilterMaterials()
        {
            _filteredStoredMaterials.Clear();
            foreach (var filteredMaterial in _storedMaterials.ApplyFilter(FilterText))
                _filteredStoredMaterials.Add(filteredMaterial);
        }
    }
}