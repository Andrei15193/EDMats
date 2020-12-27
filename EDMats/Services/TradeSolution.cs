using System.Collections.Generic;
using System.Linq;

namespace EDMats.Services
{
    public class TradeSolution
    {
        public TradeSolution(IEnumerable<TradeEntry> trades)
        {
            Trades = trades as IReadOnlyList<TradeEntry> ?? trades.ToArray();
        }

        public IReadOnlyList<TradeEntry> Trades { get; }
    }
}