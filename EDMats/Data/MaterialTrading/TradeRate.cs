using System;

namespace EDMats.Data.MaterialTrading
{
    public struct TradeRate : IEquatable<TradeRate>
    {
        public static bool operator ==(TradeRate left, TradeRate right)
            => left.Equals(right);

        public static bool operator !=(TradeRate left, TradeRate right)
            => !left.Equals(right);

        public static TradeRate Invalid { get; } = new TradeRate();

        public TradeRate(int demand, int offer)
        {
            Demand = demand;
            Offer = offer;
        }

        public int Demand { get; }

        public int Offer { get; }

        public bool Equals(TradeRate other)
            => Demand == other.Demand && Offer == other.Offer;

        public override bool Equals(object obj)
            => obj is TradeRate tradeRate && Equals(tradeRate);

        public override int GetHashCode()
            => new { Demand, Offer }.GetHashCode();
    }
}