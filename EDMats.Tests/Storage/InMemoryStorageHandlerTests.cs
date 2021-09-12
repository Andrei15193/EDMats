using EDMats.Storage;

namespace EDMats.Tests.Storage
{
    public class InMemoryStorageHandlerTests : StorageHandlerTests
    {
        protected override IStorageHandler GetStorageHandler()
            => new InMemoryStorageHandler();
    }
}