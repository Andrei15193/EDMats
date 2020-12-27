using System;
using EDMats.Data.Materials;

namespace EDMats.Services.Implementations
{
    public class MaterialTraderService : IMaterialTraderService
    {
        public TradeRate GetTradeRate(Material demand, Material offer)
            => GetTradeRate(1, demand, offer);

        public TradeRate GetTradeRate(int times, Material demand, Material offer)
        {
            if (demand == null)
                throw new ArgumentNullException(nameof(demand));
            if (offer == null)
                throw new ArgumentNullException(nameof(offer));
            if (times <= 0)
                throw new ArgumentException($"Trade times must be greater than 0 (zero), '{times}' provided.", nameof(times));

            if (demand == offer || demand.Type != offer.Type || times > 100)
                return TradeRate.Invalid;

            var baseTradeRate = _GetTradeRate(demand, offer);
            var tradeRate = times == 1 ? baseTradeRate : new TradeRate(times * baseTradeRate.Demand, times * baseTradeRate.Offer);
            if (tradeRate.Demand <= demand.MaximumCapacity && tradeRate.Offer <= offer.MaximumCapacity)
                return tradeRate;
            return TradeRate.Invalid;
        }

        private static TradeRate _GetTradeRate(Material demand, Material offer)
        {
            if (demand.Category == offer.Category)
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
    }
}