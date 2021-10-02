using EDMats.Models.Materials;

namespace EDMats.Trading
{
    public interface IMaterialTraderService
    {
        TradeRate GetTradeRate(int times, Material demand, Material offer);
        TradeRate GetTradeRate(Material demand, Material offer);
    }
}