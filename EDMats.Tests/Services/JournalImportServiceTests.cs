using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Services;
using EDMats.Services.Implementations;
using EDMats.Services.JournalEntries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class JournalImportServiceTests
    {
        private List<JournalEntry> _journalEntries = new List<JournalEntry>();

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
                new MaterialQuantity
                {
                    Material = Materials.Iron,
                    Amount = 3
                },
                new MaterialQuantity
                {
                    Material = Materials.CrystalShards,
                    Amount = 6
                },
                new MaterialQuantity
                {
                    Material = Materials.DataminedWakeExceptions,
                    Amount = 9
                }
            };
            _journalEntries.Add(
                new MaterialsJournalEntry
                {
                    Encoded = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Encoded)
                        .ToList(),
                    Manufactured = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Manufactured)
                        .ToList(),
                    Raw = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Raw)
                        .ToList()
                }
            );

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderInformation.Materials);
        }

        [TestMethod]
        public async Task ReadingMaterialsAndThenMaterialCollectedEntryUpdatesMaterialAmount()
        {
            var expectedMaterials = new[]
            {
                new MaterialQuantity
                {
                    Material = Materials.Iron,
                    Amount = 4
                },
                new MaterialQuantity
                {
                    Material = Materials.CrystalShards,
                    Amount = 6
                },
                new MaterialQuantity
                {
                    Material = Materials.DataminedWakeExceptions,
                    Amount = 9
                }
            };
            _journalEntries.Add(
                new MaterialsJournalEntry
                {
                    Encoded = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Encoded)
                        .ToList(),
                    Manufactured = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Manufactured)
                        .ToList(),
                    Raw = new[]
                    {
                        new MaterialQuantity
                        {
                            Material = Materials.Iron,
                            Amount = 3
                        }
                    }
                }
            );
            _journalEntries.Add(
                new MaterialCollectedJournalEntry
                {
                    MaterialQuantity = new MaterialQuantity
                    {
                        Material = Materials.Iron,
                        Amount = 1
                    }
                }
            );

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderInformation.Materials);
        }

        [TestMethod]
        public async Task ReadingMaterialsThenMaterialCollectedAndThenMaterialsEntryResetsPreviousInformation()
        {
            var expectedMaterials = new[]
            {
                new MaterialQuantity
                {
                    Material = Materials.Iron,
                    Amount = 4
                },
                new MaterialQuantity
                {
                    Material = Materials.CrystalShards,
                    Amount = 6
                },
                new MaterialQuantity
                {
                    Material = Materials.DataminedWakeExceptions,
                    Amount = 9
                }
            };
            _journalEntries.Add(
                new MaterialsJournalEntry
                {
                    Encoded = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Encoded)
                        .ToList(),
                    Manufactured = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Manufactured)
                        .ToList(),
                    Raw = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Raw)
                        .ToList(),
                }
            );
            _journalEntries.Add(
                new MaterialCollectedJournalEntry
                {
                    MaterialQuantity = new MaterialQuantity
                    {
                        Material = Materials.Iron,
                        Amount = 1
                    }
                }
            );
            _journalEntries.Add(_journalEntries[0]);

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderInformation.Materials);
        }

        [TestMethod]
        public async Task ReadingMaterialCollectedForTheFirstTime()
        {
            var expectedMaterials = new[]
            {
                new MaterialQuantity
                {
                    Material = Materials.Iron,
                    Amount = 3
                },
                new MaterialQuantity
                {
                    Material = Materials.CrystalShards,
                    Amount = 6
                },
                new MaterialQuantity
                {
                    Material = Materials.DataminedWakeExceptions,
                    Amount = 9
                }
            };
            _journalEntries.Add(
                new MaterialsJournalEntry
                {
                    Encoded = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Encoded)
                        .ToList(),
                    Manufactured = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Manufactured)
                        .ToList(),
                    Raw = new List<MaterialQuantity>()
                }
            );
            _journalEntries.Add(
                new MaterialCollectedJournalEntry
                {
                    MaterialQuantity = new MaterialQuantity
                    {
                        Material = Materials.Iron,
                        Amount = 3
                    }
                }
            );

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderInformation.Materials);
        }

        [TestMethod]
        public async Task LogsAreProcessedInOrderBasedOnTimestamp()
        {
            var expectedMaterials = new[]
            {
                new MaterialQuantity
                {
                    Material = Materials.Iron,
                    Amount = 3
                },
                new MaterialQuantity
                {
                    Material = Materials.CrystalShards,
                    Amount = 6
                },
                new MaterialQuantity
                {
                    Material = Materials.DataminedWakeExceptions,
                    Amount = 9
                }
            };
            _journalEntries.Add(
                new MaterialsJournalEntry
                {
                    Timestamp = DateTime.UtcNow.AddDays(1),
                    Encoded = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Encoded)
                        .ToList(),
                    Manufactured = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Manufactured)
                        .ToList(),
                    Raw = expectedMaterials
                        .Where(materialQuantity => materialQuantity.Material.Type == Materials.Raw)
                        .ToList(),
                }
            );
            _journalEntries.Add(
                new MaterialCollectedJournalEntry
                {
                    Timestamp = DateTime.UtcNow,
                    MaterialQuantity = new MaterialQuantity
                    {
                        Material = Materials.Iron,
                        Amount = 1
                    }
                }
            );

            var commanderInformation = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderInformation.Materials);
        }

        [TestMethod]
        public async Task LatestTimestampIsReturned()
        {
            var utcNow = DateTime.UtcNow;

            _journalEntries.Add(
                new MaterialCollectedJournalEntry
                {
                    Timestamp = utcNow.AddDays(1),
                    MaterialQuantity = new MaterialQuantity
                    {
                        Material = Materials.Iron,
                        Amount = 1
                    }
                }
            );
            _journalEntries.Add(
                new MaterialCollectedJournalEntry
                {
                    Timestamp = utcNow,
                    MaterialQuantity = new MaterialQuantity
                    {
                        Material = Materials.Carbon,
                        Amount = 1
                    }
                }
            );

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
            var collectedMaterial = new MaterialQuantity
            {
                Material = Materials.Carbon,
                Amount = 1
            };

            _journalEntries.Add(
                new MaterialCollectedJournalEntry
                {
                    Timestamp = utcNow,
                    MaterialQuantity = new MaterialQuantity
                    {
                        Material = Materials.Iron,
                        Amount = 1
                    }
                }
            );
            _journalEntries.Add(
                new MaterialCollectedJournalEntry
                {
                    Timestamp = utcNow.AddDays(1),
                    MaterialQuantity = collectedMaterial
                }
            );

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