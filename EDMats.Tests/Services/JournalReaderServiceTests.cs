using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EDMats.Services;
using EDMats.Services.Implementations;
using EDMats.Services.JournalEntries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class JournalReaderServiceTests
    {
        private IJournalReaderService _JournalReaderService { get; } = new JournalReaderService();

        [TestMethod]
        public async Task ReadingEmptyStringReturnsEmptyJournalEntriesCollection()
        {
            IReadOnlyList<JournalEntry> journalEntries;

            using (var stringReader = new StringReader(""))
                journalEntries = await _JournalReaderService.ReadAsync(stringReader);

            Assert.AreEqual(0, journalEntries.Count);
        }

        [TestMethod]
        public async Task ReadingMaterialsEntryReturnsListOfMaterials()
        {
            IReadOnlyList<JournalEntry> journalEntries;

            using (var stringReader = new StringReader(@"{ ""timestamp"":""2018-12-23T17:44:26Z"", ""event"":""Materials"", ""Raw"":[ { ""Name"":""carbon"", ""Count"":21 }, { ""Name"":""manganese"", ""Count"":11 }, { ""Name"":""nickel"", ""Count"":33 } ], ""Manufactured"":[ { ""Name"":""focuscrystals"", ""Count"":4 }, { ""Name"":""exquisitefocuscrystals"", ""Count"":7 }, { ""Name"":""mechanicalscrap"", ""Count"":2 } ], ""Encoded"":[ { ""Name"":""shieldcyclerecordings"", ""Count"":9 }, { ""Name"":""shieldsoakanalysis"", ""Count"":3 }, { ""Name"":""bulkscandata"", ""Count"":45 } ] }"))
                journalEntries = await _JournalReaderService.ReadAsync(stringReader);

            Assert.AreEqual(1, journalEntries.Count);
            var materialsJournalEntry = (MaterialsJournalEntry)journalEntries.Single();
            Assert.AreEqual(new DateTime(2018, 12, 23, 17, 44, 26, DateTimeKind.Utc), materialsJournalEntry.Timestamp);
            _AssertCollectionsAreEqual(
                new[]
                {
                    new MaterialQuantity
                    {
                        Material = Materials.Carbon,
                        Amount = 21
                    },
                    new MaterialQuantity
                    {
                        Material = Materials.Manganese,
                        Amount = 11
                    },
                    new MaterialQuantity
                    {
                        Material = Materials.Nickel,
                        Amount = 33
                    }
                },
                materialsJournalEntry.Raw
            );

            _AssertCollectionsAreEqual(
                new[]
                {
                    new MaterialQuantity
                    {
                        Material = Materials.FocusCrystals,
                        Amount = 4
                    },
                    new MaterialQuantity
                    {
                        Material = Materials.ExquisiteFocusCrystals,
                        Amount = 7
                    },
                    new MaterialQuantity
                    {
                        Material = Materials.MechanicalScrap,
                        Amount = 2
                    }
                },
                materialsJournalEntry.Manufactured
            );

            _AssertCollectionsAreEqual(new[]
                {
                    new MaterialQuantity
                    {
                        Material = Materials.DistortedShieldCycleRecordings,
                        Amount = 9
                    },
                    new MaterialQuantity
                    {
                        Material = Materials.InconsistentShieldSoakAnalysis,
                        Amount = 3
                    },
                    new MaterialQuantity
                    {
                        Material = Materials.AnomalousBulkScanData,
                        Amount = 45
                    }
                },
                materialsJournalEntry.Encoded
            );
        }

        [TestMethod]
        public async Task ReadingMaterialsEntryWithoutCorrespondingPropertiesReturnsEmptyCollections()
        {
            IReadOnlyList<JournalEntry> journalEntries;

            using (var stringReader = new StringReader(@"{ ""timestamp"":""2018-12-23T17:44:26Z"", ""event"":""Materials"" }"))
                journalEntries = await _JournalReaderService.ReadAsync(stringReader);

            Assert.AreEqual(1, journalEntries.Count);
            var materialsJournalEntry = (MaterialsJournalEntry)journalEntries.Single();
            Assert.AreEqual(new DateTime(2018, 12, 23, 17, 44, 26, DateTimeKind.Utc), materialsJournalEntry.Timestamp);
            _AssertCollectionsAreEqual(Enumerable.Empty<MaterialQuantity>(), materialsJournalEntry.Raw);
            _AssertCollectionsAreEqual(Enumerable.Empty<MaterialQuantity>(), materialsJournalEntry.Manufactured);
            _AssertCollectionsAreEqual(Enumerable.Empty<MaterialQuantity>(), materialsJournalEntry.Encoded);
        }

        [TestMethod]
        public async Task ReadingMaterialCollectedEntryReturnsCollectedMaterialQuantity()
        {
            IReadOnlyList<JournalEntry> journalEntries;

            using (var stringReader = new StringReader(@"{ ""timestamp"":""2018-12-23T18:25:17Z"", ""event"":""MaterialCollected"", ""Name"":""bulkscandata"", ""Count"":3 }"))
                journalEntries = await _JournalReaderService.ReadAsync(stringReader);

            Assert.AreEqual(1, journalEntries.Count);
            var materialCollectedJournalEntry = (MaterialCollectedJournalEntry)journalEntries.Single();
            Assert.AreEqual(new DateTime(2018, 12, 23, 18, 25, 17, DateTimeKind.Utc), materialCollectedJournalEntry.Timestamp);
            Assert.AreEqual(Materials.AnomalousBulkScanData, materialCollectedJournalEntry.MaterialQuantity.Material);
            Assert.AreEqual(3, materialCollectedJournalEntry.MaterialQuantity.Amount);
        }

        private static void _AssertCollectionsAreEqual(IEnumerable<MaterialQuantity> expected, IEnumerable<MaterialQuantity> actual)
        {
            Assert.IsTrue(
                expected
                .Select(materialQuantity =>
                    new
                    {
                        materialQuantity.Material,
                        materialQuantity.Amount
                    }
                )
                .SequenceEqual(actual
                    .Select(materialQuantity =>
                        new
                        {
                            materialQuantity.Material,
                            materialQuantity.Amount
                        }
                    )
                )
            );
        }
    }
}