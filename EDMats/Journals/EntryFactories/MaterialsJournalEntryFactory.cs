using EDMats.Journals.Entries;
using EDMats.Models.Materials;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDMats.Journals.EntryFactories
{
    public class MaterialsJournalEntryFactory : IJournalEntryFactory<MaterialsJournalEntry>
    {
        public bool TryCreate(JObject journalEntryJson, out JournalEntry journalEntry)
        {
            if (string.Equals("Materials", JournalEntryFactoryUtils.GetEventFrom(journalEntryJson), StringComparison.OrdinalIgnoreCase))
            {
                journalEntry = new MaterialsJournalEntry(
                    JournalEntryFactoryUtils.GetTimestampFrom(journalEntryJson),
                    _GetMaterialQuantities("Raw"),
                    _GetMaterialQuantities("Manufactured"),
                    _GetMaterialQuantities("Encoded")
                );

                IReadOnlyCollection<MaterialQuantity> _GetMaterialQuantities(string propertyName)
                {
                    var materialQuantities = journalEntryJson.TryGetValue(propertyName, StringComparison.OrdinalIgnoreCase, out var materialQuantitiesJson)
                        ? ((JArray)materialQuantitiesJson)
                            .OfType<JObject>()
                            .Select(JournalEntryFactoryUtils.TryGetMaterialQuantityFrom)
                            .Where(materialQuantity => materialQuantity is not null)
                        : Array.Empty<MaterialQuantity>();

                    return materialQuantities as IReadOnlyCollection<MaterialQuantity> ?? materialQuantities.ToArray();
                }
            }
            else
                journalEntry = null;

            return journalEntry is not null;
        }
    }
}