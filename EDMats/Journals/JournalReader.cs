using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EDMats.Journals.Entries;
using EDMats.Models.Materials;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EDMats.Journals
{
    public class JournalReader
    {
        private readonly IReadOnlyDictionary<string, Func<JObject, JournalEntry>> _journalEntryFactories = new Dictionary<string, Func<JObject, JournalEntry>>(StringComparer.OrdinalIgnoreCase)
        {
            { "Materials", _TryGetMaterialsJournalEntry },
            { "MaterialCollected", _TryGetMaterialCollectedJournalEntry }
        };

        public CommanderInfo Read(TextReader textReader)
        {
            var commanderInfo = new CommanderInfo();

            var journalEntryJsonText = textReader.ReadLine();
            while (journalEntryJsonText is object)
            {
                var journalEntryJson = JsonConvert.DeserializeObject<JObject>(journalEntryJsonText);

                if (journalEntryJson is object && journalEntryJson.TryGetValue("event", StringComparison.Ordinal, out var eventToken) && _journalEntryFactories.TryGetValue(eventToken.Value<string>(), out var factory))
                    factory(journalEntryJson)?.Populate(commanderInfo);

                journalEntryJsonText = textReader.ReadLine();
            }

            return commanderInfo;
        }

        private static JournalEntry _TryGetMaterialsJournalEntry(JObject journalEntryJson)
        {
            return new MaterialsJournalEntry(
                _GetTimestampFrom(journalEntryJson),
                _GetMaterialQuantities("Raw"),
                _GetMaterialQuantities("Manufactured"),
                _GetMaterialQuantities("Encoded")
            );

            IReadOnlyCollection<MaterialQuantity> _GetMaterialQuantities(string propertyName)
            {
                journalEntryJson.TryGetValue(propertyName, StringComparison.OrdinalIgnoreCase, out var x);
                var y = x;

                var materialQuantities = journalEntryJson.TryGetValue(propertyName, StringComparison.OrdinalIgnoreCase, out var materialQuantitiesJson)
                    ? ((JArray)materialQuantitiesJson).OfType<JObject>().Select(_TryGetMaterialQuantityFrom).Where(materialQuantity => materialQuantity is object)
                    : Enumerable.Empty<MaterialQuantity>();

                return materialQuantities as IReadOnlyCollection<MaterialQuantity> ?? materialQuantities.ToArray();
            }
        }

        private static JournalEntry _TryGetMaterialCollectedJournalEntry(JObject journalEntryJson)
        {
            var materialQuantity = _TryGetMaterialQuantityFrom(journalEntryJson);
            if (materialQuantity is object)
                return new MaterialCollectedJournalEntry(_GetTimestampFrom(journalEntryJson), materialQuantity);
            else
                return null;
        }

        private static DateTime _GetTimestampFrom(JObject journalEntryJson)
            => journalEntryJson.GetValue("timestamp", StringComparison.OrdinalIgnoreCase).Value<DateTime>();

        private static MaterialQuantity _TryGetMaterialQuantityFrom(JObject materialQuantityJson)
        {
            if (materialQuantityJson.TryGetValue("Name", StringComparison.OrdinalIgnoreCase, out var nameToken)
                && materialQuantityJson.TryGetValue("Count", StringComparison.OrdinalIgnoreCase, out var countToken))
            {
                var materialQuantity = new MaterialQuantity(Material.FindById(nameToken.Value<string>()), countToken.Value<int>());

#warning Remove when all materials have been tested
                if (materialQuantityJson.TryGetValue("Name_Localised", StringComparison.OrdinalIgnoreCase, out var localisedName)
                    && materialQuantity.Material.Name != localisedName.Value<string>().Trim())
                    throw new InvalidDataException($"Expected '{materialQuantity.Material.Name}' material name, actual '{localisedName}' received.");

                return materialQuantity;
            }
            else
                return null;
        }
    }
}