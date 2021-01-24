using EDMats.Data.Engineering;

namespace EDMats.ViewModels
{
    public class BlueprintGradeRequirementsViewModel : ViewModel
    {
        private int _repetitions;

        public BlueprintGradeRequirementsViewModel(BlueprintGradeRequirements blueprintGradeRequirements, int repetitions)
        {
            BlueprintGradeRequirements = blueprintGradeRequirements;
            _repetitions = repetitions;
        }

        public BlueprintGradeRequirements BlueprintGradeRequirements { get; }

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