using System.Collections.Generic;
using EDMats.Models.Materials;

namespace EDMats.Models.Engineering
{
    public class BlueprintGradeRequirements
    {
        public BlueprintGradeRequirements(BlueprintGrade grade, IReadOnlyCollection<MaterialQuantity> requirements)
        {
            Grade = grade;
            Requirements = requirements;
        }

        public BlueprintGrade Grade { get; }

        public IReadOnlyCollection<MaterialQuantity> Requirements { get; }
    }
}