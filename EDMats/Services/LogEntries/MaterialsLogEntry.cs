using System.Collections.Generic;

namespace EDMats.Services.LogEntries
{
    public class MaterialsLogEntry : LogEntry
    {
        public IReadOnlyCollection<MaterialQuantity> Raw { get; set; }

        public IReadOnlyCollection<MaterialQuantity> Manufactured { get; set; }

        public IReadOnlyCollection<MaterialQuantity> Encoded { get; set; }
    }
}