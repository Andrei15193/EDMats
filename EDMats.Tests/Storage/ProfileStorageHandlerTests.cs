using EDMats.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace EDMats.Tests.Storage
{
    [TestClass]
    public class ProfileStorageHandlerTests
    {
        [TestMethod]
        public void LoadProfile_WhenItDoesNotExists_ReturnsDefaultProfile()
        {
            var profileStorageHandler = new ProfileStorageHandler(new InMemoryStorageHandler());

            var profile = profileStorageHandler.LoadProfile("Profile");

            Assert.AreEqual("profile", profile.Name);
            Assert.IsNotNull(profile.Commander);
            Assert.IsNull(profile.Commander.Name);
            Assert.IsNull(profile.Commander.Picture);
            Assert.IsNull(profile.Commander.JournalsDirectoryPath);
            Assert.IsNotNull(profile.Modules);
            Assert.AreEqual(0, profile.Modules.Count);
        }

        [TestMethod]
        public void SaveProfile_WhenItDoesNotExists_StoresProfileAsJson()
        {
            var storageHandler = new InMemoryStorageHandler();
            var profileStorageHandler = new ProfileStorageHandler(storageHandler);

            profileStorageHandler.SaveProfile(new StorageProfile("Profile")
            {
                Commander =
                {
                    Name = "commander name"
                },
                Modules =
                {
                    {
                        "module id",
                        new StorageModule
                        {
                            Blueprint = new StorageBlueprint
                            {
                                Id = "blueprint id",
                                Grade1 = new StorageBlueprintGrade
                                {
                                    Repetitions = 3
                                },
                                Grade2 = new StorageBlueprintGrade
                                {
                                    Repetitions = 10
                                },
                                Grade4 = new StorageBlueprintGrade
                                {
                                    Repetitions = 1
                                }
                            },
                            ExperimentalEffect = new StorageExperimentalEffect
                            {
                                Id = "experimental effect id",
                                Repetitions = 2
                            }
                        }
                    }
                }
            });

            string profileJson;
            using (var textReader = storageHandler.GetTextReader("profile"))
                profileJson = textReader.ReadToEnd();

            Assert.AreEqual(
                _FormatJson(
                @"{
                    ""commander"": {
                        ""name"": ""commander name""
                    },
                    ""modules"": {
                        ""module id"": {
                            ""blueprint"": {
                                ""id"": ""blueprint id"",
                                ""grade1"": {
                                    ""repetitions"": 3
                                },
                                ""grade2"": {
                                    ""repetitions"": 10
                                },
                                ""grade4"": {
                                    ""repetitions"": 1
                                },
                            },
                            ""experimentalEffect"": {
                                ""id"": ""experimental effect id"",
                                ""repetitions"": 2
                            }
                        }
                    }
                }"),
                _FormatJson(profileJson)
            );
        }

        [TestMethod]
        public void LoadProfile_WhenItExists_LoadsStoredProfile()
        {
            var profileStorageHandler = new ProfileStorageHandler(new InMemoryStorageHandler());
            profileStorageHandler.SaveProfile(new StorageProfile("profile")
            {
                Commander =
                {
                    Name = "commander name"
                },
                Modules =
                {
                    {
                        "module id",
                        new StorageModule
                        {
                            Blueprint = new StorageBlueprint
                            {
                                Id = "blueprint id",
                                Grade1 = new StorageBlueprintGrade
                                {
                                    Repetitions = 3
                                },
                                Grade2 = new StorageBlueprintGrade
                                {
                                    Repetitions = 10
                                },
                                Grade4 = new StorageBlueprintGrade
                                {
                                    Repetitions = 1
                                }
                            },
                            ExperimentalEffect = new StorageExperimentalEffect
                            {
                                Id = "experimental effect id",
                                Repetitions = 2
                            }
                        }
                    }
                }
            });

            var profile = profileStorageHandler.LoadProfile("Profile");

            Assert.AreEqual("profile", profile.Name);
            Assert.IsNotNull(profile.Commander);
            Assert.AreEqual("commander name", profile.Commander.Name);
            Assert.IsNull(profile.Commander.Picture);
            Assert.IsNotNull(profile.Modules);
            Assert.AreEqual(1, profile.Modules.Count);
            Assert.IsNotNull(profile.Modules["module id"].Blueprint);
            Assert.IsNotNull(profile.Modules["module id"].Blueprint.Grade1);
            Assert.AreEqual(3, profile.Modules["module id"].Blueprint.Grade1.Repetitions);
            Assert.IsNotNull(profile.Modules["module id"].Blueprint.Grade2);
            Assert.AreEqual(10, profile.Modules["module id"].Blueprint.Grade2.Repetitions);
            Assert.IsNull(profile.Modules["module id"].Blueprint.Grade3);
            Assert.IsNotNull(profile.Modules["module id"].Blueprint.Grade4);
            Assert.AreEqual(1, profile.Modules["module id"].Blueprint.Grade4.Repetitions);
            Assert.IsNull(profile.Modules["module id"].Blueprint.Grade5);
            Assert.IsNotNull(profile.Modules["module id"].ExperimentalEffect);
            Assert.AreEqual("experimental effect id", profile.Modules["module id"].ExperimentalEffect.Id);
            Assert.AreEqual(2, profile.Modules["module id"].ExperimentalEffect.Repetitions);
        }

        private static string _FormatJson(string json)
            => JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.Indented);
    }
}