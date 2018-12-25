using System;

namespace EDMats.Services.Implementations
{
    public class MaterialTraderService : IMaterialTraderService
    {
        public TradeRate GetTradeRate(Material demand, Material offer)
        {
            if (demand == offer || demand == null || offer == null || demand.Subcategory.Category != offer.Subcategory.Category)
                return TradeRate.Invalid;

            var tradeRate = _GetTradeRate(demand, offer);
            var maximumDemandCapacity = _GetMaximumMaterialCapacity(demand.Grade);
            var maximumOfferCapacity = _GetMaximumMaterialCapacity(offer.Grade);
            if (tradeRate.Demand <= maximumDemandCapacity && tradeRate.Offer <= maximumOfferCapacity)
                return tradeRate;

            return TradeRate.Invalid;
        }

        private static TradeRate _GetTradeRate(Material demand, Material offer)
        {
            if (demand.Subcategory == offer.Subcategory)
                if (demand.Grade > offer.Grade)
                    return new TradeRate(
                        1,
                        (int)Math.Pow(6, demand.Grade - offer.Grade)
                    );
                else
                    return new TradeRate(
                        (int)Math.Pow(3, offer.Grade - demand.Grade),
                        1
                    );
            else if (demand.Grade >= offer.Grade)
                return new TradeRate(
                    1,
                    (int)Math.Pow(6, demand.Grade - offer.Grade + 1)
                );
            else
                return new TradeRate(
                    (int)Math.Pow(3, offer.Grade - demand.Grade - 1),
                    2
                );
        }

        private static int _GetMaximumMaterialCapacity(MaterialGrade materialGrade)
        {
            switch (materialGrade)
            {
                case MaterialGrade.VeryCommon:
                    return 300;

                case MaterialGrade.Common:
                    return 250;

                case MaterialGrade.Standard:
                    return 200;

                case MaterialGrade.Rare:
                    return 150;

                case MaterialGrade.VeryRare:
                    return 100;

                default:
                    return 0;
            }
        }
    }
}