using System;
using System.Collections.Generic;
using System.IO;

namespace EDMats.Storage.Implementations
{
    public class InMemoryStorageHandler : IStorageHandler
    {
        private readonly IDictionary<string, MemoryStream> _files = new Dictionary<string, MemoryStream>(StringComparer.OrdinalIgnoreCase);

        public TextReader OpenRead(string fileName)
        {
            if (_files.TryGetValue(fileName, out var memoryStream))
            {
                memoryStream.Seek(0, SeekOrigin.Begin);
                return new StreamReader(memoryStream, leaveOpen: true);
            }
            else
                return new StringReader(string.Empty);
        }

        public TextWriter OpenWrite(string fileName)
        {
            if (!_files.TryGetValue(fileName, out var memoryStream))
            {
                memoryStream = new MemoryStream();
                _files.Add(fileName, memoryStream);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            memoryStream.SetLength(0);
            return new StreamWriter(memoryStream, leaveOpen: true);
        }
    }
}