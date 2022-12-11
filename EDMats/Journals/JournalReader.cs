using System.Collections.Generic;
using System.IO;
using EDMats.Journals.Entries;
using EDMats.Journals.EntryFactories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EDMats.Journals
{
    public class JournalReader
    {
        private readonly IEnumerable<IJournalEntryFactory<JournalEntry>> _journalEntryFactories = new IJournalEntryFactory<JournalEntry>[]
        {
            new MaterialCollectedJournalEntryFactory(),
            new MaterialsJournalEntryFactory(),
            new MaterialTradeJournalEntryFactory(),
            new MissionCompletedJournalEntryFactory()
        };

        public IEnumerable<JournalEntry> Read(TextReader textReader)
        {
            var journalEntries = new List<JournalEntry>();

            string journalEntryJsonText;
            do
            {
                journalEntryJsonText = textReader.ReadLine();
                if (!string.IsNullOrWhiteSpace(journalEntryJsonText))
                {
                    var journalEntryJson = JsonConvert.DeserializeObject<JObject>(journalEntryJsonText);
                    if (journalEntryJson is not null && _TryCreateJournalEntry(journalEntryJson, out var journalEntry))
                        journalEntries.Add(journalEntry);
                }
            } while (journalEntryJsonText is not null);

            return journalEntries;
        }

        private bool _TryCreateJournalEntry(JObject journalEntryJson, out JournalEntry journalEntry)
        {
            using (var journalEntryFactory = _journalEntryFactories.GetEnumerator())
                do
                    journalEntry = null;
                while (journalEntryFactory.MoveNext() && !journalEntryFactory.Current.TryCreate(journalEntryJson, out journalEntry));

            return journalEntry is not null;
        }
    }
}