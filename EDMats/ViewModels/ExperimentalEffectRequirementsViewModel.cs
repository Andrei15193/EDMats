using System.Collections.Generic;
using EDMats.Data.Materials;

namespace EDMats.ViewModels
{
    public class ExperimentalEffectRequirementsViewModel : ViewModel
    {
        private int _repetitions;

        public ExperimentalEffectRequirementsViewModel(IReadOnlyCollection<MaterialQuantity> requirements, int repetitions)
        {
            Requirements = requirements;
            _repetitions = repetitions;
        }

        public IReadOnlyCollection<MaterialQuantity> Requirements { get; }

        public int Repetitions
        {
            get => _repetitions;
            set
            {
                if (_repetitions != value)
                {
                    _repetitions = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}