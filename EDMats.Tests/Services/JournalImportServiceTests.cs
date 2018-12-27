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
                .Setup(journalReaderService => journalReaderService.ReadAsync(It.IsAny<TextReader>()))
                .ReturnsAsync(_journalEntries);
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

            var commanderData = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderData.Materials);
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

            var commanderData = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderData.Materials);
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

            var commanderData = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderData.Materials);
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

            var commanderData = await _JournalImportService.ImportJournalAsync(null);

            _AssertAreEqual(expectedMaterials, commanderData.Materials);
        }

        private static void _AssertAreEqual(IEnumerable<MaterialQuantity> expected, IReadOnlyDictionary<Material, int> actual)
        {
            Assert.IsTrue(
                expected
                    .OrderBy(materialQuantity => materialQuantity.Material.Id)
                    .Select(materialQuantity => new { materialQuantity.Material, materialQuantity.Amount })
                    .SequenceEqual(
                        actual
                            .OrderBy(materialQuantity => materialQuantity.Key.Id)
                            .Select(materialQuantity => new { Material = materialQuantity.Key, Amount = materialQuantity.Value })
                    )
            );
        }
    }
}