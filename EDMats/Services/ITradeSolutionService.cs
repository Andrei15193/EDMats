﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EDMats.Services
{
    public interface ITradeSolutionService
    {
        Task<TradeSolution> TryFindSolutionAsync(IEnumerable<MaterialQuantity> desiredMaterials, IEnumerable<MaterialQuantity> availableMaterialQuantities, IEnumerable<AllowedTrade> allowedTrades);

        Task<TradeSolution> TryFindSolutionAsync(IEnumerable<MaterialQuantity> desiredMaterials, IEnumerable<MaterialQuantity> availableMaterialQuantities, IEnumerable<AllowedTrade> allowedTrades, CancellationToken cancellationToken);
    }
}