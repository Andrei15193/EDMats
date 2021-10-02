using System.Collections.Generic;
using System.Linq;
using EDMats.Models.Materials;

namespace EDMats.Trading
{
    public class AllowedTrade
    {
        public AllowedTrade(Material demand, Material offer)
            => (Demand, Offer) = (demand, offer);

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