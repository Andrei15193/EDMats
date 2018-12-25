namespace EDMats.Services
{
    public interface IMaterialTraderService
    {
        TradeRate GetTradeRate(Material demand, Material offer);
    }
}