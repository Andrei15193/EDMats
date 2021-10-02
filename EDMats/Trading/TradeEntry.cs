using EDMats.Models.Materials;

namespace EDMats.Trading
{
    public class TradeEntry
    {
        public TradeEntry(MaterialQuantity demand, MaterialQuantity offer)
            => (Demand, Offer) = (demand, offer);

        public MaterialQuantity Demand { get; }

        public MaterialQuantity Offer { get; }
    }
}