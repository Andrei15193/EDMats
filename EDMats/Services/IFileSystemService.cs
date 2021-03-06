﻿using System.IO;

namespace EDMats.Services
{
    public interface IFileSystemService
    {
        TextReader OpenRead(string fileName);

        TextWriter OpenWrite(string fileName);
    }
}