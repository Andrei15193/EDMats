using System;
using System.Linq;
using EDMats.Models.Materials;

namespace EDMats.Journals.Entries
{
    public record MaterialCollectedJournalEntry(DateTime Timestamp, MaterialQuantity CollectedMaterialQuantity) : JournalEntry(Timestamp)
    {
        public override void Accept(JournalEntryVisitor visitor)
            => visitor.Visit(this);
    }
}