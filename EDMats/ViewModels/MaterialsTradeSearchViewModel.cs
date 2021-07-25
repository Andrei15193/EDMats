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
        private EngineeringConfiguration _selectedEngineeringConfiguration;

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

        public EngineeringConfiguration SelectedEngineeringConfiguration
        {
            get => _selectedEngineeringConfiguration;
            set
            {
                if (value != _selectedEngineeringConfiguration)
                {
                    _selectedEngineeringConfiguration = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ReadOnlyObservableCollection<EngineeringConfiguration> EngineeringConfigurations { get; }

        public void Load()
        {
            var engineeringConfigurations = from module in _profileStorageHandler.LoadProfile("default").Modules
                                            let matchedModule = Module.FindById(module.Key)
                                            let matchedBlueprint = matchedModule.Blueprints.SingleOrDefault(blueprint => blueprint.Id == module.Value.Blueprint?.Id)
                                            let matechedExperimentalEffect = matchedModule.ExperimentalEffects.SingleOrDefault(experimentalEffect => experimentalEffect.Id == module.Value.ExperimentalEffect?.Id)
                                            select new EngineeringConfiguration(matchedModule, matchedBlueprint, matechedExperimentalEffect);
            _engineeringConfigurations.Clear();
            foreach (var engineeringConfiguration in engineeringConfigurations)
                _engineeringConfigurations.Add(engineeringConfiguration);
        }
    }
}