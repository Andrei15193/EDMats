using System.Collections.Generic;

namespace EDMats.Models.Engineering
{
    public record ModuleType(string Id, string Name, IReadOnlyCollection<Module> Modules);
}