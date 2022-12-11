using EDMats.Models.Engineering;

namespace EDMats.ViewModels
{
    public class BlueprintRequirementRepetitionsViewModel : ViewModel
    {
        private int _repetitions;

        public BlueprintRequirementRepetitionsViewModel(BlueprintGradeRequirements gradeRequirements)
            => (GradeRequirements, Repetitions) = (gradeRequirements, gradeRequirements.DefaultRepetitions);

        public BlueprintGradeRequirements GradeRequirements { get; }

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