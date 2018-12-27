﻿using System.IO;
using System.Text;

namespace EDMats.Services.Implementations
{
    public class FileSystemService : IFileSystemService
    {
        public TextReader OpenRead(string fileName)
            => new StreamReader(fileName, Encoding.UTF8);
    }
}