using System;
using System.Collections.Generic;
using EDMats.Data.Materials;

namespace EDMats.Data.JournalEntries
{
    public class MaterialsJournalEntry : JournalEntry
    {
        public MaterialsJournalEntry(DateTime timestamp, IReadOnlyCollection<MaterialQuantity> raw, IReadOnlyCollection<MaterialQuantity> manufactured, IReadOnlyCollection<MaterialQuantity> encoded)
            : base(timestamp)
        {
            Raw = raw;
            Manufactured = manufactured;
            Encoded = encoded;
        }

        public IReadOnlyCollection<MaterialQuantity> Raw { get; }

        public IReadOnlyCollection<MaterialQuantity> Manufactured { get; }

        public IReadOnlyCollection<MaterialQuantity> Encoded { get; }
    }
}