using System.Collections.Generic;

namespace EDMats.Services.JournalEntries
{
    public class MaterialsJournalEntry : JournalEntry
    {
        public IReadOnlyCollection<MaterialQuantity> Raw { get; set; }

        public IReadOnlyCollection<MaterialQuantity> Manufactured { get; set; }

        public IReadOnlyCollection<MaterialQuantity> Encoded { get; set; }
    }
}