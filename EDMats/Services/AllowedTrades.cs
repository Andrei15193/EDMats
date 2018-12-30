using System.Collections.Generic;
using System.Linq;

namespace EDMats.Services
{
    public static class AllowedTrades
    {
        public static IReadOnlyCollection<AllowedTrade> All
        {
            get
            {
                var allowedTrades = new List<AllowedTrade>();

                foreach (var materialsByType in Materials.All.GroupBy(material => material.Type))
                    foreach (var offeredMaterial in materialsByType)
                        foreach (var demandedMaterial in materialsByType)
                            if (offeredMaterial != demandedMaterial)
                                allowedTrades.Add(new AllowedTrade(demandedMaterial, offeredMaterial));

                return allowedTrades;
            }
        }
    }
}