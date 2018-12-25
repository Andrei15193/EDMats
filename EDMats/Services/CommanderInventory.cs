using System.Collections.Generic;

namespace EDMats.Services
{
    public class CommanderInventory
    {
        public IReadOnlyDictionary<Material, int> Materials { get; set; }
    }
}