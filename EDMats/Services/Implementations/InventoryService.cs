using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Services.LogEntries;
using Newtonsoft.Json;

namespace EDMats.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IJournalReaderService _journalReaderService;

        public InventoryService(IJournalReaderService journalReaderService)
        {
            _journalReaderService = journalReaderService;
        }

        public Task<CommanderInventory> GetInventoryAsync(string journalFilePath)
            => GetInventoryAsync(journalFilePath, CancellationToken.None);

        public async Task<CommanderInventory> GetInventoryAsync(string journalFilePath, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<LogEntry> logs;
            using (var fileReader = new StreamReader(journalFilePath, Encoding.UTF8))
                logs = await _journalReaderService.ReadAsync(fileReader, cancellationToken).ConfigureAwait(false);

            var materials = new Dictionary<Material, int>();
            foreach (var log in logs)
            {
                switch (log)
                {
                    case MaterialsLogEntry materialsLogEntry:
                        materials = materialsLogEntry
                            .Raw
                            .Concat(materialsLogEntry.Manufactured)
                            .Concat(materialsLogEntry.Encoded)
                            .ToDictionary(
                                materialQuantity => materialQuantity.Material,
                                materialQuantity => materialQuantity.Amount
                            );
                        break;

                    case MaterialCollectedLogEntry materialCollectedLogEntry:
                        if (materials.TryGetValue(materialCollectedLogEntry.MaterialQuantity.Material, out var amount))
                            materials[materialCollectedLogEntry.MaterialQuantity.Material] = amount + materialCollectedLogEntry.MaterialQuantity.Amount;
                        else
                            materials.Add(materialCollectedLogEntry.MaterialQuantity.Material, materialCollectedLogEntry.MaterialQuantity.Amount);
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