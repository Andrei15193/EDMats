using System;
using EDMats.Data.Engineering;

namespace EDMats.ViewModels
{
    public class ModuleEngineeringViewModel : ViewModel
    {
        private Module _module;
        private Blueprint _selectedBlueprint;
        private ExperimentalEffect _selectedExperimentalEffect;

        public ModuleEngineeringViewModel()
        {
            SaveCommand = CreateCommand(() => Saved?.Invoke(this, EventArgs.Empty));
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