using System;
using EDMats.Data.Materials;

namespace EDMats.Data.JournalEntries
{
    public class MaterialCollectedJournalEntry : JournalEntry
    {
        public MaterialCollectedJournalEntry(DateTime timestamp, MaterialQuantity materialQuantity)
            : base(timestamp)
        {
            MaterialQuantity = materialQuantity;
        }

        public MaterialQuantity MaterialQuantity { get; }
    }
}