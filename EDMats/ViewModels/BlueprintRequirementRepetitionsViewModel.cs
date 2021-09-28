using EDMats.Models.Engineering;

namespace EDMats.ViewModels
{
    public class BlueprintRequirementRepetitionsViewModel : ViewModel
    {
        private int _repetitions;

        public BlueprintRequirementRepetitionsViewModel(BlueprintGradeRequirements gradeRequirements, int repetitions = 8)
            => (GradeRequirements, Repetitions) = (gradeRequirements, repetitions);

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