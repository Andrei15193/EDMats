using System.Collections.Generic;
using EDMats.Models.Materials;

namespace EDMats.Journals
{
    public class CommanderInfo
    {
        public ICollection<MaterialQuantity> Materials { get; } = new List<MaterialQuantity>();
    }
}