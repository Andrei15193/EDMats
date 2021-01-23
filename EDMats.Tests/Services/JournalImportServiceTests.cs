using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Data.JournalEntries;
using EDMats.Data.Materials;
using EDMats.Services;
using EDMats.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class JournalImportServiceTests
    {
        private List<JournalEntry> _journalEntries;

        private Mock<IJournalReaderService> _JournalReaderService { get; set; }

        private JournalImportService _JournalImportService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            _journalEntries = new List<JournalEntry>();
            _JournalReaderService = new Mock<IJournalReaderService>();
            _JournalReaderService
                .Setup(journalReaderService => journalReaderService.ReadAsync(It.IsAny<TextReader>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_journalEntries);

            _JournalImportService = new JournalImportService(_JournalReaderService.Object);
        }

        [TestMethod]
        public async Task ReadingMaterialsEntry()
        {
            var expectedMaterials = new[]
            {
                new MaterialQuantity(Material.Iron, 3),
                new MaterialQuantity(Material.CrystalShards, 6),
                new MaterialQuantity(Material.DataminedWakeExceptions, 9)
            };
            _journalEntries.Add(new MaterialsJournalEntry(
                DateTime.Now,
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Encoded)
                    .ToList(),
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Manufactured)
                    .ToList(),
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Raw)
                    .ToList()
            ));

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderInformation.Materials);
        }

        [TestMethod]
        public async Task ReadingMaterialsAndThenMaterialCollectedEntryUpdatesMaterialAmount()
        {
            var expectedMaterials = new[]
            {
                new MaterialQuantity(Material.Iron, 4),
                new MaterialQuantity(Material.CrystalShards, 6),
                new MaterialQuantity( Material.DataminedWakeExceptions, 9)
            };
            _journalEntries.Add(new MaterialsJournalEntry(
                DateTime.Now,
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Encoded)
                    .ToList(),
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Manufactured)
                    .ToList(),
                new[] { new MaterialQuantity(Material.Iron, 3) }
            ));
            _journalEntries.Add(new MaterialCollectedJournalEntry(DateTime.Now, new MaterialQuantity(Material.Iron, 1)));

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderInformation.Materials);
        }

        [TestMethod]
        public async Task ReadingMaterialsThenMaterialCollectedAndThenMaterialsEntryResetsPreviousInformation()
        {
            var now = DateTime.Now;
            var expectedMaterials = new[]
            {
                new MaterialQuantity(Material.Iron, 4),
                new MaterialQuantity(Material.CrystalShards, 6),
                new MaterialQuantity(Material.DataminedWakeExceptions, 9)
            };
            _journalEntries.Add(new MaterialsJournalEntry(
                now,
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Raw)
                    .ToList(),
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Manufactured)
                    .ToList(),
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Encoded)
                    .ToList()
            ));
            _journalEntries.Add(new MaterialCollectedJournalEntry(now, new MaterialQuantity(Material.Iron, 1)));
            _journalEntries.Add(_journalEntries[0]);

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderInformation.Materials);
        }

        [TestMethod]
        public async Task ReadingMaterialCollectedForTheFirstTime()
        {
            var expectedMaterials = new[]
            {
                new MaterialQuantity(Material.Iron, 3),
                new MaterialQuantity(Material.CrystalShards, 6),
                new MaterialQuantity(Material.DataminedWakeExceptions, 9)
            };
            _journalEntries.Add(new MaterialsJournalEntry(
                DateTime.Now,
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Encoded)
                    .ToList(),
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Manufactured)
                    .ToList(),
                new MaterialQuantity[0]
            ));
            _journalEntries.Add(new MaterialCollectedJournalEntry(DateTime.Now, new MaterialQuantity(Material.Iron, 3)));

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderInformation.Materials);
        }

        [TestMethod]
        public async Task LogsAreProcessedInOrderBasedOnTimestamp()
        {
            var utcNow = DateTime.UtcNow;
            var expectedMaterials = new[]
            {
                new MaterialQuantity(Material.Iron, 3),
                new MaterialQuantity(Material.CrystalShards, 6),
                new MaterialQuantity(Material.DataminedWakeExceptions, 9)
            };
            _journalEntries.Add(new MaterialsJournalEntry(
                utcNow.AddDays(1),
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Encoded)
                    .ToList(),
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Manufactured)
                    .ToList(),
                expectedMaterials
                    .Where(materialQuantity => materialQuantity.Material.Type == Material.Raw)
                    .ToList()
            ));
            _journalEntries.Add(new MaterialCollectedJournalEntry(utcNow, new MaterialQuantity(Material.Iron, 1)));

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderInformation.Materials);
        }

        [TestMethod]
        public async Task LatestTimestampIsReturned()
        {
            var utcNow = DateTime.UtcNow;

            _journalEntries.Add(new MaterialCollectedJournalEntry(utcNow.AddDays(1), new MaterialQuantity(Material.Iron, 1)));
            _journalEntries.Add(new MaterialCollectedJournalEntry(utcNow, new MaterialQuantity(Material.Carbon, 1)));

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            Assert.AreEqual(utcNow.AddDays(1), commanderInformation.LatestUpdate);
        }

        [TestMethod]
        public async Task EarliestUtcDateTimeIsReturnedWhenThereAreNoLogs()
        {
            var expected = DateTime.MinValue.ToUniversalTime();

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            Assert.AreEqual(expected, commanderInformation.LatestUpdate);
        }

        [TestMethod]
        public async Task ImportingLatestUpdatesSkipsProcessedJournalEntries()
        {
            var utcNow = DateTime.UtcNow;
            var collectedMaterial = new MaterialQuantity(Material.Carbon, 1);

            _journalEntries.Add(new MaterialCollectedJournalEntry(utcNow, new MaterialQuantity(Material.Iron, 1)));
            _journalEntries.Add(new MaterialCollectedJournalEntry(utcNow.AddDays(1), collectedMaterial));

            var updates = await _JournalImportService.ImportLatestJournalUpdatesAsync(null, utcNow);

            Assert.AreEqual(1, updates.Count);
            var update = (MaterialCollectedJournalUpdate)updates.Single();
            Assert.AreEqual(utcNow.AddDays(1), update.Timestamp);
            Assert.AreSame(update.CollectedMaterial, collectedMaterial);
        }

        private static void _AssertAreEqual(IEnumerable<MaterialQuantity> expected, IEnumerable<MaterialQuantity> actual)
        {
            Assert.IsTrue(
                expected
                    .OrderBy(materialQuantity => materialQuantity.Material.Id)
                    .Select(materialQuantity => new { materialQuantity.Material, materialQuantity.Amount })
                    .SequenceEqual(
                        actual
                            .OrderBy(materialQuantity => materialQuantity.Material.Id)
                            .Select(materialQuantity => new { Material = materialQuantity.Material, Amount = materialQuantity.Amount })
                    )
            );
        }
    }
}