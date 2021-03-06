﻿using System.Linq;
using EDMats.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class AllowedTradesTests
    {
        [TestMethod]
        public void AllAllowedTrades()
        {
            var expectedAllowedTrades =
                from materialsByType in new[]
                                        {
                                            Materials.Encoded.Categories.SelectMany(category => category.Materials),
                                            Materials.Manufactured.Categories.SelectMany(category => category.Materials),
                                            Materials.Raw.Categories.SelectMany(category => category.Materials)
                                        }
                from offeredEncodedMaterial in materialsByType
                from demandedEncodedMaterial in materialsByType
                where offeredEncodedMaterial != demandedEncodedMaterial
                select new AllowedTrade(demandedEncodedMaterial, offeredEncodedMaterial);

            var actualAllowedTrades = AllowedTrades.All;

            Assert.IsTrue(
                expectedAllowedTrades
                    .OrderBy(allowedTrade => allowedTrade.Offer.Name)
                    .ThenBy(allowedTrade => allowedTrade.Demand.Name)
                    .Select(allowedTrade => new { allowedTrade.Offer, allowedTrade.Demand })
                    .SequenceEqual(
                        actualAllowedTrades
                            .OrderBy(allowedTrade => allowedTrade.Offer.Name)
                            .ThenBy(allowedTrade => allowedTrade.Demand.Name)
                            .Select(allowedTrade => new { allowedTrade.Offer, allowedTrade.Demand })
                    )
            );
        }
    }
}