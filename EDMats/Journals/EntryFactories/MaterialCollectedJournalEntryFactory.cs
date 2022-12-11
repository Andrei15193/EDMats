using EDMats.Journals.Entries;
using Newtonsoft.Json.Linq;
using System;

namespace EDMats.Journals.EntryFactories
{
    public class MaterialCollectedJournalEntryFactory : IJournalEntryFactory<MaterialCollectedJournalEntry>
    {
        public bool TryCreate(JObject journalEntryJson, out JournalEntry journalEntry)
        {
            if (string.Equals("MaterialCollected",  JournalEntryFactoryUtils.GetEventFrom(journalEntryJson), StringComparison.OrdinalIgnoreCase))
            {
                var materialQuantity = JournalEntryFactoryUtils.TryGetMaterialQuantityFrom(journalEntryJson);
                if (materialQuantity is not null)
                    journalEntry = new MaterialCollectedJournalEntry(JournalEntryFactoryUtils.GetTimestampFrom(journalEntryJson), materialQuantity);
                else
                    journalEntry = null;
            }
            else
                journalEntry = null;

            return journalEntry is not null;
        }
    }
}