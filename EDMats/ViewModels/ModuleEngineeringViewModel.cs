using System;
using System.Collections.ObjectModel;
using System.Linq;
using EDMats.Data.Engineering;
using EDMats.Storage;

namespace EDMats.ViewModels
{
    public class ModuleEngineeringViewModel : ViewModel
    {
        private Module _module;
        private Blueprint _selectedBlueprint;
        private readonly IProfileStorageHandler _profileStorageHandler;
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
            SaveCommand = CreateCommand(
                () => SelectedBlueprint is object,
                _Save,
                nameof(SelectedBlueprint)
            );
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
                }
            }
        }

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
                    {
                        var storageBlueprint = _GetStorageBlueprint(_selectedBlueprint.Id);
                        var blueprintGradeIndex = 0;
                        foreach (var blueprintGradeRequirement in _selectedBlueprint.GradeRequirements)
                        {
                            var repetitions = _GetRepetitionsFor(blueprintGradeRequirement.Grade, storageBlueprint) ?? 8;
                            var blueprintGradeRequirementViewModel = new BlueprintGradeRequirementsViewModel(blueprintGradeRequirement, repetitions);

                            if (blueprintGradeIndex == _blueprintGradesRequirements.Count)
                                _blueprintGradesRequirements.Add(blueprintGradeRequirementViewModel);
                            else
                                _blueprintGradesRequirements[blueprintGradeIndex] = blueprintGradeRequirementViewModel;
                            blueprintGradeIndex++;
                        }
                        while (blueprintGradeIndex < _blueprintGradesRequirements.Count)
                            _blueprintGradesRequirements.RemoveAt(blueprintGradeIndex);
                        SelectedExperimentalEffect = Module.ExperimentalEffects.SingleOrDefault(experimentalEffect => experimentalEffect.Id == storageBlueprint.ExperimentalEffect?.Id);
                    }
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
                    {
                        var storageBlueprint = _GetStorageBlueprint(_selectedBlueprint.Id);
                        if (storageBlueprint.ExperimentalEffect?.Id == _selectedExperimentalEffect.Id)
                            ExperimentalEffectRequirements = new ExperimentalEffectRequirementsViewModel(_selectedExperimentalEffect.Requirements, storageBlueprint.ExperimentalEffect.Repetitions);
                        else
                            ExperimentalEffectRequirements = new ExperimentalEffectRequirementsViewModel(_selectedExperimentalEffect.Requirements, 8);
                    }
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
            if (_selectedBlueprint is object)
            {
                var storageBlueprint = _GetStorageBlueprint(_selectedBlueprint.Id, out var profile);

                foreach (var blueprintGradeRequirements in _blueprintGradesRequirements)
                    _SetRepetitionsFor(storageBlueprint, blueprintGradeRequirements);
                if (_selectedExperimentalEffect is object)
                    storageBlueprint.ExperimentalEffect = new StorageExperimentalEffect
                    {
                        Id = _selectedExperimentalEffect.Id,
                        Repetitions = _experimentalEffectRequirements.Repetitions
                    };
                else
                    storageBlueprint.ExperimentalEffect = null;

                _profileStorageHandler.SaveProfile(profile);
            }

            Saved?.Invoke(this, EventArgs.Empty);
        }

        private StorageBlueprint _GetStorageBlueprint(string blueprintId)
            => _GetStorageBlueprint(blueprintId, out var _);

        private StorageBlueprint _GetStorageBlueprint(string blueprintId, out StorageProfile profile)
        {
            profile = _profileStorageHandler.LoadProfile("default");

            var storageBlueprintId = $"{Module.Id}/{blueprintId}";
            var storageBlueprint = profile.Blueprints.SingleOrDefault(blueprint => blueprint.Id == storageBlueprintId);
            if (storageBlueprint is null)
            {
                storageBlueprint = new StorageBlueprint
                {
                    Id = storageBlueprintId
                };
                profile.Blueprints.Add(storageBlueprint);
            }

            return storageBlueprint;
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