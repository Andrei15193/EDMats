using System;
using System.Collections.Generic;
using System.Linq;
using EDMats.Models.Materials;
using EDMats.Trading;
using EDMats.Trading.Implementations;
using Moq;
using Xunit;

namespace EDMats.Tests.Trading
{
    public class TradeSolutionServiceTests
    {
        private readonly Mock<IMaterialTraderService> _materialTraderService;
        private readonly TradeSolutionService _tradeSolutionService;

        public TradeSolutionServiceTests()
        {
            _materialTraderService = new Mock<IMaterialTraderService>();
            _tradeSolutionService = new TradeSolutionService(_materialTraderService.Object);
        }

        [Fact]
        public void Trade6For1()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Iron, 6) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 1),
                        new MaterialQuantity(Material.Iron, 6)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void Trade12For2()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 2) },
                new[] { new MaterialQuantity(Material.Iron, 12) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 2),
                        new MaterialQuantity(Material.Iron, 12)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void Trade6For1Having9Available()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Iron, 9) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 1),
                        new MaterialQuantity(Material.Iron, 6)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void Trade6For1HavingMoreDifferentMaterialsAvailable()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Manganese))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Iron, 9), new MaterialQuantity(Material.Germanium, 9), new MaterialQuantity(Material.Manganese, 9) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 1),
                        new MaterialQuantity(Material.Iron, 6)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void TradingPrioritizesLowerGradeSameCategoryOverDifferentCategory()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Germanium, 9), new MaterialQuantity(Material.Iron, 12) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 1),
                        new MaterialQuantity(Material.Iron, 6)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void TradingPrioritizesLowerGradeFromDifferentCategoryOverDowngradingFromSameCategory()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Nickel))
                .Returns(new TradeRate(1, 36));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Tin))
                .Returns(new TradeRate(3, 1));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 3) },
                new[] { new MaterialQuantity(Material.Tin, 6), new MaterialQuantity(Material.Nickel, 108) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 3),
                        new MaterialQuantity(Material.Nickel, 108)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void TradingPrioritizesSameGradeFromDifferentCategoryOverDowngradingFromSameCategory()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Tin))
                .Returns(new TradeRate(3, 1));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 3) },
                new[] { new MaterialQuantity(Material.Tin, 6), new MaterialQuantity(Material.Germanium, 18) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 3),
                        new MaterialQuantity(Material.Germanium, 18)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void TradingPrioritizesDowngradeFromSameCategoryOverDowngradeFromDifferentCategory()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Tungsten))
                .Returns(new TradeRate(2, 1));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Tin))
                .Returns(new TradeRate(3, 1));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 6) },
                new[] { new MaterialQuantity(Material.Tungsten, 3), new MaterialQuantity(Material.Tin, 2) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 6),
                        new MaterialQuantity(Material.Tin, 2)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void Trade2DifferentMaterialsForSameOne()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 2) },
                new[] { new MaterialQuantity(Material.Iron, 6), new MaterialQuantity(Material.Germanium, 6) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 1),
                        new MaterialQuantity(Material.Iron, 6)
                    ),
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 1),
                        new MaterialQuantity(Material.Germanium, 6)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void DowngradingMaterialsMakesAsFewTradesAsPossible()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Tin))
                .Returns(new TradeRate(3, 1));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Tin, 2) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 3),
                        new MaterialQuantity(Material.Tin, 1)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void TradingMaterialsConsidersAllowedTrades()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Germanium, 9), new MaterialQuantity(Material.Iron, 12) },
                new[] { new AllowedTrade(Material.Zinc, Material.Germanium) }
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 1),
                        new MaterialQuantity(Material.Germanium, 6)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void TradeSolutionIsNullWhenItCannotBeFound()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Germanium, 5), new MaterialQuantity(Material.Iron, 5) },
                AllowedTrade.All
            );

            Assert.Null(tradeSolution);
        }

        [Fact]
        public void TradeSolutionReturnsEmptyTradeEntriesWhenThereAreNoDesiredMaterials()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                Array.Empty<MaterialQuantity>(),
                new[] { new MaterialQuantity(Material.Germanium, 5), new MaterialQuantity(Material.Iron, 5) },
                AllowedTrade.All
            );

            Assert.False(tradeSolution.Trades.Any());
        }

        [Fact]
        public void TradeSolutionIsNullWhenThereAreNoAvailableMaterials()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                Array.Empty<MaterialQuantity>(),
                AllowedTrade.All
            );

            Assert.Null(tradeSolution);
        }

        [Fact]
        public void TradeSolutionIsNullWhenThereAreNoAlloedTrades()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Germanium, 5), new MaterialQuantity(Material.Iron, 5) },
                Array.Empty<AllowedTrade>()
            );

            Assert.Null(tradeSolution);
        }

        [Fact]
        public void TradeSolutionHasEmptyTradeListIfTheDesiredMaterialsAreAlreadyAvailable()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Germanium, 9), new MaterialQuantity(Material.Iron, 12), new MaterialQuantity(Material.Zinc, 1) },
                AllowedTrade.All
            );

            Assert.False(tradeSolution.Trades.Any());
        }

        [Fact]
        public void TradeSolutionDoesNotTradeDesiredMaterials()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1), new MaterialQuantity(Material.Iron, 6) },
                new[] { new MaterialQuantity(Material.Germanium, 9), new MaterialQuantity(Material.Iron, 11) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 1),
                        new MaterialQuantity(Material.Germanium, 6)
                    )
                },
                tradeSolution.Trades
            );
        }

        [Fact]
        public void TradeSameMaterialForMultiple()
        {
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Germanium, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _materialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Manganese, Material.Iron))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = _tradeSolutionService.TryFindSolution(
                new[] { new MaterialQuantity(Material.Zinc, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.Manganese, 1) },
                new[] { new MaterialQuantity(Material.Iron, 18) },
                AllowedTrade.All
            );

            _AssertAreEqual(
                new[]
                {
                    new TradeEntry(
                        new MaterialQuantity(Material.Zinc, 1),
                        new MaterialQuantity(Material.Iron, 6)
                    ),
                    new TradeEntry(
                        new MaterialQuantity(Material.Germanium, 1),
                        new MaterialQuantity(Material.Iron, 6)
                    ),
                    new TradeEntry(
                        new MaterialQuantity(Material.Manganese, 1),
                        new MaterialQuantity(Material.Iron, 6)
                    )
                },
                tradeSolution.Trades
            );
        }

        private void _AssertAreEqual(IEnumerable<TradeEntry> expectedTradeEntries, IEnumerable<TradeEntry> actualTradeEntries)
        {
            var expectedEntries =
                expectedTradeEntries
                .Select(
                    tradeEntry => new
                    {
                        DemandMaterial = tradeEntry.Demand.Material,
                        DemandAmount = tradeEntry.Demand.Amount,
                        OfferMaterial = tradeEntry.Offer.Material,
                        OfferAmount = tradeEntry.Offer.Amount
                    }
                )
                .OrderBy(entry => entry.DemandMaterial.Name)
                .ThenBy(entry => entry.OfferMaterial.Name);
            var actualEntries =
                actualTradeEntries
                    .Select(
                        tradeEntry => new
                        {
                            DemandMaterial = tradeEntry.Demand.Material,
                            DemandAmount = tradeEntry.Demand.Amount,
                            OfferMaterial = tradeEntry.Offer.Material,
                            OfferAmount = tradeEntry.Offer.Amount
                        }
                    )
                    .OrderBy(entry => entry.DemandMaterial.Name)
                    .ThenBy(entry => entry.OfferMaterial.Name);

            Assert.True(
                expectedEntries.SequenceEqual(actualEntries),
                $"Expected <{string.Join(", ", expectedEntries)}>, actually <{string.Join(", ", actualEntries)}> received"
            );
        }
    }
}