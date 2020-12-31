using System;
using System.Windows.Media;
using EDMats.Converters;
using EDMats.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Converters
{
    [TestClass]
    public class TradeSolutionSearchStatusBrushConverterTests
    {
        [TestMethod]
        public void IdleStatus()
        {
            var expectedBrush = Brushes.Red;
            var converter = new TradeSolutionSearchStatusBrushConverter
            {
                IdleBrush = expectedBrush
            };

            var actualBrush = (Brush)converter.Convert(TradeSolutionSearchStatus.Idle, typeof(Brush), null, null);

            Assert.AreSame(expectedBrush, actualBrush);
        }

        [TestMethod]
        public void SearchingStatus()
        {
            var expectedBrush = Brushes.Red;
            var converter = new TradeSolutionSearchStatusBrushConverter
            {
                SearchingBrush = expectedBrush
            };

            var actualBrush = (Brush)converter.Convert(TradeSolutionSearchStatus.Searching, typeof(Brush), null, null);

            Assert.AreSame(expectedBrush, actualBrush);
        }

        [TestMethod]
        public void SuccessStatus()
        {
            var expectedBrush = Brushes.Red;
            var converter = new TradeSolutionSearchStatusBrushConverter
            {
                SearchSucceededBrush = expectedBrush
            };

            var actualBrush = (Brush)converter.Convert(TradeSolutionSearchStatus.SearchSucceeded, typeof(Brush), null, null);

            Assert.AreSame(expectedBrush, actualBrush);
        }

        [TestMethod]
        public void SearchFailedStatus()
        {
            var expectedBrush = Brushes.Red;
            var converter = new TradeSolutionSearchStatusBrushConverter
            {
                SearchFailedBrush = expectedBrush
            };

            var actualBrush = (Brush)converter.Convert(TradeSolutionSearchStatus.SearchFailed, typeof(Brush), null, null);

            Assert.AreSame(expectedBrush, actualBrush);
        }

        [TestMethod]
        public void ConverterReturnsNullWhenConvertingNull()
        {
            var converter = new TradeSolutionSearchStatusBrushConverter();

            var result = converter.Convert(null, typeof(Brush), null, null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertThrowsExceptionWhenTargetTypeIsNotABrush()
        {
            var converter = new TradeSolutionSearchStatusBrushConverter();
            var exception = Assert.ThrowsException<ArgumentException>(() => converter.Convert(null, typeof(string), null, null));
            Assert.AreEqual(new ArgumentException("Expected 'Brush' target type, 'String' provided.", "targetType").Message, exception.Message);
        }

        [TestMethod]
        public void ConvertingBackThrowsException()
        {
            var converter = new TradeSolutionSearchStatusBrushConverter();
            Assert.ThrowsException<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}