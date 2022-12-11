using System;
using System.Collections.Generic;
using System.Linq;
using EDMats.Models.Materials;

namespace EDMats.Journals.Entries
{
    public record MaterialsJournalEntry(DateTime Timestamp, IReadOnlyCollection<MaterialQuantity> Raw, IReadOnlyCollection<MaterialQuantity> Manufactured, IReadOnlyCollection<MaterialQuantity> Encoded)
        : JournalEntry(Timestamp)
    {
        public override void Accept(JournalEntryVisitor visitor)
            => visitor.Visit(this);
    }
}