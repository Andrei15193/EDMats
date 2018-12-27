using System;

namespace EDMats.Services.JournalEntries
{
    public abstract class JournalEntry
    {
        public DateTime Timestamp { get; set; }
    }
}