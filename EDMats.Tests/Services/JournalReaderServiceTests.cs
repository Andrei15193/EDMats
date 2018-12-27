using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EDMats.Services;
using EDMats.Services.Implementations;
using EDMats.Services.LogEntries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class JournalReaderServiceTests
    {
        private IJournalReaderService _JournalReaderService { get; } = new JournalReaderService();

        [TestMethod]
        public async Task ReadingEmptyStringReturnsEmptyLogsCollection()
        {
            IReadOnlyList<LogEntry> logs;

            using (var stringReader = new StringReader(""))
                logs = await _JournalReaderService.ReadAsync(stringReader);

            Assert.AreEqual(0, logs.Count);
        }

        [TestMethod]
        public async Task ReadingMaterialsEntryReturnsListOfMaterials()
        {
            IReadOnlyList<LogEntry> logs;

            using (var stringReader = new StringReader(@"{ ""timestamp"":""2018-12-23T17:44:26Z"", ""event"":""Materials"", ""Raw"":[ { ""Name"":""carbon"", ""Count"":21 }, { ""Name"":""manganese"", ""Count"":11 }, { ""Name"":""nickel"", ""Count"":33 } ], ""Manufactured"":[ { ""Name"":""focuscrystals"", ""Count"":4 }, { ""Name"":""exquisitefocuscrystals"", ""Count"":7 }, { ""Name"":""mechanicalscrap"", ""Count"":2 } ], ""Encoded"":[ { ""Name"":""shieldcyclerecordings"", ""Count"":9 }, { ""Name"":""shieldsoakanalysis"", ""Count"":3 }, { ""Name"":""bulkscandata"", ""Count"":45 } ] }"))
                logs = await _JournalReaderService.ReadAsync(stringReader);

            Assert.AreEqual(1, logs.Count);
            var materialsLogEntry = (MaterialsLogEntry)logs.Single();
            Assert.AreEqual(new DateTime(2018, 12, 23, 17, 44, 26, DateTimeKind.Utc), materialsLogEntry.Timestamp);
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
                materialsLogEntry.Raw
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
                materialsLogEntry.Manufactured
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
                materialsLogEntry.Encoded
            );
        }

        [TestMethod]
        public async Task ReadingMaterialsEntryWithoutCorrespondingPropertiesReturnsEmptyCollections()
        {
            IReadOnlyList<LogEntry> logs;

            using (var stringReader = new StringReader(@"{ ""timestamp"":""2018-12-23T17:44:26Z"", ""event"":""Materials"" }"))
                logs = await _JournalReaderService.ReadAsync(stringReader);

            Assert.AreEqual(1, logs.Count);
            var materialsLogEntry = (MaterialsLogEntry)logs.Single();
            Assert.AreEqual(new DateTime(2018, 12, 23, 17, 44, 26, DateTimeKind.Utc), materialsLogEntry.Timestamp);
            _AssertCollectionsAreEqual(Enumerable.Empty<MaterialQuantity>(), materialsLogEntry.Raw);
            _AssertCollectionsAreEqual(Enumerable.Empty<MaterialQuantity>(), materialsLogEntry.Manufactured);
            _AssertCollectionsAreEqual(Enumerable.Empty<MaterialQuantity>(), materialsLogEntry.Encoded);
        }

        [TestMethod]
        public async Task ReadingMaterialCollectedEntryReturnsCollectedMaterialQuantity()
        {
            IReadOnlyList<LogEntry> logs;

            using (var stringReader = new StringReader(@"{ ""timestamp"":""2018-12-23T18:25:17Z"", ""event"":""MaterialCollected"", ""Name"":""bulkscandata"", ""Count"":3 }"))
                logs = await _JournalReaderService.ReadAsync(stringReader);

            Assert.AreEqual(1, logs.Count);
            var materialCollectedLogEntry = (MaterialCollectedLogEntry)logs.Single();
            Assert.AreEqual(new DateTime(2018, 12, 23, 18, 25, 17, DateTimeKind.Utc), materialCollectedLogEntry.Timestamp);
            Assert.AreEqual(Materials.AnomalousBulkScanData, materialCollectedLogEntry.MaterialQuantity.Material);
            Assert.AreEqual(3, materialCollectedLogEntry.MaterialQuantity.Amount);
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