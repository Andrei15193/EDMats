using System;
using EDMats.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Converters
{
    [TestClass]
    public class StringConverterTests
    {
        private readonly StringConverter _stringConverter = new StringConverter();

        [TestMethod]
        public void ConvertStringFromObjectWorks()
        {
            var stringValue = Guid.NewGuid().ToString();
            var actualValue = _stringConverter.Convert(stringValue, typeof(string), null, null);

            Assert.AreSame(stringValue, actualValue);
        }

        [TestMethod]
        public void ConvertingToTargetTypeOtherThanStringThrowsException()
        {
            var exception = Assert.ThrowsException<ArgumentException>(() => _stringConverter.Convert(null, typeof(object), null, null));
            Assert.AreEqual(new ArgumentException("Expected 'String' target type, 'Object' provided.", "targetType").Message, exception.Message);
        }

        [TestMethod]
        public void ConvertingNonStringValueThrowsException()
        {
            Assert.ThrowsException<InvalidCastException>(() => _stringConverter.Convert(new object(), typeof(string), null, null));
        }

        [TestMethod]
        public void ConvertingBackThrowsException()
        {
            Assert.ThrowsException<NotImplementedException>(() => _stringConverter.ConvertBack(null, null, null, null));
        }
    }
}