using System;

namespace EDMats.Journals.Entries
{
    public abstract class JournalEntry
    {
        public JournalEntry(DateTime timestamp)
            => Timestamp = timestamp;

        public DateTime Timestamp { get; }

        public abstract void Populate(CommanderInfo commanderInfo);
    }
}