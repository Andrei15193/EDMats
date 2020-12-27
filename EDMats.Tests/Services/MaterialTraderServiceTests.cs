using System;
using EDMats.Data.Materials;
using EDMats.Services;
using EDMats.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class MaterialTraderServiceTests
    {
        private MaterialTraderService _MaterialTraderService { get; } = new MaterialTraderService();

        [TestMethod]
        public void TradeGrade1InSameCategory()
        {
            _AssertTradeRate(TradeRate.Invalid, Material.CrystalShards, Material.CrystalShards);
            _AssertTradeRate(new TradeRate(1, 6), Material.FlawedFocusCrystals, Material.CrystalShards);
            _AssertTradeRate(new TradeRate(1, 36), Material.FocusCrystals, Material.CrystalShards);
            _AssertTradeRate(new TradeRate(1, 216), Material.RefinedFocusCrystals, Material.CrystalShards);
            _AssertTradeRate(TradeRate.Invalid, Material.ExquisiteFocusCrystals, Material.CrystalShards);
        }

        [TestMethod]
        public void TradeGrade2InSameCategory()
        {
            _AssertTradeRate(new TradeRate(3, 1), Material.CrystalShards, Material.FlawedFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Material.FlawedFocusCrystals, Material.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Material.FocusCrystals, Material.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 36), Material.RefinedFocusCrystals, Material.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 216), Material.ExquisiteFocusCrystals, Material.FlawedFocusCrystals);
        }

        [TestMethod]
        public void TradeGrade3InSameCategory()
        {
            _AssertTradeRate(new TradeRate(9, 1), Material.CrystalShards, Material.FocusCrystals);
            _AssertTradeRate(new TradeRate(3, 1), Material.FlawedFocusCrystals, Material.FocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Material.FocusCrystals, Material.FocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Material.RefinedFocusCrystals, Material.FocusCrystals);
            _AssertTradeRate(new TradeRate(1, 36), Material.ExquisiteFocusCrystals, Material.FocusCrystals);
        }

        [TestMethod]
        public void TradeGrade4InSameCategory()
        {
            _AssertTradeRate(new TradeRate(27, 1), Material.CrystalShards, Material.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(9, 1), Material.FlawedFocusCrystals, Material.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(3, 1), Material.FocusCrystals, Material.RefinedFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Material.RefinedFocusCrystals, Material.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Material.ExquisiteFocusCrystals, Material.RefinedFocusCrystals);
        }

        [TestMethod]
        public void TradeGrade5InSameCategory()
        {
            _AssertTradeRate(new TradeRate(81, 1), Material.CrystalShards, Material.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(27, 1), Material.FlawedFocusCrystals, Material.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(9, 1), Material.FocusCrystals, Material.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(3, 1), Material.RefinedFocusCrystals, Material.ExquisiteFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Material.ExquisiteFocusCrystals, Material.ExquisiteFocusCrystals);
        }

        [TestMethod]
        public void TradeGrade1CrossCategory()
        {
            _AssertTradeRate(new TradeRate(1, 6), Material.SalvagedAlloys, Material.CrystalShards);
            _AssertTradeRate(new TradeRate(1, 36), Material.GalvanisingAlloys, Material.CrystalShards);
            _AssertTradeRate(new TradeRate(1, 216), Material.PhaseAlloys, Material.CrystalShards);
            _AssertTradeRate(TradeRate.Invalid, Material.ProtoLightAlloys, Material.CrystalShards);
            _AssertTradeRate(TradeRate.Invalid, Material.ProtoRadiolicAlloys, Material.CrystalShards);
        }

        [TestMethod]
        public void TradeGrade2CrossCategory()
        {
            _AssertTradeRate(new TradeRate(1, 2), Material.SalvagedAlloys, Material.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Material.GalvanisingAlloys, Material.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 36), Material.PhaseAlloys, Material.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 216), Material.ProtoLightAlloys, Material.FlawedFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Material.ProtoRadiolicAlloys, Material.FlawedFocusCrystals);
        }

        [TestMethod]
        public void TradeGrade3CrossCategory()
        {
            _AssertTradeRate(new TradeRate(3, 2), Material.SalvagedAlloys, Material.FocusCrystals);
            _AssertTradeRate(new TradeRate(1, 2), Material.GalvanisingAlloys, Material.FocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Material.PhaseAlloys, Material.FocusCrystals);
            _AssertTradeRate(new TradeRate(1, 36), Material.ProtoLightAlloys, Material.FocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Material.ProtoRadiolicAlloys, Material.FocusCrystals);
        }

        [TestMethod]
        public void TradeGrade4CrossCategory()
        {
            _AssertTradeRate(new TradeRate(9, 2), Material.SalvagedAlloys, Material.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(3, 2), Material.GalvanisingAlloys, Material.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 2), Material.PhaseAlloys, Material.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Material.ProtoLightAlloys, Material.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 36), Material.ProtoRadiolicAlloys, Material.RefinedFocusCrystals);
        }

        [TestMethod]
        public void TradeGrade5CrossCategory()
        {
            _AssertTradeRate(new TradeRate(27, 2), Material.SalvagedAlloys, Material.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(9, 2), Material.GalvanisingAlloys, Material.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(3, 2), Material.PhaseAlloys, Material.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 2), Material.ProtoLightAlloys, Material.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Material.ProtoRadiolicAlloys, Material.ExquisiteFocusCrystals);
        }

        [TestMethod]
        public void GettingRateForMultipleTimes()
        {
            _AssertTradeRate(TradeRate.Invalid, 2, Material.CrystalShards, Material.CrystalShards);
            _AssertTradeRate(new TradeRate(2, 12), 2, Material.FlawedFocusCrystals, Material.CrystalShards);
            _AssertTradeRate(new TradeRate(2, 72), 2, Material.FocusCrystals, Material.CrystalShards);
            _AssertTradeRate(TradeRate.Invalid, 2, Material.RefinedFocusCrystals, Material.CrystalShards);
            _AssertTradeRate(TradeRate.Invalid, Material.ExquisiteFocusCrystals, Material.CrystalShards);
        }

        [TestMethod]
        public void TradingMultipleTimesForMaximumCapacityWorks()
        {
            _AssertTradeRate(new TradeRate(300, 100), 100, Material.CrystalShards, Material.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(150, 50), 50, Material.RefinedFocusCrystals, Material.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(50, 300), 50, Material.FlawedFocusCrystals, Material.CrystalShards);
        }

        [TestMethod]
        public void TradingMultipleTimesForAnAmountThatExceedMaximumCapacityReturnsInvalidTradeRate()
        {
            _AssertTradeRate(TradeRate.Invalid, 101, Material.CrystalShards, Material.FlawedFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, 51, Material.RefinedFocusCrystals, Material.ExquisiteFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, 51, Material.FlawedFocusCrystals, Material.CrystalShards);
        }

        [TestMethod]
        public void TradingCrossMaterialTypeReturnsInvalidTradeRate()
        {
            _AssertTradeRate(TradeRate.Invalid, Material.Iron, Material.GalvanisingAlloys);
        }

        [TestMethod]
        public void TradingNullDemandThrowsException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => _MaterialTraderService.GetTradeRate(null, Material.Iron));
            Assert.AreEqual(new ArgumentNullException("demand").Message, exception.Message);
        }

        [TestMethod]
        public void TradingNullOfferThrowsException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => _MaterialTraderService.GetTradeRate(Material.Iron, null));
            Assert.AreEqual(new ArgumentNullException("offer").Message, exception.Message);
        }

        [TestMethod]
        public void TradingZeroTimesThrowsException()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => _MaterialTraderService.GetTradeRate(0, Material.Sulphur, Material.Iron));
            Assert.AreEqual(new ArgumentException("Trade times must be greater than 0 (zero), '0' provided.", "times").Message, exception.Message);
        }

        [TestMethod]
        public void TradingNegativeTimesThrowsException()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => _MaterialTraderService.GetTradeRate(-20, Material.Sulphur, Material.Iron));
            Assert.AreEqual(new ArgumentException("Trade times must be greater than 0 (zero), '-20' provided.", "times").Message, exception.Message);
        }

        private void _AssertTradeRate(TradeRate expectedTradeRate, Material demand, Material offer)
        {
            var actualTradeRate = _MaterialTraderService.GetTradeRate(demand, offer);
            Assert.AreEqual(expectedTradeRate, actualTradeRate);
        }

        private void _AssertTradeRate(TradeRate expectedTradeRate, int times, Material demand, Material offer)
        {
            var actualTradeRate = _MaterialTraderService.GetTradeRate(times, demand, offer);
            Assert.AreEqual(expectedTradeRate, actualTradeRate);
        }
    }
}