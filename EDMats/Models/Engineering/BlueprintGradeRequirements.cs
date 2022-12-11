using System.Collections.Generic;
using EDMats.Models.Materials;

namespace EDMats.Models.Engineering
{
    public record BlueprintGradeRequirements(BlueprintGrade Grade, IReadOnlyCollection<MaterialQuantity> Requirements, int DefaultRepetitions = 8);
}