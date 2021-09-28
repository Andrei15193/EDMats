using System.Collections.Generic;
using System.Linq;
using EDMats.Models.Engineering;
using EDMats.Storage;

namespace EDMats.ViewModels
{
    public class SolutionSearchViewModel : ViewModel
    {
        private readonly CommanderProfileStorageHandler _commanderProfileStorageHandler;
        private Module _selectedModule;
        private Blueprint _selectedBlueprint;
        private IEnumerable<BlueprintRequirementRepetitionsViewModel> _blueprintGradeRequirements = Enumerable.Empty<BlueprintRequirementRepetitionsViewModel>();
        private ExperimentalEffect _selectedExperimentalEffect;
        private int _experimentalEffectRepetitions;
        private object _rawMaterialsTradeSolution;
        private object _encodedMaterialsTradeSolution;
        private object _manufacturedMaterialsTradeSolution;

        public SolutionSearchViewModel()
            : this(App.Resolve<CommanderProfileStorageHandler>())
        {
        }

        public SolutionSearchViewModel(CommanderProfileStorageHandler commanderProfileStorageHandler)
        {
            _commanderProfileStorageHandler = commanderProfileStorageHandler;
            SearchTradeSolutionCommand = CreateCommand(
                () => (RawMaterialsTradeSolution is null || EncodedMaterialsTradeSolution is null || ManufacturedMaterialsTradeSolution is null)
                      && (SelectedBlueprint is object || SelectedExperimentalEffect is object),
                _SearchSolution,
                new[] { nameof(RawMaterialsTradeSolution), nameof(EncodedMaterialsTradeSolution), nameof(ManufacturedMaterialsTradeSolution), nameof(SelectedBlueprint), nameof(SelectedExperimentalEffect) }
            );
        }

        public Command SearchTradeSolutionCommand { get; }

        public object RawMaterialsTradeSolution
        {
            get => _rawMaterialsTradeSolution;
            private set
            {
                if (_rawMaterialsTradeSolution != value)
                {
                    _rawMaterialsTradeSolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public object EncodedMaterialsTradeSolution
        {
            get => _encodedMaterialsTradeSolution;
            private set
            {
                if (_encodedMaterialsTradeSolution != value)
                {
                    _encodedMaterialsTradeSolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public object ManufacturedMaterialsTradeSolution
        {
            get => _manufacturedMaterialsTradeSolution;
            private set
            {
                if (_manufacturedMaterialsTradeSolution != value)
                {
                    _manufacturedMaterialsTradeSolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Module SelectedModule
        {
            get => _selectedModule;
            set
            {
                if (_selectedModule != value)
                {
                    _selectedModule = value;
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
                    BlueprintGradeRequirements = _selectedBlueprint?.GradeRequirements.Select(gradeRequirement => new BlueprintRequirementRepetitionsViewModel(gradeRequirement));
                }
            }
        }

        public IEnumerable<BlueprintRequirementRepetitionsViewModel> BlueprintGradeRequirements
        {
            get => _blueprintGradeRequirements;
            private set
            {
                if (_blueprintGradeRequirements != value)
                {
                    _blueprintGradeRequirements = value ?? Enumerable.Empty<BlueprintRequirementRepetitionsViewModel>();
                    NotifyPropertyChanged();
                }
            }
        }

        public ExperimentalEffect SelectedExperimentalEffect
        {
            get => _selectedExperimentalEffect;
            set
            {
                if (_selectedExperimentalEffect != value)
                {
                    _selectedExperimentalEffect = value;
                    NotifyPropertyChanged();
                    ExperimentalEffectRepetitions = 8;
                }
            }
        }

        public int ExperimentalEffectRepetitions
        {
            get => _experimentalEffectRepetitions;
            set
            {
                if (_experimentalEffectRepetitions != value)
                {
                    _experimentalEffectRepetitions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        int interval = 0;
        private void _SearchSolution()
        {
            switch (interval)
            {
                case 3:
                    RawMaterialsTradeSolution = new object();
                    break;

                case 5:
                    ManufacturedMaterialsTradeSolution = new object();
                    break;

                case 6:
                    EncodedMaterialsTradeSolution = new object();
                    break;
            }
            interval++;
        }
    }
}