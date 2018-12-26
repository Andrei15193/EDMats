using System;
using EDMats.Services;
using EDMats.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class MaterialTraderServiceTests
    {
        private readonly MaterialTraderService _materialTraderService = new MaterialTraderService();

        [TestMethod]
        public void TradeGrade1InSameCategory()
        {
            _AssertTradeRate(TradeRate.Invalid, Materials.CrystalShards, Materials.CrystalShards);
            _AssertTradeRate(new TradeRate(1, 6), Materials.FlawedFocusCrystals, Materials.CrystalShards);
            _AssertTradeRate(new TradeRate(1, 36), Materials.FocusCrystals, Materials.CrystalShards);
            _AssertTradeRate(new TradeRate(1, 216), Materials.RefinedFocusCrystals, Materials.CrystalShards);
            _AssertTradeRate(TradeRate.Invalid, Materials.ExquisiteFocusCrystals, Materials.CrystalShards);
        }

        [TestMethod]
        public void TradeGrade2InSameCategory()
        {
            _AssertTradeRate(new TradeRate(3, 1), Materials.CrystalShards, Materials.FlawedFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Materials.FlawedFocusCrystals, Materials.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Materials.FocusCrystals, Materials.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 36), Materials.RefinedFocusCrystals, Materials.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 216), Materials.ExquisiteFocusCrystals, Materials.FlawedFocusCrystals);
        }

        [TestMethod]
        public void TradeGrade3InSameCategory()
        {
            _AssertTradeRate(new TradeRate(9, 1), Materials.CrystalShards, Materials.FocusCrystals);
            _AssertTradeRate(new TradeRate(3, 1), Materials.FlawedFocusCrystals, Materials.FocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Materials.FocusCrystals, Materials.FocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Materials.RefinedFocusCrystals, Materials.FocusCrystals);
            _AssertTradeRate(new TradeRate(1, 36), Materials.ExquisiteFocusCrystals, Materials.FocusCrystals);
        }

        [TestMethod]
        public void TradeGrade4InSameCategory()
        {
            _AssertTradeRate(new TradeRate(27, 1), Materials.CrystalShards, Materials.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(9, 1), Materials.FlawedFocusCrystals, Materials.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(3, 1), Materials.FocusCrystals, Materials.RefinedFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Materials.RefinedFocusCrystals, Materials.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Materials.ExquisiteFocusCrystals, Materials.RefinedFocusCrystals);
        }

        [TestMethod]
        public void TradeGrade5InSameCategory()
        {
            _AssertTradeRate(new TradeRate(81, 1), Materials.CrystalShards, Materials.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(27, 1), Materials.FlawedFocusCrystals, Materials.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(9, 1), Materials.FocusCrystals, Materials.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(3, 1), Materials.RefinedFocusCrystals, Materials.ExquisiteFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Materials.ExquisiteFocusCrystals, Materials.ExquisiteFocusCrystals);
        }

        [TestMethod]
        public void TradeGrade1CrossCategory()
        {
            _AssertTradeRate(new TradeRate(1, 6), Materials.SalvagedAlloys, Materials.CrystalShards);
            _AssertTradeRate(new TradeRate(1, 36), Materials.GalvanisingAlloys, Materials.CrystalShards);
            _AssertTradeRate(new TradeRate(1, 216), Materials.PhaseAlloys, Materials.CrystalShards);
            _AssertTradeRate(TradeRate.Invalid, Materials.ProtoLightAlloys, Materials.CrystalShards);
            _AssertTradeRate(TradeRate.Invalid, Materials.ProtoRadiolicAlloys, Materials.CrystalShards);
        }

        [TestMethod]
        public void TradeGrade2CrossCategory()
        {
            _AssertTradeRate(new TradeRate(1, 2), Materials.SalvagedAlloys, Materials.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Materials.GalvanisingAlloys, Materials.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 36), Materials.PhaseAlloys, Materials.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 216), Materials.ProtoLightAlloys, Materials.FlawedFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Materials.ProtoRadiolicAlloys, Materials.FlawedFocusCrystals);
        }

        [TestMethod]
        public void TradeGrade3CrossCategory()
        {
            _AssertTradeRate(new TradeRate(3, 2), Materials.SalvagedAlloys, Materials.FocusCrystals);
            _AssertTradeRate(new TradeRate(1, 2), Materials.GalvanisingAlloys, Materials.FocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Materials.PhaseAlloys, Materials.FocusCrystals);
            _AssertTradeRate(new TradeRate(1, 36), Materials.ProtoLightAlloys, Materials.FocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, Materials.ProtoRadiolicAlloys, Materials.FocusCrystals);
        }

        [TestMethod]
        public void TradeGrade4CrossCategory()
        {
            _AssertTradeRate(new TradeRate(9, 2), Materials.SalvagedAlloys, Materials.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(3, 2), Materials.GalvanisingAlloys, Materials.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 2), Materials.PhaseAlloys, Materials.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Materials.ProtoLightAlloys, Materials.RefinedFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 36), Materials.ProtoRadiolicAlloys, Materials.RefinedFocusCrystals);
        }

        [TestMethod]
        public void TradeGrade5CrossCategory()
        {
            _AssertTradeRate(new TradeRate(27, 2), Materials.SalvagedAlloys, Materials.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(9, 2), Materials.GalvanisingAlloys, Materials.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(3, 2), Materials.PhaseAlloys, Materials.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 2), Materials.ProtoLightAlloys, Materials.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(1, 6), Materials.ProtoRadiolicAlloys, Materials.ExquisiteFocusCrystals);
        }

        [TestMethod]
        public void GettingRateForMultipleTimes()
        {
            _AssertTradeRate(TradeRate.Invalid, 2, Materials.CrystalShards, Materials.CrystalShards);
            _AssertTradeRate(new TradeRate(2, 12), 2, Materials.FlawedFocusCrystals, Materials.CrystalShards);
            _AssertTradeRate(new TradeRate(2, 72), 2, Materials.FocusCrystals, Materials.CrystalShards);
            _AssertTradeRate(TradeRate.Invalid, 2, Materials.RefinedFocusCrystals, Materials.CrystalShards);
            _AssertTradeRate(TradeRate.Invalid, Materials.ExquisiteFocusCrystals, Materials.CrystalShards);
        }

        [TestMethod]
        public void TradingMultipleTimesForMaximumCapacityWorks()
        {
            _AssertTradeRate(new TradeRate(300, 100), 100, Materials.CrystalShards, Materials.FlawedFocusCrystals);
            _AssertTradeRate(new TradeRate(150, 50), 50, Materials.RefinedFocusCrystals, Materials.ExquisiteFocusCrystals);
            _AssertTradeRate(new TradeRate(50, 300), 50, Materials.FlawedFocusCrystals, Materials.CrystalShards);
        }

        [TestMethod]
        public void TradingMultipleTimesForAnAmountThatExceedMaximumCapacityReturnsInvalidTradeRate()
        {
            _AssertTradeRate(TradeRate.Invalid, 101, Materials.CrystalShards, Materials.FlawedFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, 51, Materials.RefinedFocusCrystals, Materials.ExquisiteFocusCrystals);
            _AssertTradeRate(TradeRate.Invalid, 51, Materials.FlawedFocusCrystals, Materials.CrystalShards);
        }

        [TestMethod]
        public void TradingCrossMaterialTypeReturnsInvalidTradeRate()
        {
            _AssertTradeRate(TradeRate.Invalid, Materials.Iron, Materials.GalvanisingAlloys);
        }

        [TestMethod]
        public void TradingNullDemandThrowsException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => _materialTraderService.GetTradeRate(null, Materials.Iron));
            Assert.AreEqual(new ArgumentNullException("demand").Message, exception.Message);
        }

        [TestMethod]
        public void TradingNullOfferThrowsException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => _materialTraderService.GetTradeRate(Materials.Iron, null));
            Assert.AreEqual(new ArgumentNullException("offer").Message, exception.Message);
        }

        [TestMethod]
        public void TradingZeroTimesThrowsException()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => _materialTraderService.GetTradeRate(0, Materials.Sulphur, Materials.Iron));
            Assert.AreEqual(new ArgumentException("Trade times must be greater than 0 (zero), '0' provided.", "times").Message, exception.Message);
        }

        [TestMethod]
        public void TradingNegativeTimesThrowsException()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => _materialTraderService.GetTradeRate(-20, Materials.Sulphur, Materials.Iron));
            Assert.AreEqual(new ArgumentException("Trade times must be greater than 0 (zero), '-20' provided.", "times").Message, exception.Message);
        }

        private void _AssertTradeRate(TradeRate expectedTradeRate, Material target, Material source)
        {
            var actualTradeRate = _materialTraderService.GetTradeRate(target, source);
            Assert.AreEqual(expectedTradeRate, actualTradeRate);
        }

        private void _AssertTradeRate(TradeRate expectedTradeRate, int times, Material target, Material source)
        {
            var actualTradeRate = _materialTraderService.GetTradeRate(times, target, source);
            Assert.AreEqual(expectedTradeRate, actualTradeRate);
        }
    }
}