using EDMats.Journals.Entries;
using EDMats.Models.Materials;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDMats.Journals.EntryFactories
{
    public class MissionCompletedJournalEntryFactory : IJournalEntryFactory<MissionCompletedJournalEntry>
    {
        public bool TryCreate(JObject journalEntryJson, out JournalEntry journalEntry)
        {
            if (string.Equals("MissionCompleted", JournalEntryFactoryUtils.GetEventFrom(journalEntryJson), StringComparison.OrdinalIgnoreCase))
            {
                var materialsRewards =
                    journalEntryJson.TryGetValue("MaterialsReward", StringComparison.OrdinalIgnoreCase, out var materialsRewardJson)
                    ? ((JArray)materialsRewardJson)
                        .OfType<JObject>()
                        .Select(JournalEntryFactoryUtils.TryGetMaterialQuantityFrom)
                        .Where(materialQuantity => materialQuantity is not null)
                        .ToArray()
                    : Array.Empty<MaterialQuantity>();

                if (materialsRewards.Length > 0)
                    journalEntry = new MissionCompletedJournalEntry(JournalEntryFactoryUtils.GetTimestampFrom(journalEntryJson), materialsRewards);
                else
                    journalEntry = null;
            }
            else
                journalEntry = null;

            return journalEntry is not null;
        }
    }
}