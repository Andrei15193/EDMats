namespace EDMats.Data.Engineering
{
    public class BlueprintGoal
    {
        public BlueprintGoal(Blueprint blueprint, int grade1Repetitions, int grade2Repetitions, int grade3Repetitions, int grade4Repetitions, int grade5Repetitions, int experimentalEffectRepetition)
        {
            Blueprint = blueprint;
            Grade1Repetitions = grade1Repetitions;
            Grade2Repetitions = grade2Repetitions;
            Grade3Repetitions = grade3Repetitions;
            Grade4Repetitions = grade4Repetitions;
            Grade5Repetitions = grade5Repetitions;
            ExperimentalEffectRepetition = experimentalEffectRepetition;
        }

        public Blueprint Blueprint { get; }

        public int Grade1Repetitions { get; }

        public int Grade2Repetitions { get; }

        public int Grade3Repetitions { get; }

        public int Grade4Repetitions { get; }

        public int Grade5Repetitions { get; }

        public int ExperimentalEffectRepetition { get; }
    }
}