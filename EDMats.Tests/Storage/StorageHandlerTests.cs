using EDMats.Storage;
using Xunit;

namespace EDMats.Tests.Storage
{
    public abstract class StorageHandlerTests
    {
        protected abstract IStorageHandler GetStorageHandler();

        [Fact]
        public void OpenRead_WhenNoDataHasBeenWritten_ReturnsEmptyContent()
        {
            var storageHandler = GetStorageHandler();

            using (var reader = storageHandler.OpenRead(nameof(OpenRead_WhenNoDataHasBeenWritten_ReturnsEmptyContent)))
                Assert.Empty(reader.ReadToEnd());
        }

        [Fact]
        public void OpenWrite_WhenDataHasBeenWritten_ReturnsSameContent()
        {
            var storageHandler = GetStorageHandler();

            using (var writer = storageHandler.OpenWrite(nameof(OpenWrite_WhenDataHasBeenWritten_ReturnsSameContent)))
                writer.Write("this is a test content");

            using (var reader = storageHandler.OpenRead(nameof(OpenWrite_WhenDataHasBeenWritten_ReturnsSameContent)))
                Assert.Equal("this is a test content", reader.ReadToEnd());
        }

        [Fact]
        public void OpenWrite_WhenDataHasBeenWritten_OverwritesExistingData()
        {
            var storageHandler = GetStorageHandler();
            using (var writer = storageHandler.OpenWrite(nameof(OpenWrite_WhenDataHasBeenWritten_OverwritesExistingData)))
                writer.Write("this is a test content");

            using (var writer = storageHandler.OpenWrite(nameof(OpenWrite_WhenDataHasBeenWritten_OverwritesExistingData)))
                writer.Write("updated content");

            using (var reader = storageHandler.OpenRead(nameof(OpenWrite_WhenDataHasBeenWritten_OverwritesExistingData)))
                Assert.Equal("updated content", reader.ReadToEnd());
        }
    }
}