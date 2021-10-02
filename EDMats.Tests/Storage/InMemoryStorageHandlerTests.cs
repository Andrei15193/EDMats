using EDMats.Storage;
using EDMats.Storage.Implementations;

namespace EDMats.Tests.Storage
{
    public class InMemoryStorageHandlerTests : StorageHandlerTests
    {
        protected override IStorageHandler GetStorageHandler()
            => new InMemoryStorageHandler();
    }
}