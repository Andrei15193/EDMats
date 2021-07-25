using System;
using System.Collections.ObjectModel;
using System.Linq;
using EDMats.Data.Engineering;
using EDMats.Storage;

namespace EDMats.ViewModels
{
    public class ModuleEngineeringViewModel : ViewModel
    {
        private readonly IProfileStorageHandler _profileStorageHandler;
        private Module _module;
        private StorageModule _storageModule;
        private Blueprint _selectedBlueprint;
        private readonly ObservableCollection<BlueprintGradeRequirementsViewModel> _blueprintGradesRequirements;
        private ExperimentalEffect _selectedExperimentalEffect;
        private ExperimentalEffectRequirementsViewModel _experimentalEffectRequirements;

        public ModuleEngineeringViewModel()
            : this(App.Resolve<IProfileStorageHandler>())
        {
        }

        public ModuleEngineeringViewModel(IProfileStorageHandler profileStorageHandler)
        {
            _profileStorageHandler = profileStorageHandler;
            SaveCommand = CreateCommand(_Save);
            _blueprintGradesRequirements = new ObservableCollection<BlueprintGradeRequirementsViewModel>();
            BlueprintGradesRequirements = new ReadOnlyObservableCollection<BlueprintGradeRequirementsViewModel>(_blueprintGradesRequirements);
        }

        public Module Module
        {
            get => _module;
            private set
            {
                if (_module != value)
                {
                    _module = value;
                    NotifyPropertyChanged();

                    if (!_profileStorageHandler.LoadProfile("default").Modules.TryGetValue(_module.Id, out _storageModule))
                        _storageModule = new StorageModule();

                    SelectedBlueprint = _storageModule.Blueprint is object ? Module.Blueprints.SingleOrDefault(blueprint => blueprint.Id == _storageModule.Blueprint.Id) : default;
                    SelectedExperimentalEffect = _storageModule.ExperimentalEffect is object ? Module.ExperimentalEffects.SingleOrDefault(experimentalEffect => experimentalEffect.Id == _storageModule.ExperimentalEffect.Id) : default;
                }
            }
        }

        private const int DefaultRepetitions = 8;

        public Blueprint SelectedBlueprint
        {
            get => _selectedBlueprint;
            set
            {
                if (_selectedBlueprint != value)
                {
                    _selectedBlueprint = value;
                    NotifyPropertyChanged();

                    if (_selectedBlueprint is object)
                        if (_storageModule.Blueprint is object && _selectedBlueprint.Id == _storageModule.Blueprint.Id)
                            _blueprintGradesRequirements.Reset(
                                from gradeRequirement in _selectedBlueprint.GradeRequirements
                                select new BlueprintGradeRequirementsViewModel(gradeRequirement, _GetRepetitionsFor(gradeRequirement.Grade, _storageModule.Blueprint) ?? DefaultRepetitions)
                            );
                        else
                            _blueprintGradesRequirements.Reset(
                                from gradeRequirement in _selectedBlueprint.GradeRequirements
                                select new BlueprintGradeRequirementsViewModel(gradeRequirement, DefaultRepetitions)
                            );
                    else
                        _blueprintGradesRequirements.Clear();
                }
            }
        }

        public ReadOnlyObservableCollection<BlueprintGradeRequirementsViewModel> BlueprintGradesRequirements { get; }

        public ExperimentalEffect SelectedExperimentalEffect
        {
            get => _selectedExperimentalEffect;
            set
            {
                if (_selectedExperimentalEffect != value)
                {
                    _selectedExperimentalEffect = value;
                    NotifyPropertyChanged();

                    if (_selectedExperimentalEffect is object)
                        if (_selectedExperimentalEffect.Id == _storageModule.ExperimentalEffect?.Id)
                            ExperimentalEffectRequirements = new ExperimentalEffectRequirementsViewModel(_selectedExperimentalEffect.Requirements, _storageModule.ExperimentalEffect.Repetitions);
                        else
                            ExperimentalEffectRequirements = new ExperimentalEffectRequirementsViewModel(_selectedExperimentalEffect.Requirements, DefaultRepetitions);
                    else
                        ExperimentalEffectRequirements = null;
                }
            }
        }

        public ExperimentalEffectRequirementsViewModel ExperimentalEffectRequirements
        {
            get => _experimentalEffectRequirements;
            private set
            {
                if (_experimentalEffectRequirements != value)
                {
                    _experimentalEffectRequirements = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Command SaveCommand { get; }

        public event EventHandler Saved;

        public void LoadModule(Module module)
            => Module = module;

        private void _Save()
        {
            var profile = _profileStorageHandler.LoadProfile("default");

            if (_selectedBlueprint is object)
            {
                _storageModule.Blueprint = new StorageBlueprint
                {
                    Id = _selectedBlueprint.Id
                };
                foreach (var gradeRequirements in _blueprintGradesRequirements)
                    _SetRepetitionsFor(_storageModule.Blueprint, gradeRequirements);
            }
            else
                _storageModule.Blueprint = null;

            if (_selectedExperimentalEffect is object)
                _storageModule.ExperimentalEffect = new StorageExperimentalEffect
                {
                    Id = _selectedExperimentalEffect.Id,
                    Repetitions = _experimentalEffectRequirements.Repetitions
                };
            else
                _storageModule.ExperimentalEffect = null;

            if (_storageModule.Blueprint is object || _storageModule.ExperimentalEffect is object)
                profile.Modules[_module.Id] = _storageModule;
            else
                profile.Modules.Remove(_module.Id);

            _profileStorageHandler.SaveProfile(profile);
            Saved?.Invoke(this, EventArgs.Empty);
        }

        private static int? _GetRepetitionsFor(BlueprintGrade blueprintGrade, StorageBlueprint storageBlueprint)
        {
            switch (blueprintGrade)
            {
                case BlueprintGrade.Grade1:
                    return storageBlueprint.Grade1?.Repetitions;

                case BlueprintGrade.Grade2:
                    return storageBlueprint.Grade2?.Repetitions;

                case BlueprintGrade.Grade3:
                    return storageBlueprint.Grade3?.Repetitions;

                case BlueprintGrade.Grade4:
                    return storageBlueprint.Grade4?.Repetitions;

                case BlueprintGrade.Grade5:
                    return storageBlueprint.Grade5?.Repetitions;

                default:
                    return null;
            }
        }

        private static void _SetRepetitionsFor(StorageBlueprint storageBlueprint, BlueprintGradeRequirementsViewModel blueprintGradeRequirements)
        {
            switch (blueprintGradeRequirements.BlueprintGradeRequirements.Grade)
            {
                case BlueprintGrade.Grade1:
                    storageBlueprint.Grade1 = new StorageBlueprintGrade { Repetitions = blueprintGradeRequirements.Repetitions };
                    break;

                case BlueprintGrade.Grade2:
                    storageBlueprint.Grade2 = new StorageBlueprintGrade { Repetitions = blueprintGradeRequirements.Repetitions };
                    break;

                case BlueprintGrade.Grade3:
                    storageBlueprint.Grade3 = new StorageBlueprintGrade { Repetitions = blueprintGradeRequirements.Repetitions };
                    break;

                case BlueprintGrade.Grade4:
                    storageBlueprint.Grade4 = new StorageBlueprintGrade { Repetitions = blueprintGradeRequirements.Repetitions };
                    break;

                case BlueprintGrade.Grade5:
                    storageBlueprint.Grade5 = new StorageBlueprintGrade { Repetitions = blueprintGradeRequirements.Repetitions };
                    break;
            }
        }
    }
}