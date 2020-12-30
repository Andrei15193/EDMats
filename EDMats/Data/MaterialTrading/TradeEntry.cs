using EDMats.Data.Materials;

namespace EDMats.Data.MaterialTrading
{
    public class TradeEntry
    {
        public TradeEntry(MaterialQuantity demand, MaterialQuantity offer)
        {
            Demand = demand;
            Offer = offer;
        }

        public MaterialQuantity Demand { get; }

        public MaterialQuantity Offer { get; }
    }
}