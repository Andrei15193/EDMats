using System;
using System.IO;
using System.Xml.Serialization;

namespace EDMats.Data
{
    [XmlRoot("commanderProfile")]
    public class CommanderProfile
    {
        [XmlAttribute("commanderName")]
        public string CommanderName { get; set; } = "Anonymous";

        [XmlAttribute("journalsDirectoryPath")]
        public string JournalsDirectoryPath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Saved Games", "Frontier Developments", "Elite Dangerous");
    }
}