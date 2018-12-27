using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Services.JournalEntries;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EDMats.Services.Implementations
{
    public class JournalReaderService : IJournalReaderService
    {
        private readonly IReadOnlyDictionary<string, Func<JObject, JournalEntry>> _logEntryFactories = new Dictionary<string, Func<JObject, JournalEntry>>(StringComparer.OrdinalIgnoreCase)
        {
            { "Materials", _GetMaterialsLogEntry },
            { "MaterialCollected", _GetMaterialCollectedLogEntry }
        };

        public Task<IReadOnlyList<JournalEntry>> ReadAsync(TextReader textReader)
            => ReadAsync(textReader, CancellationToken.None);

        public async Task<IReadOnlyList<JournalEntry>> ReadAsync(TextReader textReader, CancellationToken cancellationToken)
        {
            var logEntries = new List<JournalEntry>();

            var logEntryJsonText = await textReader.ReadLineAsync().ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            while (logEntryJsonText != null)
            {
                var jsonLogEntry = await Task
                    .Run(() => JsonConvert.DeserializeObject<JObject>(logEntryJsonText))
                    .ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();

                if (_logEntryFactories.TryGetValue((string)jsonLogEntry["event"], out var factory))
                    logEntries.Add(factory(jsonLogEntry));

                logEntryJsonText = await textReader.ReadLineAsync().ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
            }

            return logEntries;
        }

        private static JournalEntry _GetMaterialsLogEntry(JObject jsonLogEntry)
        {
            return new MaterialsJournalEntry
            {
                Timestamp = _GetTimestampFrom(jsonLogEntry),
                Encoded = _GetMaterialQuantities("Encoded"),
                Manufactured = _GetMaterialQuantities("Manufactured"),
                Raw = _GetMaterialQuantities("Raw")
            };

            IReadOnlyCollection<MaterialQuantity> _GetMaterialQuantities(string propertyName)
            {
                jsonLogEntry.TryGetValue(propertyName, StringComparison.OrdinalIgnoreCase, out var x);
                var y = x;

                var materialQuantities = jsonLogEntry.TryGetValue(propertyName, StringComparison.OrdinalIgnoreCase, out var materialQuantitiesJson)
                    ? ((JArray)materialQuantitiesJson).OfType<JObject>().Select(_GetMaterialQuantityFrom)
                    : Enumerable.Empty<MaterialQuantity>();

                return materialQuantities as IReadOnlyCollection<MaterialQuantity> ?? materialQuantities.ToList();
            }
        }

        private static JournalEntry _GetMaterialCollectedLogEntry(JObject jsonLogEntry)
            => new MaterialCollectedJournalEntry
            {
                Timestamp = _GetTimestampFrom(jsonLogEntry),
                MaterialQuantity = _GetMaterialQuantityFrom(jsonLogEntry)
            };

        private static DateTime _GetTimestampFrom(JObject jsonLogEntry)
            => jsonLogEntry.GetValue("timestamp", StringComparison.OrdinalIgnoreCase).Value<DateTime>();

        private static MaterialQuantity _GetMaterialQuantityFrom(JObject materialQuantityJson)
        {
            var materialQuantity = new MaterialQuantity
            {
                Material = Materials.FindById(materialQuantityJson.GetValue("Name", StringComparison.OrdinalIgnoreCase).Value<string>()),
                Amount = materialQuantityJson.GetValue("Count", StringComparison.OrdinalIgnoreCase).Value<int>()
            };

#warning Remove when all materials have been tested
            if (materialQuantityJson.TryGetValue("Name_Localised", StringComparison.OrdinalIgnoreCase, out var localisedName)
                && materialQuantity.Material.Name != localisedName.Value<string>().Trim())
                throw new InvalidDataException($"Expected '{materialQuantity.Material.Name}' material name, actual '{localisedName}' received.");

            return materialQuantity;
        }
    }
}