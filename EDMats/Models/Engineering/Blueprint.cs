using System.Collections.Generic;

namespace EDMats.Models.Engineering
{
    public record Blueprint(Module Module, string Id, string Name, IReadOnlyList<BlueprintGradeRequirements> GradeRequirements, bool IsPreEngineered = false);
}