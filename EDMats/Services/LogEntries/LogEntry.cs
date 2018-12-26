using System;

namespace EDMats.Services.LogEntries
{
    public abstract class LogEntry
    {
        public DateTime Timestamp { get; set; }
    }
}