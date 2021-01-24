using System;
using System.Collections.ObjectModel;
using EDMats.Data.Engineering;

namespace EDMats.ViewModels
{
    public class ModuleEngineeringViewModel : ViewModel
    {
        private Module _module;
        private Blueprint _selectedBlueprint;
        private readonly ObservableCollection<BlueprintGradeRequirementsViewModel> _blueprintGradesRequirements;
        private ExperimentalEffect _selectedExperimentalEffect;
        private ExperimentalEffectRequirementsViewModel _experimentalEffectRequirements;

        public ModuleEngineeringViewModel()
        {
            SaveCommand = CreateCommand(() => Saved?.Invoke(this, EventArgs.Empty));
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
                    _blueprintGradesRequirements.Clear();
                    if (_selectedBlueprint is object)
                        foreach (var blueprintGradeRequirement in _selectedBlueprint.GradeRequirements)
                            _blueprintGradesRequirements.Add(new BlueprintGradeRequirementsViewModel(blueprintGradeRequirement, 8));
                    NotifyPropertyChanged();
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
                    if (_selectedExperimentalEffect is object)
                        ExperimentalEffectRequirements = new ExperimentalEffectRequirementsViewModel(_selectedExperimentalEffect.Requirements, 8);
                    else
                        ExperimentalEffectRequirements = null;
                    NotifyPropertyChanged();
                }
            }
        }

        public ExperimentalEffectRequirementsViewModel ExperimentalEffectRequirements
        {
            get => _experimentalEffectRequirements;
            set
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
        {
            Module = module;
        }
    }
}