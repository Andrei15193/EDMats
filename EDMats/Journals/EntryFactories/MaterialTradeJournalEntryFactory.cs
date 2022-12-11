using EDMats.Journals.Entries;
using Newtonsoft.Json.Linq;
using System;

namespace EDMats.Journals.EntryFactories
{
    public class MaterialTradeJournalEntryFactory : IJournalEntryFactory<MaterialTradeJournalEntry>
    {
        public bool TryCreate(JObject journalEntryJson, out JournalEntry journalEntry)
        {
            if (string.Equals("MaterialTrade", JournalEntryFactoryUtils.GetEventFrom(journalEntryJson), StringComparison.OrdinalIgnoreCase))
            {
                var paid = JournalEntryFactoryUtils.TryGetMaterialQuantityFrom(journalEntryJson.Property("Paid", StringComparison.OrdinalIgnoreCase).Value<JObject>(), "Material");
                var received = JournalEntryFactoryUtils.TryGetMaterialQuantityFrom(journalEntryJson.Property("Received", StringComparison.OrdinalIgnoreCase).Value<JObject>(), "Material");

                journalEntry = new MaterialTradeJournalEntry(JournalEntryFactoryUtils.GetTimestampFrom(journalEntryJson), paid, received);
            }
            else
                journalEntry = null;

            return journalEntry is not null;
        }
    }
}