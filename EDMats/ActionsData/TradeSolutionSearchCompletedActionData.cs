using EDMats.Data.MaterialTrading;

namespace EDMats.ActionsData
{
    public class TradeSolutionSearchCompletedActionData
    {
        public TradeSolutionSearchCompletedActionData(TradeSolution tradeSolution)
        {
            TradeSolution = tradeSolution;
        }

        public TradeSolution TradeSolution { get; }
    }
}