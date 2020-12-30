using System.Collections.Generic;
using System.Linq;
using EDMats.Data.Materials;

namespace EDMats.Data.MaterialTrading
{
    public class AllowedTrade
    {
        public AllowedTrade(Material demand, Material offer)
        {
            Demand = demand;
            Offer = offer;
        }

        public Material Demand { get; }

        public Material Offer { get; }

        public static IReadOnlyCollection<AllowedTrade> All
        {
            get
            {
                var allowedTrades = new List<AllowedTrade>();

                foreach (var materialsByType in Material.All.GroupBy(material => material.Type))
                    foreach (var offeredMaterial in materialsByType)
                        foreach (var demandedMaterial in materialsByType)
                            if (offeredMaterial != demandedMaterial)
                                allowedTrades.Add(new AllowedTrade(demandedMaterial, offeredMaterial));

                return allowedTrades;
            }
        }
    }
}