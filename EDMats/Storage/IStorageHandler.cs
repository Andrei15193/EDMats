using System.IO;

namespace EDMats.Storage
{
    public interface IStorageHandler
    {
        TextReader GetTextReader(string profileName);

        TextWriter GetTextWriter(string profileName);
    }
}