using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Data.JournalEntries;
using EDMats.Data.Materials;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EDMats.Services.Implementations
{
    public class JournalReaderService : IJournalReaderService
    {
        private readonly IReadOnlyDictionary<string, Func<JObject, JournalEntry>> _journalEntryFactories = new Dictionary<string, Func<JObject, JournalEntry>>(StringComparer.OrdinalIgnoreCase)
        {
            { "Materials", _GetMaterialsJournalEntry },
            { "MaterialCollected", _GetMaterialCollectedJournalEntry }
        };

        public Task<IReadOnlyList<JournalEntry>> ReadAsync(TextReader textReader)
            => ReadAsync(textReader, CancellationToken.None);

        public async Task<IReadOnlyList<JournalEntry>> ReadAsync(TextReader textReader, CancellationToken cancellationToken)
        {
            var journalEntries = new List<JournalEntry>();

            var journalEntryJsonText = await textReader.ReadLineAsync().ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            while (journalEntryJsonText != null)
            {
                var journalEntryJson = await Task
                    .Run(() => JsonConvert.DeserializeObject<JObject>(journalEntryJsonText))
                    .ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();

                if (_journalEntryFactories.TryGetValue((string)journalEntryJson["event"], out var factory))
                    journalEntries.Add(factory(journalEntryJson));

                journalEntryJsonText = await textReader.ReadLineAsync().ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
            }

            return journalEntries;
        }

        private static JournalEntry _GetMaterialsJournalEntry(JObject journalEntryJson)
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
                    ? ((JArray)materialQuantitiesJson).OfType<JObject>().Select(_GetMaterialQuantityFrom)
                    : Enumerable.Empty<MaterialQuantity>();

                return materialQuantities as IReadOnlyCollection<MaterialQuantity> ?? materialQuantities.ToArray();
            }
        }

        private static JournalEntry _GetMaterialCollectedJournalEntry(JObject journalEntryJson)
            => new MaterialCollectedJournalEntry(_GetTimestampFrom(journalEntryJson), _GetMaterialQuantityFrom(journalEntryJson));

        private static DateTime _GetTimestampFrom(JObject journalEntryJson)
            => journalEntryJson.GetValue("timestamp", StringComparison.OrdinalIgnoreCase).Value<DateTime>();

        private static MaterialQuantity _GetMaterialQuantityFrom(JObject materialQuantityJson)
        {
            var materialQuantity = new MaterialQuantity(
                Material.FindById(materialQuantityJson.GetValue("Name", StringComparison.OrdinalIgnoreCase).Value<string>()),
                materialQuantityJson.GetValue("Count", StringComparison.OrdinalIgnoreCase).Value<int>()
            );

#warning Remove when all materials have been tested
            if (materialQuantityJson.TryGetValue("Name_Localised", StringComparison.OrdinalIgnoreCase, out var localisedName)
                && materialQuantity.Material.Name != localisedName.Value<string>().Trim())
                throw new InvalidDataException($"Expected '{materialQuantity.Material.Name}' material name, actual '{localisedName}' received.");

            return materialQuantity;
        }
    }
}