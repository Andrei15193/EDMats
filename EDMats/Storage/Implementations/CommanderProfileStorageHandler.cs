using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using EDMats.Data;

namespace EDMats.Storage.Implementations
{
    public class CommanderProfileStorageHandler : ICommanderProfileStorageHandler
    {
        private const string CommanderProfileFileName = "commander-profile.xml";
        private static readonly XmlSerializer _xmlSerializer = new XmlSerializer(typeof(CommanderProfile));
        private readonly IStorageHandler _storageHandler;

        public CommanderProfileStorageHandler(IStorageHandler storageHandler)
            => _storageHandler = storageHandler;

        public CommanderProfile Load()
        {
            using (var reader = _storageHandler.OpenRead(CommanderProfileFileName))
            {
                var content = reader.ReadToEnd();
                if (string.IsNullOrWhiteSpace(content))
                    return new CommanderProfile
                    {
                        CommanderName = "Anonymous",
                        JournalsDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Saved Games", "Frontier Developments", "Elite Dangerous")
                    };
                else
                    using (var stringReader = new StringReader(content))
                        return (CommanderProfile)_xmlSerializer.Deserialize(XmlReader.Create(stringReader));
            }
        }

        public void Save(CommanderProfile commanderProfile)
        {
            using (var writer = _storageHandler.OpenWrite(CommanderProfileFileName))
                _xmlSerializer.Serialize(XmlWriter.Create(writer), commanderProfile);
        }
    }
}