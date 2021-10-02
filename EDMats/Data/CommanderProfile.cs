using System.Xml.Serialization;

namespace EDMats.Data
{
    [XmlRoot("commanderProfile")]
    public class CommanderProfile
    {
        [XmlAttribute("commanderName")]
        public string CommanderName { get; set; }

        [XmlAttribute("journalsDirectoryPath")]
        public string JournalsDirectoryPath { get; set; }
    }
}