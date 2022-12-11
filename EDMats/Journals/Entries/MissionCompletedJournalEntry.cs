using EDMats.Models.Materials;
using System;
using System.Collections.Generic;

namespace EDMats.Journals.Entries
{
    public record MissionCompletedJournalEntry(DateTime Timestamp, IReadOnlyCollection<MaterialQuantity> MaterialsReward) : JournalEntry(Timestamp)
    {
        public override void Accept(JournalEntryVisitor visitor)
            => visitor.Visit(this);
    }
}