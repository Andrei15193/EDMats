using EDMats.Data.Materials;

namespace EDMats.Services
{
    public interface IMaterialTraderService
    {
        TradeRate GetTradeRate(Material demand, Material offer);

        TradeRate GetTradeRate(int times, Material demand, Material offer);
    }
}