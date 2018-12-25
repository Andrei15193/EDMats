using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EDMats.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        public Task<CommanderInventory> GetInventoryAsync(string journalFilePath)
            => GetInventoryAsync(journalFilePath, CancellationToken.None);

        public async Task<CommanderInventory> GetInventoryAsync(string journalFilePath, CancellationToken cancellationToken)
        {
            var logs = await _ReadLogsAsync(journalFilePath, cancellationToken).ConfigureAwait(false);
            var materials = new Dictionary<Material, int>();

            foreach (var log in logs)
            {
                switch (log.@event)
                {
                    case "Materials":
                        materials = ((IEnumerable<dynamic>)log.Raw)
                                .Concat((IEnumerable<dynamic>)log.Manufactured)
                                .Concat((IEnumerable<dynamic>)log.Encoded)
                                .ToDictionary(
                                    material =>
                                    {
#warning Remove when all materials have been tested
                                        var mat = Materials.FindById((string)material.Name);
                                        if (((IDictionary<string, object>)material).TryGetValue("Name_Localised", out object nameLocalised2) && mat.Name != ((string)nameLocalised2).Trim())
                                            throw new InvalidDataException();
                                        return mat;
                                    },
                                    material => (int)material.Count);
                        break;

                    case "MaterialCollected":
                        var collectedMaterial = Materials.FindById((string)log.Name);
                        var collectedAmount = (int)log.Count;
                        if (materials.TryGetValue(collectedMaterial, out int amount))
                            materials[collectedMaterial] = amount + collectedAmount;
                        else
                            materials.Add(collectedMaterial, collectedAmount);

#warning Remove when all materials have been tested
                        if (((IDictionary<string, object>)log).TryGetValue("Name_Localised", out object nameLocalised) && collectedMaterial.Name != ((string)nameLocalised).Trim())
                            throw new InvalidDataException();
                        break;
                }
            }

            return new CommanderInventory
            {
                Materials = materials
            };
        }

        private async Task<IReadOnlyList<dynamic>> _ReadLogsAsync(string journalFilePath, CancellationToken cancellationToken)
        {
            var logs = new List<dynamic>();

            using (var fileStream = new FileStream(journalFilePath, FileMode.Open))
            using (var streamReader = new StreamReader(fileStream))
            {
                var logEntryJson = await streamReader.ReadLineAsync().ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();

                while (logEntryJson != null)
                {
                    using (var stringReader = new StringReader(logEntryJson))
                    using (var reader = new JsonTextReader(stringReader) { DateFormatString = "yyyy-MM-ddTHH:mm:ssZ", Culture = CultureInfo.InvariantCulture })
                    {
                        var logEntry = await Task.Run(() => JsonConvert.DeserializeObject<ExpandoObject>(logEntryJson)).ConfigureAwait(false);
                        cancellationToken.ThrowIfCancellationRequested();

                        logs.Add(logEntry);
                    }

                    logEntryJson = await streamReader.ReadLineAsync().ConfigureAwait(false);
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }

            return logs;
        }
    }
}