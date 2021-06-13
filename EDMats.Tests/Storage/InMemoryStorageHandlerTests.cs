using EDMats.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Storage
{
    [TestClass]
    public class InMemoryStorageHandlerTests
    {
        [TestMethod]
        public void GetTextReader_WhenProfileDoesNotExist_ReturnsEmptyReader()
        {
            var storageHandler = new InMemoryStorageHandler();

            using (var textReader = storageHandler.GetTextReader("profile"))
                Assert.AreEqual(string.Empty, textReader.ReadToEnd());
        }

        [TestMethod]
        public void GetTextWriter_WhenProfileDoesNotExist_SavesNewContent()
        {
            var storageHandler = new InMemoryStorageHandler();

            using (var textWriter = storageHandler.GetTextWriter("profile"))
                textWriter.Write("content");

            using (var textReader = storageHandler.GetTextReader("profile"))
                Assert.AreEqual("content", textReader.ReadToEnd());
        }

        [TestMethod]
        public void GetTextWriter_WhenProfileExists_OverwritesOldContent()
        {
            var storageHandler = new InMemoryStorageHandler();
            using (var textWriter = storageHandler.GetTextWriter("profile"))
                textWriter.Write("this is some old content");

            using (var textWriter = storageHandler.GetTextWriter("profile"))
                textWriter.Write("new content");

            using (var textReader = storageHandler.GetTextReader("profile"))
                Assert.AreEqual("new content", textReader.ReadToEnd());
        }
    }
}