using System.IO;

namespace EDMats.Storage
{
    public interface IStorageHandler
    {
        TextReader OpenRead(string fileName);

        TextWriter OpenWrite(string fileName);
    }
}