using System.IO;
using EDMats.Journals;
using EDMats.Models.Materials;
using Xunit;

namespace EDMats.Tests.Journals
{
    public class JournalReaderTests
    {
        [Fact]
        public void Read_EmptyString_ReturnsCommanderInfoWithNoMaterials()
        {
            var commanderInfo = _GetCommanderInfo("");

            Assert.Empty(commanderInfo.Materials);
        }

        [Fact]
        public void Read_MaterialsEntry_ReturnsCommanderInfoWithMaterials()
        {
            var commanderInfo = _GetCommanderInfo(@"
                { ""timestamp"":""2018-12-23T17:44:26Z"", ""event"":""Materials"", ""Raw"":[ { ""Name"":""carbon"", ""Count"":14 }, { ""Name"":""carbon"", ""Count"":7 }, { ""Name"":""manganese"", ""Count"":11 }, { ""Name"":""nickel"", ""Count"":33 } ], ""Manufactured"":[ { ""Name"":""focuscrystals"", ""Count"":4 }, { ""Name"":""exquisitefocuscrystals"", ""Count"":7 }, { ""Name"":""mechanicalscrap"", ""Count"":2 } ], ""Encoded"":[ { ""Name"":""shieldcyclerecordings"", ""Count"":9 }, { ""Name"":""shieldsoakanalysis"", ""Count"":3 }, { ""Name"":""bulkscandata"", ""Count"":45 } ] }
            ");

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
                commanderInfo.Materials
            );
        }

        [Fact]
        public void Read_MaterialsEntryWithoutRelatedProperties_ReturnsCommanderInfoWithNoMaterials()
        {
            var commanderInfo = _GetCommanderInfo(@"
                { ""timestamp"":""2018-12-23T17:44:26Z"", ""event"":""Materials"" }
            ");

            Assert.Empty(commanderInfo.Materials);
        }

        [Fact]
        public void Read_MultipleMaterialsEntryWithoutRelatedProperties_ReturnsCommanderInfoWithLatestMaterialsEntry()
        {
            var commanderInfo = _GetCommanderInfo(@"
                { ""timestamp"":""2018-12-23T17:44:26Z"", ""event"":""Materials"", ""Raw"":[ { ""Name"":""carbon"", ""Count"":14 }, { ""Name"":""manganese"", ""Count"":11 }, { ""Name"":""nickel"", ""Count"":33 } ], ""Manufactured"":[ { ""Name"":""focuscrystals"", ""Count"":4 }, { ""Name"":""exquisitefocuscrystals"", ""Count"":7 }, { ""Name"":""mechanicalscrap"", ""Count"":2 } ], ""Encoded"":[ { ""Name"":""shieldcyclerecordings"", ""Count"":9 }, { ""Name"":""shieldsoakanalysis"", ""Count"":3 }, { ""Name"":""bulkscandata"", ""Count"":45 } ] }
                { ""timestamp"":""2018-12-23T17:44:26Z"", ""event"":""Materials"", ""Raw"":[ { ""Name"":""carbon"", ""Count"":1 }, { ""Name"":""manganese"", ""Count"":2 }, { ""Name"":""nickel"", ""Count"":3 } ], ""Manufactured"":[ { ""Name"":""focuscrystals"", ""Count"":4 }, { ""Name"":""exquisitefocuscrystals"", ""Count"":5 }, { ""Name"":""mechanicalscrap"", ""Count"":6 } ], ""Encoded"":[ { ""Name"":""shieldcyclerecordings"", ""Count"":7 }, { ""Name"":""shieldsoakanalysis"", ""Count"":8 } ] }
            ");

            Assert.Equal(
                new[]
                {
                    new MaterialQuantity(Material.Carbon, 1),
                    new MaterialQuantity(Material.Manganese, 2),
                    new MaterialQuantity(Material.Nickel, 3),
                    new MaterialQuantity(Material.FocusCrystals, 4),
                    new MaterialQuantity(Material.ExquisiteFocusCrystals, 5),
                    new MaterialQuantity(Material.MechanicalScrap, 6),
                    new MaterialQuantity(Material.DistortedShieldCycleRecordings, 7),
                    new MaterialQuantity(Material.InconsistentShieldSoakAnalysis, 8)
                },
                commanderInfo.Materials
            );
        }

        [Fact]
        public void Read_MaterialCollectedEntry_ReturnsCommanderInfoWithMaterials()
        {
            var commanderInfo = _GetCommanderInfo(@"
                { ""timestamp"":""2018-12-23T18:25:17Z"", ""event"":""MaterialCollected"", ""Name"":""bulkscandata"", ""Count"":1 }
                { ""timestamp"":""2018-12-23T18:25:17Z"", ""event"":""MaterialCollected"", ""Name"":""bulkscandata"", ""Count"":2 }
                { ""timestamp"":""2018-12-23T18:25:17Z"", ""event"":""MaterialCollected"" }
            ");

            Assert.Equal(
                new[]
                {
                    new MaterialQuantity(Material.AnomalousBulkScanData, 3)
                },
                commanderInfo.Materials
            );
        }

        [Fact]
        public void Read_MissionCompletedEntry_ReturnsCommanderInfoWithMaterials()
        {
            var commanderInfo = _GetCommanderInfo(@"
                { ""timestamp"":""2018-12-23T18:25:17Z"", ""event"":""MissionCompleted"",""MaterialsReward"": [{ ""Name"": ""DisruptedWakeEchoes"", ""Name_Localised"": ""Atypical Disrupted Wake Echoes"", ""Category"": ""$MICRORESOURCE_CATEGORY_Encoded;"", ""Category_Localised"": ""Encoded"", ""Count"": 3 }] }
                { ""timestamp"":""2018-12-23T18:25:17Z"", ""event"":""MissionCompleted"",""MaterialsReward"": [] }
                { ""timestamp"":""2018-12-23T18:25:17Z"", ""event"":""MissionCompleted"" }
            ");

            Assert.Equal(
                new[]
                {
                    new MaterialQuantity(Material.AtypicalDisruptedWakeEchoes, 3)
                },
                commanderInfo.Materials
            );
        }

        private static CommanderInfo _GetCommanderInfo(string journalFileJson)
        {
            var commanderInfoJournalEntryVisitor = new CommanderInfoJournalEntryVisitor();

            using var stringReader = new StringReader(journalFileJson);
            foreach (var journalEntry in  new JournalReader().Read(stringReader))
                journalEntry.Accept(commanderInfoJournalEntryVisitor);

            return commanderInfoJournalEntryVisitor.CommanderInfo;
        }
    }
}