using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Data.Materials;
using EDMats.Data.MaterialTrading;
using EDMats.Services;

namespace EDMats.ViewModels
{
    public class CommanderViewModel : ViewModel
    {
        private readonly AsyncState _asyncState;
        private readonly NotificationsViewModel _notificationsViewModel;
        private readonly IJournalFileImportService _journalFileImportService;
        private readonly ITradeSolutionService _tradeSolutionService;
        private string _journalFilePath;
        private string _filterString;
        private DateTime? _latestUpdate;
        private readonly List<StoredMaterial> _storedMaterials;
        private readonly ObservableCollection<StoredMaterial> _filteredStoredMaterials;
        private readonly ObservableCollection<StoredMaterial> _materialGoals;
        private readonly ObservableCollection<StoredMaterial> _filteredMaterialGoals;
        private CancellationTokenSource _cancellationTokenSource = null;
        private TradeSolution _tradeSolution;
        private TradeSolutionSearchStatus _searchStatus;

        public CommanderViewModel()
            : this(App.Resolve<NotificationsViewModel>(), App.Resolve<IJournalFileImportService>(), App.Resolve<ITradeSolutionService>())
        {
        }

        public CommanderViewModel(NotificationsViewModel notificationsViewModel, IJournalFileImportService journalFileImportService, ITradeSolutionService tradeSolutionService)
        {
            _asyncState = new AsyncState();
            _notificationsViewModel = notificationsViewModel;
            _journalFileImportService = journalFileImportService;
            _tradeSolutionService = tradeSolutionService;
            _journalFilePath = string.Empty;
            _filterString = string.Empty;
            _latestUpdate = null;
            _tradeSolution = null;
            _searchStatus = TradeSolutionSearchStatus.Idle;
            _storedMaterials = new List<StoredMaterial>();
            _filteredStoredMaterials = new ObservableCollection<StoredMaterial>(_storedMaterials);
            FilteredStoredMaterials = new ReadOnlyObservableCollection<StoredMaterial>(_filteredStoredMaterials);
            _materialGoals = new ObservableCollection<StoredMaterial>(Material.All.Select(material => new StoredMaterial { Id = material.Id, Name = material.Name, Amount = 0 }));
            MaterialGoals = new ReadOnlyObservableCollection<StoredMaterial>(_materialGoals);
            _filteredMaterialGoals = new ObservableCollection<StoredMaterial>(_materialGoals);
            FilteredMaterialGoals = new ReadOnlyObservableCollection<StoredMaterial>(_filteredMaterialGoals);
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

        public TradeSolution TradeSolution
        {
            get => _tradeSolution;
            set
            {
                if (_tradeSolution != value)
                {
                    _tradeSolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TradeSolutionSearchStatus SearchStatus
        {
            get => _searchStatus;
            private set
            {
                if (_searchStatus != value)
                {
                    _searchStatus = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ReadOnlyObservableCollection<StoredMaterial> MaterialGoals { get; }

        public ReadOnlyObservableCollection<StoredMaterial> FilteredMaterialGoals { get; }

        public ReadOnlyObservableCollection<StoredMaterial> FilteredStoredMaterials { get; }

        public async Task LoadJournalAsync()
        {
            using (_asyncState.BusySection())
                await _LoadCommanderInfo();
            _FilterMaterials();
        }

        public async Task<bool> RefreshJournalAsync()
        {
            var hasUpdates = false;
            using (_asyncState.BusySection())
                hasUpdates = await _UpdateCommanderInfoAsync();
            _FilterMaterials();
            return hasUpdates;
        }

        public async Task SearchForTradeSolutionAsync()
        {
            _cancellationTokenSource?.Cancel();
            using (var cancellationTokenSource = new CancellationTokenSource())
                try
                {
                    _cancellationTokenSource = cancellationTokenSource;

                    SearchStatus = TradeSolutionSearchStatus.Searching;
                    _notificationsViewModel.AddNotification("Searching for trade solution");

                    TradeSolution = await _tradeSolutionService.TryFindSolutionAsync(
                        _materialGoals
                            .Select(materialGoal => new MaterialQuantity(Material.FindById(materialGoal.Id), materialGoal.Amount)),
                        _storedMaterials
                            .Select(storedMaterial => new MaterialQuantity(Material.FindById(storedMaterial.Id), storedMaterial.Amount)),
                        _GetAllowedTrades(),
                        cancellationTokenSource.Token
                    );
                    if (TradeSolution is null)
                        SearchStatus = TradeSolutionSearchStatus.SearchFailed;
                    else
                        SearchStatus = TradeSolutionSearchStatus.SearchSucceeded;

                    _notificationsViewModel.AddNotification("Trade solution search completed");
                }
                catch (OperationCanceledException operationCanceledException) when (operationCanceledException.CancellationToken == _cancellationTokenSource.Token)
                {
                }

            _cancellationTokenSource = null;
        }
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

        private async Task<bool> _UpdateCommanderInfoAsync()
        {
            var hasUpdates = false;
            var journalUpdates = await _journalFileImportService.ImportLatestJournalUpdatesAsync(JournalFilePath, _latestUpdate.Value);
            foreach (var journalUpdate in journalUpdates.OfType<MaterialCollectedJournalUpdate>())
            {
                hasUpdates = true;
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
            return hasUpdates;
        }

        private void _FilterMaterials()
        {
            _filteredStoredMaterials.Clear();
            foreach (var filteredMaterial in _storedMaterials.ApplyFilter(FilterText))
                _filteredStoredMaterials.Add(filteredMaterial);

            _filteredMaterialGoals.Clear();
            foreach (var filteredMaterial in _materialGoals.ApplyFilter(FilterText))
                _filteredMaterialGoals.Add(filteredMaterial);
        }
    }
}