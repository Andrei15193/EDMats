using System.Collections.Generic;
using EDMats.Models.Materials;

namespace EDMats.Models.Engineering
{
    public record ExperimentalEffect(Module Module, string Id, string Name, IReadOnlyCollection<MaterialQuantity> Requirements);
}