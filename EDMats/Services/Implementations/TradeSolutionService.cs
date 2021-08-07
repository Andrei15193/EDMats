using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Data.Materials;
using EDMats.Data.MaterialTrading;

namespace EDMats.Services.Implementations
{
    public class TradeSolutionService : ITradeSolutionService
    {
        private readonly IMaterialTraderService _materialTraderService;

        public TradeSolutionService(IMaterialTraderService materialTraderService)
        {
            _materialTraderService = materialTraderService;
        }

        public Task<TradeSolution> TryFindSolutionAsync(IEnumerable<MaterialQuantity> desiredMaterialQuantities, IEnumerable<MaterialQuantity> availableMaterialQuantities, IEnumerable<AllowedTrade> allowedTrades)
            => TryFindSolutionAsync(desiredMaterialQuantities, availableMaterialQuantities, allowedTrades, CancellationToken.None);

        public async Task<TradeSolution> TryFindSolutionAsync(IEnumerable<MaterialQuantity> desiredMaterialQuantities, IEnumerable<MaterialQuantity> availableMaterialQuantities, IEnumerable<AllowedTrade> allowedTrades, CancellationToken cancellationToken)
        {
            var availableMaterialQuantitiesCollection = availableMaterialQuantities as IReadOnlyCollection<MaterialQuantity>
                ?? availableMaterialQuantities?.ToArray()
                ?? throw new ArgumentNullException(nameof(availableMaterialQuantities));
            var desiredMaterialQuantitiesCollection =
                desiredMaterialQuantities as IReadOnlyCollection<MaterialQuantity>
                ?? desiredMaterialQuantities?.ToArray()
                ?? throw new ArgumentNullException(nameof(desiredMaterialQuantities));
            var allowedTradesList = allowedTrades
                ?? throw new ArgumentNullException(nameof(allowedTrades));

            var desiredMaterialQuantitiesList = _SubtractFromMatchingWithPositiveAmounts(desiredMaterialQuantitiesCollection, availableMaterialQuantitiesCollection).ToArray();
            var availableMaterialQuantitiesList = _SubtractFromMatchingWithPositiveAmounts(availableMaterialQuantitiesCollection, desiredMaterialQuantitiesCollection).ToArray();

            if (desiredMaterialQuantitiesList.Length == 0)
                return new TradeSolution(Enumerable.Empty<TradeEntry>());
            else if (availableMaterialQuantitiesList.Length == 0)
                return null;

            await Task.Yield();
            cancellationToken.ThrowIfCancellationRequested();

            var desiredMaterialsData = _GetDesiredMaterialsData(availableMaterialQuantitiesList, desiredMaterialQuantitiesList, allowedTrades);
            if (desiredMaterialsData.Any(desiredMaterialData => desiredMaterialData.AvailableMaterialQuantities.Count == 0))
                return null;

            return await _TryFindSolutionAsync(desiredMaterialsData, cancellationToken).ConfigureAwait(false);
        }

        private async Task<TradeSolution> _TryFindSolutionAsync(IEnumerable<DesiredMaterialInfo> desiredMaterialsData, CancellationToken cancellationToken)
        {
            var desiredMaterialData = desiredMaterialsData.FirstOrDefault();
            if (desiredMaterialData == null)
                return new TradeSolution(Enumerable.Empty<TradeEntry>());

            IReadOnlyCollection<TradeEntry> tradeEntries = null;
            var searchStates = new Stack<SearchState>();
            try
            {
                searchStates.Push(new SearchState(desiredMaterialsData.Skip(1), _GetTradePossibilitiesFor(desiredMaterialData).GetEnumerator()));
                do
                {
                    var searchState = searchStates.Peek();
                    if (!searchState.TradePossibilities.MoveNext())
                        searchStates.Pop().TradePossibilities.Dispose();
                    else
                    {
                        var currentDesiredMaterialData = searchState.RemainingDesiredMaterialsData.FirstOrDefault();
                        if (currentDesiredMaterialData == null)
                            tradeEntries = searchStates.SelectMany(validSearchState => validSearchState.TradePossibilities.Current).ToArray();
                        else
                        {
                            await Task.Yield();
                            cancellationToken.ThrowIfCancellationRequested();

                            var currentTradePosibilities = _GetTradePossibilitiesFor(
                                _RemoveUsed(
                                    currentDesiredMaterialData,
                                    searchState.TradePossibilities.Current
                                )
                            );
                            searchStates.Push(new SearchState(searchState.RemainingDesiredMaterialsData.Skip(1), currentTradePosibilities.GetEnumerator()));
                        }
                    }
                } while (searchStates.Any() && tradeEntries == null);
            }
            finally
            {
                while (searchStates.Count > 0)
                    searchStates.Pop().TradePossibilities.Dispose();
            }

            if (tradeEntries == null)
                return null;
            return new TradeSolution(tradeEntries
                .OrderBy(trade => trade.Demand.Material.Type.Name)
                .ThenBy(trade => trade.Demand.Amount)
                .ThenBy(trade => trade.Demand.Material.Grade)
                .ThenBy(trade => trade.Demand.Material.Name)
            );
        }

        private DesiredMaterialInfo _RemoveUsed(DesiredMaterialInfo desiredMaterialData, IReadOnlyCollection<TradeEntry> tradeEntries)
            => new DesiredMaterialInfo(
                desiredMaterialData.DesiredMaterialQuantity,
                from availableMaterialQuantity in desiredMaterialData.AvailableMaterialQuantities
                join tradeEntry in tradeEntries
                    on availableMaterialQuantity.Material equals tradeEntry.Offer.Material
                    into matchedTradeEntries
                let remaningMaterialAmount = availableMaterialQuantity.Amount - matchedTradeEntries.Sum(tradeEntry => tradeEntry.Offer.Amount)
                where remaningMaterialAmount > 0
                select new MaterialQuantity(availableMaterialQuantity.Material, remaningMaterialAmount)
            );

        private IEnumerable<IReadOnlyCollection<TradeEntry>> _GetTradePossibilitiesFor(DesiredMaterialInfo desiredMaterialsData)
        {
            var tradeOptions = (
                from availableMaterialQuantity in desiredMaterialsData.AvailableMaterialQuantities
                let tradeRate = _materialTraderService.GetTradeRate(desiredMaterialsData.DesiredMaterialQuantity.Material, availableMaterialQuantity.Material)
                where tradeRate != TradeRate.Invalid
                let desiredAmount = desiredMaterialsData.DesiredMaterialQuantity.Amount
                let tradeTimes = Math.Min(
                    desiredAmount / tradeRate.Demand + (desiredAmount % tradeRate.Demand != 0 ? 1 : 0),
                    availableMaterialQuantity.Amount / tradeRate.Offer
                )
                where tradeTimes > 0
                select new
                {
                    TradeRate = tradeRate,
                    TradeTimes = tradeTimes,
                    AvailableQuantity = availableMaterialQuantity
                }
            )
            .ToList();

            while (tradeOptions.Count > 0)
            {
                var remaningDesiredAmount = desiredMaterialsData.DesiredMaterialQuantity.Amount;
                var tradeEntries = new List<TradeEntry>();
                using (var tradeOption = tradeOptions.GetEnumerator())
                    while (tradeOption.MoveNext() && remaningDesiredAmount > 0)
                    {
                        var tradeEntry = new TradeEntry(
                            new MaterialQuantity(
                                desiredMaterialsData.DesiredMaterialQuantity.Material,
                                tradeOption.Current.TradeRate.Demand * tradeOption.Current.TradeTimes
                            ),
                            new MaterialQuantity(
                                tradeOption.Current.AvailableQuantity.Material,
                                tradeOption.Current.TradeRate.Offer * tradeOption.Current.TradeTimes
                            )
                        );
                        remaningDesiredAmount -= tradeEntry.Demand.Amount;
                        tradeEntries.Add(tradeEntry);
                    }

                if (remaningDesiredAmount <= 0)
                {
                    yield return tradeEntries;
                    var tradeOption = tradeOptions[0];
                    if (tradeOption.TradeTimes == 1)
                        tradeOptions.RemoveAt(0);
                    else
                        tradeOptions[0] = new
                        {
                            tradeOption.TradeRate,
                            TradeTimes = tradeOption.TradeTimes - 1,
                            tradeOption.AvailableQuantity
                        };
                }
                else
                    tradeOptions.Clear();
            }
        }

        private IReadOnlyCollection<DesiredMaterialInfo> _GetDesiredMaterialsData(IReadOnlyCollection<MaterialQuantity> availableMaterialQuantities, IReadOnlyCollection<MaterialQuantity> desiredMaterialsQuantities, IEnumerable<AllowedTrade> allowedTrades)
            => (
                from desiredMaterialQuantity in desiredMaterialsQuantities
                join allowedTrade in allowedTrades on desiredMaterialQuantity.Material equals allowedTrade.Demand into desiredMaterialAllowedTrades
                let tradableMaterialQuantities = from desiredMaterialAllowedTrade in desiredMaterialAllowedTrades
                                                 join availableMaterialQuantity in availableMaterialQuantities
                                                     on desiredMaterialAllowedTrade.Offer equals availableMaterialQuantity.Material
                                                 let tradeRate = _materialTraderService.GetTradeRate(desiredMaterialQuantity.Material, availableMaterialQuantity.Material)
                                                 where tradeRate != TradeRate.Invalid
                                                 orderby desiredMaterialQuantity.Material.Category == availableMaterialQuantity.Material.Category
                                                     ? desiredMaterialQuantity.Material.Grade > availableMaterialQuantity.Material.Grade
                                                         ? 0
                                                         : 2
                                                     : desiredMaterialQuantity.Material.Grade >= availableMaterialQuantity.Material.Grade
                                                         ? 1
                                                         : 3
                                                 select availableMaterialQuantity
                select new DesiredMaterialInfo(desiredMaterialQuantity, tradableMaterialQuantities)
            )
            .ToArray();

        private static IEnumerable<MaterialQuantity> _SubtractFromMatchingWithPositiveAmounts(IReadOnlyCollection<MaterialQuantity> materialQuantities, IReadOnlyCollection<MaterialQuantity> materialQuantitiesToSubtract)
            => from materialQuantity in materialQuantities
               join materialQuantityToSubtract in materialQuantitiesToSubtract
                   on materialQuantity.Material equals materialQuantityToSubtract.Material
                   into match
               select match.Any()
                   ? new MaterialQuantity(materialQuantity.Material, materialQuantity.Amount - match.Sum(material => material.Amount))
                   : materialQuantity into materialQuantity
               where materialQuantity.Amount > 0
               select materialQuantity;

        private sealed class SearchState
        {
            public SearchState(IEnumerable<DesiredMaterialInfo> remainingDesiredMaterialsData, IEnumerator<IReadOnlyCollection<TradeEntry>> tradePossibilities)
            {
                RemainingDesiredMaterialsData = remainingDesiredMaterialsData;
                TradePossibilities = tradePossibilities;
            }

            public IEnumerable<DesiredMaterialInfo> RemainingDesiredMaterialsData { get; }

            public IEnumerator<IReadOnlyCollection<TradeEntry>> TradePossibilities { get; }
        }

        private sealed class DesiredMaterialInfo
        {
            public DesiredMaterialInfo(MaterialQuantity desiredMaterialQuantity, IEnumerable<MaterialQuantity> availableMaterialQuantities)
            {
                DesiredMaterialQuantity = desiredMaterialQuantity;
                AvailableMaterialQuantities = availableMaterialQuantities as IReadOnlyCollection<MaterialQuantity> ?? availableMaterialQuantities.ToArray();
            }

            public MaterialQuantity DesiredMaterialQuantity { get; }

            public IReadOnlyCollection<MaterialQuantity> AvailableMaterialQuantities { get; }
        }
    }
}