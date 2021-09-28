using System.IO;
using EDMats.Journals;
using EDMats.Models.Materials;
using Xunit;

namespace EDMats.Tests.Journals
{
    public class JournalReaderTests
    {
        private readonly JournalReader _journalReader = new JournalReader();

        [Fact]
        public void Read_EmptyString_ReturnsCommanderInfoWithNoMaterials()
        {
            using var stringReader = new StringReader("");
            var commanderInfo = _journalReader.Read(stringReader);

            Assert.Empty(commanderInfo.Materials);
        }

        [Fact]
        public void Read_MaterialsEntry_ReturnsCommanderInfoWithMaterials()
        {
            using var stringReader = new StringReader(@"{ ""timestamp"":""2018-12-23T17:44:26Z"", ""event"":""Materials"", ""Raw"":[ { ""Name"":""carbon"", ""Count"":14 }, { ""Name"":""carbon"", ""Count"":7 }, { ""Name"":""manganese"", ""Count"":11 }, { ""Name"":""nickel"", ""Count"":33 } ], ""Manufactured"":[ { ""Name"":""focuscrystals"", ""Count"":4 }, { ""Name"":""exquisitefocuscrystals"", ""Count"":7 }, { ""Name"":""mechanicalscrap"", ""Count"":2 } ], ""Encoded"":[ { ""Name"":""shieldcyclerecordings"", ""Count"":9 }, { ""Name"":""shieldsoakanalysis"", ""Count"":3 }, { ""Name"":""bulkscandata"", ""Count"":45 } ] }");
            var commanderInfo = _journalReader.Read(stringReader);

            Assert.Equal(
                new[]
                {
                    new MaterialQuantity(Material.Carbon, 21),
                    new MaterialQuantity(Material.Manganese, 11),
                    new MaterialQuantity(Material.Nickel, 33),
                    new MaterialQuantity(Material.FocusCrystals, 4),
                    new MaterialQuantity(Material.ExquisiteFocusCrystals, 7),
                    new MaterialQuantity(Material.MechanicalScrap, 2),
                    new MaterialQuantity(Material.DistortedShieldCycleRecordings, 9),
                    new MaterialQuantity(Material.InconsistentShieldSoakAnalysis, 3),
                    new MaterialQuantity(Material.AnomalousBulkScanData, 45)
                },
                commanderInfo.Materials,
                new MaterialQuantityEqualityComparer()
            );
        }

        [Fact]
        public void Read_MaterialsEntryWithoutRelatedProperties_ReturnsCommanderInfoWithNoMaterials()
        {
            using var stringReader = new StringReader(@"{ ""timestamp"":""2018-12-23T17:44:26Z"", ""event"":""Materials"" }");
            var commanderInfo = _journalReader.Read(stringReader);

            Assert.Empty(commanderInfo.Materials);
        }

        [Fact]
        public void Read_MaterialCollectedEntry_ReturnsCommanderInfoWithMaterials()
        {
            using var stringReader = new StringReader(@"
                { ""timestamp"":""2018-12-23T18:25:17Z"", ""event"":""MaterialCollected"", ""Name"":""bulkscandata"", ""Count"":1 }
                { ""timestamp"":""2018-12-23T18:25:17Z"", ""event"":""MaterialCollected"", ""Name"":""bulkscandata"", ""Count"":2 }");
            var commanderInfo = _journalReader.Read(stringReader);

            Assert.Equal(
                new[]
                {
                    new MaterialQuantity(Material.AnomalousBulkScanData, 3)
                },
                commanderInfo.Materials,
                new MaterialQuantityEqualityComparer()
            );
        }

        [Fact]
        public void Read_MaterialCollectedEntryWithoutRelatedProperties_ReturnsCommanderInfoWithMaterials()
        {
            using var stringReader = new StringReader(@"{ ""timestamp"":""2018-12-23T18:25:17Z"", ""event"":""MaterialCollected"" }");
            var commanderInfo = _journalReader.Read(stringReader);

            Assert.Empty(commanderInfo.Materials);
        }
    }
}