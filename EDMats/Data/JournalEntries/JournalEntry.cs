using System;

namespace EDMats.Data.JournalEntries
{
    public abstract class JournalEntry
    {
        public JournalEntry(DateTime timestamp)
            => Timestamp = timestamp;

        public DateTime Timestamp { get; }
    }
}