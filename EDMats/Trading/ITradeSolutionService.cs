using System.Collections.Generic;
using EDMats.Models.Materials;

namespace EDMats.Trading
{
    public interface ITradeSolutionService
    {
        TradeSolution TryFindSolution(IEnumerable<MaterialQuantity> desiredMaterialQuantities, IEnumerable<MaterialQuantity> availableMaterialQuantities, IEnumerable<AllowedTrade> allowedTrades);
    }
}