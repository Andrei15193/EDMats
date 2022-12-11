using System;

namespace EDMats.Journals.Entries
{
    public abstract record JournalEntry(DateTime Timestamp)
    {
        public abstract void Accept(JournalEntryVisitor visitor);
    }
}