using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Services.JournalEntries;

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
            var journalEntries = await _journalReaderService.ReadAsync(journalFileReader, cancellationToken).ConfigureAwait(false);

            var latestJournalEntryTimestamp = DateTime.MinValue.ToUniversalTime();
            var materials = new Dictionary<Material, int>();
            foreach (var journalEntry in journalEntries.OrderBy(journalEntry => journalEntry.Timestamp))
            {
                latestJournalEntryTimestamp = journalEntry.Timestamp;
                switch (journalEntry)
                {
                    case MaterialsJournalEntry materialsJournalEntry:
                        materials = materialsJournalEntry
                            .Raw
                            .Concat(materialsJournalEntry.Manufactured)
                            .Concat(materialsJournalEntry.Encoded)
                            .ToDictionary(
                                materialQuantity => materialQuantity.Material,
                                materialQuantity => materialQuantity.Amount
                            );
                        break;

                    case MaterialCollectedJournalEntry materialCollectedJournalEntry:
                        if (materials.TryGetValue(materialCollectedJournalEntry.MaterialQuantity.Material, out var amount))
                            materials[materialCollectedJournalEntry.MaterialQuantity.Material] = amount + materialCollectedJournalEntry.MaterialQuantity.Amount;
                        else
                            materials.Add(materialCollectedJournalEntry.MaterialQuantity.Material, materialCollectedJournalEntry.MaterialQuantity.Amount);
                        break;
                }
            }

            return new JournalCommanderInformation
            {
                LatestUpdate = latestJournalEntryTimestamp,
                Materials = materials
                    .Select(
                        materialQuantity => new MaterialQuantity
                        {
                            Material = materialQuantity.Key,
                            Amount = materialQuantity.Value
                        }
                    )
                    .ToList()
            };
        }
    }
}