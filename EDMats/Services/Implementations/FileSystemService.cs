using System.IO;
using System.Text;

namespace EDMats.Services.Implementations
{
    public class FileSystemService : IFileSystemService
    {
        public TextReader OpenRead(string fileName)
            => new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), Encoding.UTF8, false, 2048, false);

        public TextWriter OpenWrite(string fileName)
            => new StreamWriter(fileName, false, Encoding.UTF8);
    }
}