using EDMats.Data.Materials;

namespace EDMats.Services
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
    }
}