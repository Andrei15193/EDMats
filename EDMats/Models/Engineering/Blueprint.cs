using System.Collections.Generic;

namespace EDMats.Models.Engineering
{
    public class Blueprint
    {
        public Blueprint(Module module, string id, string name, IReadOnlyList<BlueprintGradeRequirements> gradeRequirements)
        {
            Module = module;
            Id = id;
            Name = name;
            GradeRequirements = gradeRequirements;
        }

        public Module Module { get; }

        public string Id { get; }

        public string Name { get; }

        public IReadOnlyList<BlueprintGradeRequirements> GradeRequirements { get; }
    }
}