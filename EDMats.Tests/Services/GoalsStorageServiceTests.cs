using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EDMats.Services;
using EDMats.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class GoalsStorageServiceTests
    {
        private IGoalsStorageService _GoalsStoreService { get; } = new GoalsStorageService();

        [TestMethod]
        public async Task WritingAndReadingCommanderGoals()
        {
            var expectedCommanderGoals = new CommanderGoalsData
            {
                Materials = new[]
                {
                    new MaterialGoalData
                    {
                        MaterialId = "carbon",
                        Name = "Carbon",
                        Amount = 3
                    },
                    new MaterialGoalData
                    {
                        MaterialId = "focuscrystals",
                        Name = "Focus Crystals",
                        Amount = 6
                    },
                    new MaterialGoalData
                    {
                        MaterialId = "sulphur",
                        Name = "Sulphur",
                        Amount = 9
                    }
                }
            };
            CommanderGoalsData actualCommanderGoals;

            string json;
            using (var stringWriter = new StringWriter())
            {
                await _GoalsStoreService.WriteGoalsAsync(stringWriter, expectedCommanderGoals);
                await stringWriter.FlushAsync();
                json = stringWriter.ToString();
            }
            using (var stringReader = new StringReader(json))
                actualCommanderGoals = await _GoalsStoreService.ReadGoalsAsync(stringReader);

            _AssertAreEqual(
                expectedCommanderGoals,
                actualCommanderGoals
            );
        }

        [TestMethod]
        public async Task ReadMaterialsGoal()
        {
            CommanderGoalsData commanderGoals;
            using (var stringReader = new StringReader(@"
{
    ""materials"": [
        {
            ""name"": ""carbon"",
            ""count"": 3
        },
        {
            ""name"": ""focuscrystals"",
            ""count"": 6
        },
        {
            ""name"": ""sulphur"",
            ""count"": 9
        }
    ]
}
"))
                commanderGoals = await _GoalsStoreService.ReadGoalsAsync(stringReader);

            _AssertAreEqual(
                new CommanderGoalsData
                {
                    Materials = new[]
                    {
                        new MaterialGoalData
                        {
                            MaterialId = "carbon",
                            Name = "Carbon",
                            Amount = 3
                        },
                        new MaterialGoalData
                        {
                            MaterialId = "focuscrystals",
                            Name = "Focus Crystals",
                            Amount = 6
                        },
                        new MaterialGoalData
                        {
                            MaterialId = "sulphur",
                            Name = "Sulphur",
                            Amount = 9
                        }
                    }
                },
                commanderGoals
            );
        }

        [TestMethod]
        public async Task ReadMaterialsGoalSkipsUnknownProperties()
        {
            CommanderGoalsData commanderGoals;
            using (var stringReader = new StringReader(@"
{
    ""materials"": [
        {
            ""name"":""carbon"",
            ""count"":3,
            ""materials.p1"": ""Test"",
            ""materials.p2"": { },
            ""materials.p3"": { ""p3.1"": ""Test"" },
            ""materials.p4"": [],
            ""materials.p5"": [ { ""p5.1"": ""Test"" } ]
        }
    ],
    ""p1"": ""Test"",
    ""p2"": { },
    ""p3"": { ""p3.1"": ""Test"" },
    ""p4"": [],
    ""p5"": [ { ""p5.1"": ""Test"" } ]
}
"))
                commanderGoals = await _GoalsStoreService.ReadGoalsAsync(stringReader);

            _AssertAreEqual(
                new CommanderGoalsData
                {
                    Materials = new MaterialGoalData[]
                    {
                        new MaterialGoalData
                        {
                            MaterialId = "carbon",
                            Name = "Carbon",
                            Amount = 3
                        }
                    }
                },
                commanderGoals
            );
        }

        [TestMethod]
        public async Task ReadMaterialsGoalFromEmptyJsonArray()
        {
            CommanderGoalsData commanderGoals;
            using (var stringReader = new StringReader(@"
{
    ""materials"": []
}
"))
                commanderGoals = await _GoalsStoreService.ReadGoalsAsync(stringReader);

            _AssertAreEqual(
                new CommanderGoalsData
                {
                    Materials = Array.Empty<MaterialGoalData>()
                },
                commanderGoals
            );
        }

        [TestMethod]
        public async Task ReadMaterialsGoalFromEmptyString()
        {
            CommanderGoalsData commanderGoals;
            using (var stringReader = new StringReader(string.Empty))
                commanderGoals = await _GoalsStoreService.ReadGoalsAsync(stringReader);

            _AssertAreEqual(
                new CommanderGoalsData
                {
                    Materials = Array.Empty<MaterialGoalData>()
                },
                commanderGoals
            );
        }

        [TestMethod]
        public async Task TryingToReadFromNullThrowsException()
        {
            var exception = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _GoalsStoreService.ReadGoalsAsync(null));
            Assert.AreEqual(new ArgumentNullException("textReader").Message, exception.Message);
        }

        [TestMethod]
        public async Task WriteMaterialsGoal()
        {
            string json;
            var commanderGoals = new CommanderGoalsData
            {
                Materials = new[]
                {
                    new MaterialGoalData
                    {
                        MaterialId = "carbon",
                        Name = "Carbon",
                        Amount = 3
                    },
                    new MaterialGoalData
                    {
                        MaterialId = "focuscrystals",
                        Name = "Focus Crystals",
                        Amount = 6
                    },
                    new MaterialGoalData
                    {
                        MaterialId = "sulphur",
                        Name = "Sulphur",
                        Amount = 9
                    }
                }
            };
            using (var stringWriter = new StringWriter())
            {
                await _GoalsStoreService.WriteGoalsAsync(stringWriter, commanderGoals);
                json = stringWriter.ToString();
            }

            Assert.AreEqual(
                Regex.Replace(@"
{
    ""materials"": [
        {
            ""name"": ""carbon"",
            ""count"": 3
        },
        {
            ""name"": ""focuscrystals"",
            ""count"": 6
        },
        {
            ""name"": ""sulphur"",
            ""count"": 9
        }
    ]
}
",
                    "\\s+",
                    string.Empty),
                json
            );
        }

        [TestMethod]
        public async Task MaterialsGoalWithZeroAmountAreIgnoredWhenWriting()
        {
            string json;
            var commanderGoals = new CommanderGoalsData
            {
                Materials = new[]
                {
                    new MaterialGoalData
                    {
                        MaterialId = "carbon",
                        Name = "Carbon",
                        Amount = 3
                    },
                    new MaterialGoalData
                    {
                        MaterialId = "focuscrystals",
                        Name = "Focus Crystals",
                        Amount = 0
                    },
                    new MaterialGoalData
                    {
                        MaterialId = "sulphur",
                        Name = "Sulphur",
                        Amount = 9
                    }
                }
            };
            using (var stringWriter = new StringWriter())
            {
                await _GoalsStoreService.WriteGoalsAsync(stringWriter, commanderGoals);
                json = stringWriter.ToString();
            }

            Assert.AreEqual(
                Regex.Replace(@"
{
    ""materials"": [
        {
            ""name"": ""carbon"",
            ""count"": 3
        },
        {
            ""name"": ""sulphur"",
            ""count"": 9
        }
    ]
}
",
                    "\\s+",
                    string.Empty),
                json
            );
        }

        [TestMethod]
        public async Task WriteEmptyMaterialsGoal()
        {
            string json;
            var commanderGoals = new CommanderGoalsData
            {
                Materials = Array.Empty<MaterialGoalData>()
            };
            using (var stringWriter = new StringWriter())
            {
                await _GoalsStoreService.WriteGoalsAsync(stringWriter, commanderGoals);
                json = stringWriter.ToString();
            }

            Assert.AreEqual(
                Regex.Replace(@"
{
    ""materials"": []
}
",
                    "\\s+",
                    string.Empty),
                json
            );
        }

        [TestMethod]
        public async Task TryingToWriteToNullThrowsException()
        {
            var exception = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _GoalsStoreService.WriteGoalsAsync(null, new CommanderGoalsData()));
            Assert.AreEqual(new ArgumentNullException("textWriter").Message, exception.Message);
        }

        [TestMethod]
        public async Task TryingToWriteToNullCommanderGoalsThrowsException()
        {
            ArgumentNullException exception;
            using (var stringWriter = new StringWriter())
                exception = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _GoalsStoreService.WriteGoalsAsync(stringWriter, null));
            Assert.AreEqual(new ArgumentNullException("commanderGoalsData").Message, exception.Message);
        }

        private void _AssertAreEqual(CommanderGoalsData expected, CommanderGoalsData actual)
        {
            Assert.AreEqual(expected.Materials.Count, actual.Materials.Count);
            Assert.IsTrue(
                expected
                    .Materials
                    .Select(
                        material => new
                        {
                            material.MaterialId,
                            material.Name,
                            material.Amount
                        }
                    )
                    .SequenceEqual(
                        actual
                            .Materials
                            .Select(
                                material => new
                                {
                                    material.MaterialId,
                                    material.Name,
                                    material.Amount
                                }
                            )
                    )
            );
        }
    }
}