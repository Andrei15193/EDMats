using EDMats.Models.Materials;
using System;

namespace EDMats.Journals.Entries
{
    public record MaterialTradeJournalEntry(DateTime Timestamp, MaterialQuantity Paid, MaterialQuantity Received) : JournalEntry(Timestamp)
    {
        public override void Accept(JournalEntryVisitor visitor)
            => visitor.Visit(this);
    }
}