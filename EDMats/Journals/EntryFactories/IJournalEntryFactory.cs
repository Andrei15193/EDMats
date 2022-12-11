using EDMats.Journals.Entries;
using Newtonsoft.Json.Linq;

namespace EDMats.Journals.EntryFactories
{
    public interface IJournalEntryFactory<out TJournalEntry>
        where TJournalEntry : JournalEntry
    {
        bool TryCreate(JObject journalEntryJson, out JournalEntry journalEntry);
    }
}