using System;
using System.Collections.ObjectModel;
using System.Linq;
using EDMats.Data.Engineering;
using EDMats.Storage;

namespace EDMats.ViewModels
{
    public class MaterialsTradeSearchViewModel : ViewModel
    {
        private readonly IProfileStorageHandler _profileStorageHandler;
        private readonly ObservableCollection<EngineeringConfiguration> _engineeringConfigurations;
        private bool _includeRawMaterials;
        private bool _includeManufacturedMaterials;
        private bool _includeEncodedMaterials;
        private EngineeringConfiguration _selectedEngineeringConfiguration;
        private bool _hasActiveSession;

        public MaterialsTradeSearchViewModel()
            : this(App.Resolve<IProfileStorageHandler>())
        {
        }

        public MaterialsTradeSearchViewModel(IProfileStorageHandler profileStorageHandler)
        {
            _profileStorageHandler = profileStorageHandler;
            _engineeringConfigurations = new ObservableCollection<EngineeringConfiguration>();
            EngineeringConfigurations = new ReadOnlyObservableCollection<EngineeringConfiguration>(_engineeringConfigurations);
        }

        public ReadOnlyObservableCollection<EngineeringConfiguration> EngineeringConfigurations { get; }

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

        public bool CanStartSession
            => SelectedEngineeringConfiguration is object && (IncludeRawMaterials || IncludeManufacturedMaterials || IncludeEncodedMaterials);

        public bool HasActiveSession
        {
            get => _hasActiveSession;
            set
            {
                if (_hasActiveSession != value)
                {
                    _hasActiveSession = value;
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

        private void _EnsureActiveSession()
        {
            if (!CanStartSession && HasActiveSession)
                HasActiveSession = false;
        }
    }
}