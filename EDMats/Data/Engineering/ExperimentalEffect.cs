using System.Collections.Generic;
using EDMats.Data.Materials;

namespace EDMats.Data.Engineering
{
    public class ExperimentalEffect
    {
        public ExperimentalEffect(Module module, string id, string name, IReadOnlyCollection<MaterialQuantity> requirements)
        {
            Module = module;
            Id = id;
            Name = name;
            Requirements = requirements;
        }

        public Module Module { get; }

        public string Id { get; }

        public string Name { get; }

        public IReadOnlyCollection<MaterialQuantity> Requirements { get; }
    }
}