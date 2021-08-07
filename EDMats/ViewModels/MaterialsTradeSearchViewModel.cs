using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EDMats.Data.Engineering;
using EDMats.Data.Materials;
using EDMats.Data.MaterialTrading;
using EDMats.Services;
using EDMats.Storage;

namespace EDMats.ViewModels
{
    public class MaterialsTradeSearchViewModel : ViewModel
    {
        private readonly IProfileStorageHandler _profileStorageHandler;
        private readonly IJournalFileImportService _journalFileImportService;
        private readonly ITradeSolutionService _tradeSolutionService;
        private readonly ObservableCollection<EngineeringConfiguration> _engineeringConfigurations;
        private bool _includeRawMaterials;
        private bool _includeManufacturedMaterials;
        private bool _includeEncodedMaterials;
        private EngineeringConfiguration _selectedEngineeringConfiguration;
        private bool _isSessionActive;
        private TradeSolutionSearchStatus _status;
        private TradeSolution _tradeSolution;

        public MaterialsTradeSearchViewModel()
            : this(App.Resolve<IProfileStorageHandler>(), App.Resolve<IJournalFileImportService>(), App.Resolve<ITradeSolutionService>())
        {
        }

        public MaterialsTradeSearchViewModel(IProfileStorageHandler profileStorageHandler, IJournalFileImportService journalFileImportService, ITradeSolutionService tradeSolutionService)
        {
            _profileStorageHandler = profileStorageHandler;
            _journalFileImportService = journalFileImportService;
            _tradeSolutionService = tradeSolutionService;
            _engineeringConfigurations = new ObservableCollection<EngineeringConfiguration>();
            EngineeringConfigurations = new ReadOnlyObservableCollection<EngineeringConfiguration>(_engineeringConfigurations);
            _status = TradeSolutionSearchStatus.Idle;
        }

        public TradeSolutionSearchStatus Status
        {
            get => _status;
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TradeSolution TradeSolution
        {
            get => _tradeSolution;
            private set
            {
                if (_tradeSolution != value)
                {
                    _tradeSolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IncludeRawMaterials
        {
            get => _includeRawMaterials;
            set
            {
                if (_includeRawMaterials != value)
                {
                    _includeRawMaterials = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(CanStartSession));
                    _EnsureActiveSession();
                }
            }
        }

        public bool IncludeManufacturedMaterials
        {
            get => _includeManufacturedMaterials;
            set
            {
                if (_includeManufacturedMaterials != value)
                {
                    _includeManufacturedMaterials = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(CanStartSession));
                    _EnsureActiveSession();
                }
            }
        }

        public bool IncludeEncodedMaterials
        {
            get => _includeEncodedMaterials;
            set
            {
                if (_includeEncodedMaterials != value)
                {
                    _includeEncodedMaterials = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(CanStartSession));
                    _EnsureActiveSession();
                }
            }
        }

        public EngineeringConfiguration SelectedEngineeringConfiguration
        {
            get => _selectedEngineeringConfiguration;
            set
            {
                if (value != _selectedEngineeringConfiguration)
                {
                    _selectedEngineeringConfiguration = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(CanStartSession));
                    _EnsureActiveSession();
                }
            }
        }

        public ReadOnlyObservableCollection<EngineeringConfiguration> EngineeringConfigurations { get; }

        public bool CanStartSession
            => SelectedEngineeringConfiguration is object && (IncludeRawMaterials || IncludeManufacturedMaterials || IncludeEncodedMaterials);

        public bool IsSessionActive
        {
            get => _isSessionActive;
            set
            {
                if (_isSessionActive != value)
                {
                    _isSessionActive = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void Load()
        {
            var configuredModules = _profileStorageHandler.LoadProfile("default").Modules;
            var engineeringConfigurations = from module in configuredModules
                                            let matchedModule = Module.FindById(module.Key)
                                            let matchedBlueprint = matchedModule.Blueprints.SingleOrDefault(blueprint => blueprint.Id == module.Value.Blueprint?.Id)
                                            let matechedExperimentalEffect = matchedModule.ExperimentalEffects.SingleOrDefault(experimentalEffect => experimentalEffect.Id == module.Value.ExperimentalEffect?.Id)
                                            select new EngineeringConfiguration(matchedModule, matchedBlueprint, matechedExperimentalEffect);

            var removedItems = _engineeringConfigurations.Where(engineeringConfiguration => !configuredModules.ContainsKey(engineeringConfiguration.Module.Id)).ToList();
            foreach (var removedItem in removedItems)
                _engineeringConfigurations.Remove(removedItem);

            foreach (var engineeringConfiguration in engineeringConfigurations)
            {
                var insertIndex = 0;
                while (insertIndex < _engineeringConfigurations.Count && string.Compare(engineeringConfiguration.Module.Name, _engineeringConfigurations[insertIndex].Module.Name, StringComparison.OrdinalIgnoreCase) > 0)
                    insertIndex++;

                if (insertIndex == _engineeringConfigurations.Count || _engineeringConfigurations[insertIndex].Module != engineeringConfiguration.Module)
                    _engineeringConfigurations.Insert(insertIndex, engineeringConfiguration);
            }
        }

        public async Task SearchTradeSolution()
        {
            if (Status != TradeSolutionSearchStatus.SearchSucceeded)
            {
                Status = TradeSolutionSearchStatus.Searching;

                var storageProfile = _profileStorageHandler.LoadProfile("default");
                var journalDirectoryInfo = new DirectoryInfo(storageProfile.Commander.JournalsDirectoryPath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Saved Games", "Frontier Developments", "Elite Dangerous"));
                var latestJournalFileInfo = journalDirectoryInfo.GetFiles("*.log", SearchOption.TopDirectoryOnly).OrderByDescending(file => file.LastWriteTimeUtc).First();
                if (latestJournalFileInfo is object)
                {
                    var journalData = await _journalFileImportService.ImportAsync(latestJournalFileInfo.FullName);

                    var materialRequirements = _GetMaterialRequirements(storageProfile);
                    TradeSolution = await _tradeSolutionService.TryFindSolutionAsync(materialRequirements, journalData.Materials, AllowedTrade.All);

                    if (TradeSolution is object)
                        Status = TradeSolutionSearchStatus.SearchSucceeded;
                    else
                        Status = TradeSolutionSearchStatus.SearchFailed;
                }
            }
        }

        private void _EnsureActiveSession()
        {
            if (!CanStartSession && IsSessionActive)
                IsSessionActive = false;
        }

        private IEnumerable<MaterialQuantity> _GetMaterialRequirements(StorageProfile storageProfile)
        {
            var materialRequirements = new List<MaterialQuantity>();
            var storageModule = storageProfile.Modules[_selectedEngineeringConfiguration.Module.Id];
            if (storageModule.Blueprint is object)
            {
                var matchedBlueprint = _selectedEngineeringConfiguration.Module.Blueprints.First(blueprint => blueprint.Id == storageModule.Blueprint.Id);
                foreach (var gradeRequirement in matchedBlueprint.GradeRequirements)
                    materialRequirements.AddRange(Enumerable.Repeat(gradeRequirement.Requirements, _GetRepetitions(storageModule, gradeRequirement) ?? 0).SelectMany(Enumerable.AsEnumerable));
            }
            if (storageModule.ExperimentalEffect is object)
            {
                var matchedExperimentalEffect = _selectedEngineeringConfiguration.Module.ExperimentalEffects.First(experimentalEffect => experimentalEffect.Id == storageModule.ExperimentalEffect.Id);
                materialRequirements.AddRange(Enumerable.Repeat(matchedExperimentalEffect.Requirements, storageModule.ExperimentalEffect.Repetitions).SelectMany(Enumerable.AsEnumerable));
            }

            return materialRequirements
                .Where(materialQuantity =>
                    (IncludeRawMaterials && materialQuantity.Material.Type == Material.Raw)
                    || (IncludeManufacturedMaterials && materialQuantity.Material.Type == Material.Manufactured)
                    || (IncludeEncodedMaterials && materialQuantity.Material.Type == Material.Encoded))
                .GroupBy(
                    materialQuantity => materialQuantity.Material,
                    materialQuantity => materialQuantity.Amount,
                    (material, quantities) => new MaterialQuantity(material, quantities.Sum())
                )
                .ToList();
        }

        private static int? _GetRepetitions(StorageModule storageModule, BlueprintGradeRequirements gradeRequirement)
        {
            switch (gradeRequirement.Grade)
            {
                case BlueprintGrade.Grade1:
                    return storageModule.Blueprint.Grade1?.Repetitions;

                case BlueprintGrade.Grade2:
                    return storageModule.Blueprint.Grade2?.Repetitions;

                case BlueprintGrade.Grade3:
                    return storageModule.Blueprint.Grade3?.Repetitions;

                case BlueprintGrade.Grade4:
                    return storageModule.Blueprint.Grade4?.Repetitions;

                case BlueprintGrade.Grade5:
                    return storageModule.Blueprint.Grade5?.Repetitions;

                default:
                    return null;
            }
        }

        private static IEnumerable<AllowedTrade> _GetAllowedTrades()
           => _NoGrade3To5DowngradesTrades(Material.Encoded)
               .Concat(_NoGrade3To5DowngradesTrades(Material.Manufactured))
               .Concat(_NoGrade3To5DowngradesTrades(Material.Raw));

        private static IEnumerable<AllowedTrade> _NoGrade3To5DowngradesTrades(MaterialType materialType)
        {
            var materials = materialType.Categories.SelectMany(category => category.Materials);
            foreach (var offer in materials)
                foreach (var demand in materials)
                    if (demand != offer)
                        if (demand.Grade > offer.Grade || (demand.Grade <= MaterialGrade.Common && offer.Grade <= MaterialGrade.Common))
                            yield return new AllowedTrade(demand, offer);
        }
    }
}