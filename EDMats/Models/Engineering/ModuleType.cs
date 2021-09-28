using System.Collections.Generic;

namespace EDMats.Models.Engineering
{
    public class ModuleType
    {
        public ModuleType(string id, string name, IReadOnlyCollection<Module> modules)
        {
            Id = id;
            Name = name;
            Modules = modules;
        }

        public string Id { get; }

        public string Name { get; }

        public IReadOnlyCollection<Module> Modules { get; }
    }
}