using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EDMats.Storage
{
    public class InMemoryStorageHandler : IStorageHandler
    {
        private IDictionary<string, MemoryStream> _profiles = new Dictionary<string, MemoryStream>(StringComparer.OrdinalIgnoreCase);

        public TextReader GetTextReader(string profileName)
        {
            if (_profiles.TryGetValue(profileName, out var profileStream))
            {
                profileStream.Seek(0, SeekOrigin.Begin);
                return new StreamReader(profileStream, Encoding.UTF8, true, 1 << 11, true);
            }
            else
                return new StringReader(string.Empty);
        }

        public TextWriter GetTextWriter(string profileName)
        {
            if (!_profiles.TryGetValue(profileName, out var profileStream))
            {
                profileStream = new MemoryStream();
                _profiles.Add(profileName, profileStream);
            }
            else
            {
                profileStream.Seek(0, SeekOrigin.Begin);
                profileStream.SetLength(0);
            }
            return new StreamWriter(profileStream, Encoding.UTF8, 1 << 11, true);
        }
    }
}