using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Services.LogEntries;

namespace EDMats.Services.Implementations
{
    public class JournalImportService : IJournalImportService
    {
        private readonly IJournalReaderService _journalReaderService;

        public JournalImportService(IJournalReaderService journalReaderService)
        {
            _journalReaderService = journalReaderService;
        }

        public Task<JournalCommanderInformation> ImportJournalAsync(TextReader journalFileReader)
            => ImportJournalAsync(journalFileReader, CancellationToken.None);

        public async Task<JournalCommanderInformation> ImportJournalAsync(TextReader journalFileReader, CancellationToken cancellationToken)
        {
            var logs = await _journalReaderService.ReadAsync(journalFileReader, cancellationToken).ConfigureAwait(false);

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

            return new JournalCommanderInformation
            {
                Materials = materials
            };
        }
    }
}