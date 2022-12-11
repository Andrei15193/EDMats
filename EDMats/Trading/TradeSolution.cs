using System;
using System.Collections.Generic;
using System.Linq;

namespace EDMats.Trading
{
    public class TradeSolution
    {
        public TradeSolution()
            : this(Array.Empty<TradeEntry>())
        {
        }

        public TradeSolution(IEnumerable<TradeEntry> trades)
            => Trades = trades as IReadOnlyList<TradeEntry> ?? trades.ToArray();

        public IReadOnlyList<TradeEntry> Trades { get; }
    }
}