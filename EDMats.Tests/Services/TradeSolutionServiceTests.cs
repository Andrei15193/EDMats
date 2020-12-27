using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDMats.Data.Materials;
using EDMats.Services;
using EDMats.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class TradeSolutionServiceTests
    {
        private Mock<IMaterialTraderService> _MaterialTraderService { get; set; }

        private TradeSolutionService _TradeSolutionService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            _MaterialTraderService = new Mock<IMaterialTraderService>();
            _TradeSolutionService = new TradeSolutionService(_MaterialTraderService.Object);
        }

        [TestMethod]
        public async Task Trade6For1()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Iron, 6) },
                AllowedTrades.All
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

        [TestMethod]
        public async Task Trade12For2()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 2) },
                new[] { new MaterialQuantity(Material.Iron, 12) },
                AllowedTrades.All
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

        [TestMethod]
        public async Task Trade6For1Having9Available()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Iron, 9) },
                AllowedTrades.All
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

        [TestMethod]
        public async Task Trade6For1HavingMoreDifferentMaterialsAvailable()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Manganese))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Iron, 9), new MaterialQuantity(Material.Germanium, 9), new MaterialQuantity(Material.Manganese, 9) },
                AllowedTrades.All
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

        [TestMethod]
        public async Task TradingPrioritizesLowerGradeSameCategoryOverDifferentCategory()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Germanium, 9), new MaterialQuantity(Material.Iron, 12) },
                AllowedTrades.All
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

        [TestMethod]
        public async Task TradingPrioritizesLowerGradeFromDifferentCategoryOverDowngradingFromSameCategory()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Nickel))
                .Returns(new TradeRate(1, 36));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Tin))
                .Returns(new TradeRate(3, 1));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 3) },
                new[] { new MaterialQuantity(Material.Tin, 6), new MaterialQuantity(Material.Nickel, 108) },
                AllowedTrades.All
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

        [TestMethod]
        public async Task TradingPrioritizesSameGradeFromDifferentCategoryOverDowngradingFromSameCategory()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Tin))
                .Returns(new TradeRate(3, 1));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 3) },
                new[] { new MaterialQuantity(Material.Tin, 6), new MaterialQuantity(Material.Germanium, 18) },
                AllowedTrades.All
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

        [TestMethod]
        public async Task TradingPrioritizesDowngradeFromSameCategoryOverDowngradeFromDifferentCategory()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Tungsten))
                .Returns(new TradeRate(2, 1));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Tin))
                .Returns(new TradeRate(3, 1));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 6) },
                new[] { new MaterialQuantity(Material.Tungsten, 3), new MaterialQuantity(Material.Tin, 2) },
                AllowedTrades.All
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

        [TestMethod]
        public async Task Trade2DifferentMaterialsForSameOne()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 2) },
                new[] { new MaterialQuantity(Material.Iron, 6), new MaterialQuantity(Material.Germanium, 6) },
                AllowedTrades.All
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

        [TestMethod]
        public async Task DowngradingMaterialsMakesAsFewTradesAsPossible()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Tin))
                .Returns(new TradeRate(3, 1));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Tin, 2) },
                AllowedTrades.All
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

        [TestMethod]
        public async Task TradingMaterialsConsidersAllowedTrades()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
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

        [TestMethod]
        public async Task TradeSolutionIsNullWhenItCannotBeFound()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Germanium, 5), new MaterialQuantity(Material.Iron, 5) },
                AllowedTrades.All
            );

            Assert.IsNull(tradeSolution);
        }

        [TestMethod]
        public async Task TradeSolutionReturnsEmptyTradeEntriesWhenThereAreNoDesiredMaterials()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                Enumerable.Empty<MaterialQuantity>(),
                new[] { new MaterialQuantity(Material.Germanium, 5), new MaterialQuantity(Material.Iron, 5) },
                AllowedTrades.All
            );

            Assert.IsFalse(tradeSolution.Trades.Any());
        }

        [TestMethod]
        public async Task TradeSolutionIsNullWhenThereAreNoAvailableMaterials()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                Enumerable.Empty<MaterialQuantity>(),
                AllowedTrades.All
            );

            Assert.IsNull(tradeSolution);
        }

        [TestMethod]
        public async Task TradeSolutionIsNullWhenThereAreNoAlloedTrades()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Germanium, 5), new MaterialQuantity(Material.Iron, 5) },
                Enumerable.Empty<AllowedTrade>()
            );

            Assert.IsNull(tradeSolution);
        }

        [TestMethod]
        public async Task TradeSolutionHasEmptyTradeListIfTheDesiredMaterialsAreAlreadyAvailable()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 1) },
                new[] { new MaterialQuantity(Material.Germanium, 9), new MaterialQuantity(Material.Iron, 12), new MaterialQuantity(Material.Zinc, 1) },
                AllowedTrades.All
            );

            Assert.IsFalse(tradeSolution.Trades.Any());
        }

        [TestMethod]
        public async Task TradeSolutionDoesNotTradeDesiredMaterials()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Germanium))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 1), new MaterialQuantity(Material.Iron, 6) },
                new[] { new MaterialQuantity(Material.Germanium, 9), new MaterialQuantity(Material.Iron, 11) },
                AllowedTrades.All
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

        [TestMethod]
        public async Task TradeSameMaterialForMultiple()
        {
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Zinc, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Germanium, Material.Iron))
                .Returns(new TradeRate(1, 6));
            _MaterialTraderService
                .Setup(materialTraderService => materialTraderService.GetTradeRate(Material.Manganese, Material.Iron))
                .Returns(new TradeRate(1, 6));

            var tradeSolution = await _TradeSolutionService.TryFindSolutionAsync(
                new[] { new MaterialQuantity(Material.Zinc, 1), new MaterialQuantity(Material.Germanium, 1), new MaterialQuantity(Material.Manganese, 1) },
                new[] { new MaterialQuantity(Material.Iron, 18) },
                AllowedTrades.All
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

            Assert.IsTrue(
                expectedEntries.SequenceEqual(actualEntries),
                $"Expected <{string.Join(", ", expectedEntries)}>, actually <{string.Join(", ", actualEntries)}> received"
            );
        }
    }
}