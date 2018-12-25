using EDMats.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class TradeRateTests
    {
        [TestMethod]
        public void EqualityComparisonUsingEquatableWorks()
        {
            var first = new TradeRate(20, 50);
            var second = new TradeRate(20, 50);

            Assert.IsTrue(first.Equals(second));
        }

        [TestMethod]
        public void InequalityComparisonUsingEquatableWorks()
        {
            var first = new TradeRate(20, 50);
            var third = new TradeRate(50, 20);

            Assert.IsFalse(first.Equals(third));
        }

        [TestMethod]
        public void EqualityComparisonUsingObjectEqualsWorks()
        {
            var first = new TradeRate(20, 50);
            var second = new TradeRate(20, 50);

            Assert.IsTrue(first.Equals((object)second));
        }

        [TestMethod]
        public void InequalityComparisonUsingObjectEqualsWorks()
        {
            var first = new TradeRate(20, 50);
            var third = new TradeRate(50, 20);

            Assert.IsFalse(first.Equals((object)third));
        }

        [TestMethod]
        public void EqualityComparisonUsingOperatorlsWorks()
        {
            var first = new TradeRate(20, 50);
            var second = new TradeRate(20, 50);

            Assert.IsTrue(first == second);
        }

        [TestMethod]
        public void InequalityComparisonUsingOperatorWorks()
        {
            var first = new TradeRate(20, 50);
            var third = new TradeRate(50, 20);

            Assert.IsTrue(first != third);
        }

        [TestMethod]
        public void GetHashCodeReturnsSameValueForEqualTradeRates()
        {
            var first = new TradeRate(20, 50);
            var second = new TradeRate(20, 50);

            Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
        }
    }
}