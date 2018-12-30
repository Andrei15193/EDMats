using EDMats.Services;

namespace EDMats.ActionsData
{
    public class TradeSolutionSearchCompletedActionData : NotificationActionData
    {
        public TradeSolutionSearchCompletedActionData(TradeSolution tradeSolution)
        {
            TradeSolution = tradeSolution;
        }

        public TradeSolution TradeSolution { get; }
    }
}