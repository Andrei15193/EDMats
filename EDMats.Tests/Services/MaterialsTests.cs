using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EDMats.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class MaterialsTests
    {
        [TestMethod]
        public void RawMaterials()
        {
            var expected =
                new Category
                {
                    Name = "Raw",
                    Subcategories = new[]
                    {
                        new Subcategory
                        {
                            Name = "Raw Material Category 1",
                            Materials = new[]{ "Carbon", "Vanadium", "Niobium", "Yttrium" }
                        },
                        new Subcategory
                        {
                            Name = "Raw Material Category 2",
                            Materials = new[]{ "Phosphorus", "Chromium", "Molybdenum", "Technetium" }
                        },
                        new Subcategory
                        {
                            Name = "Raw Material Category 3",
                            Materials = new[]{ "Sulphur", "Manganese", "Cadmium", "Ruthenium" }
                        },
                        new Subcategory
                        {
                            Name = "Raw Material Category 4",
                            Materials = new[]{ "Iron", "Zinc", "Tin", "Selenium" }
                        },
                        new Subcategory
                        {
                            Name = "Raw Material Category 5",
                            Materials = new[]{ "Nickel", "Germanium", "Tungsten", "Tellurium" }
                        },
                        new Subcategory
                        {
                            Name = "Raw Material Category 6",
                            Materials = new[]{ "Rhenium", "Arsenic", "Mercury", "Polonium" }
                        },
                        new Subcategory
                        {
                            Name  ="Raw Material Category 7",
                            Materials = new[]{ "Lead", "Zirconium", "Boron", "Antimony" }
                        }
                    }
                };

            _AssertCategory(expected, Materials.Raw);
            _AssertReflection(Materials.Raw);
            _AssertFindById(Materials.Raw);
        }

        [TestMethod]
        public void ManufacturedMaterials()
        {
            var expected =
                new Category
                {
                    Name = "Manufactured",
                    Subcategories = new[]
                    {
                        new Subcategory
                        {
                            Name = "Chemical",
                            Materials = new[]{ "Chemical Storage Units", "Chemical Processors", "Chemical Distillery", "Chemical Manipulators", "Pharmaceutical Isolators" }
                        },
                        new Subcategory
                        {
                            Name = "Thermic",
                            Materials = new[]{ "Tempered Alloys", "Heat Resistant Ceramics", "Precipitated Alloys", "Thermic Alloys", "Military Grade Alloys" }
                        },
                        new Subcategory
                        {
                            Name = "Heat",
                            Materials = new[]{ "Heat Conduction Wiring", "Heat Dispersion Plate", "Heat Exchangers", "Heat Vanes", "Proto Heat Radiators" }
                        },
                        new Subcategory
                        {
                            Name = "Conductive",
                            Materials = new[]{ "Basic Conductors", "Conductive Components", "Conductive Ceramics", "Conductive Polymers", "Biotech Conductors" }
                        },
                        new Subcategory
                        {
                            Name = "Mechanical Components",
                            Materials = new[]{ "Mechanical Scrap", "Mechanical Equipment", "Mechanical Components", "Configurable Components", "Improvised Components" }
                        },
                        new Subcategory
                        {
                            Name = "Capacitors",
                            Materials = new[]{ "Grid Resistors", "Hybrid Capacitors", "Electrochemical Arrays", "Polymer Capacitors", "Military Supercapacitors" }
                        },
                        new Subcategory
                        {
                            Name = "Shielding",
                            Materials = new[]{ "Worn Shield Emitters", "Shield Emitters", "Shielding Sensors", "Compound Shielding", "Imperial Shielding" }
                        },
                        new Subcategory
                        {
                            Name = "Composite",
                            Materials = new[]{ "Compact Composites", "Filament Composites", "High Density Composites", "Proprietary Composites", "Core Dynamics Composites" }
                        },
                        new Subcategory
                        {
                            Name = "Crystals",
                            Materials = new[]{ "Crystal Shards", "Flawed Focus Crystals", "Focus Crystals", "Refined Focus Crystals", "Exquisite Focus Crystals" }
                        },
                        new Subcategory
                        {
                            Name = "Alloys",
                            Materials = new[]{ "Salvaged Alloys", "Galvanising Alloys", "Phase Alloys", "Proto Light Alloys", "Proto Radiolic Alloys" }
                        }
                    }
                };

            _AssertCategory(expected, Materials.Manufactured);
            _AssertReflection(Materials.Manufactured);
            _AssertFindById(Materials.Manufactured);
        }

        [TestMethod]
        public void EncodedMaterials()
        {
            var expected =
                new Category
                {
                    Name = "Encoded",
                    Subcategories = new[]
                    {
                        new Subcategory
                        {
                            Name = "Emission Data",
                            Materials = new[]{ "Exceptional Scrambled Emission Data", "Irregular Emission Data", "Unexpected Emission Data", "Decoded Emission Data", "Abnormal Compact Emissions Data" }
                        },
                        new Subcategory
                        {
                            Name = "Wake Scans",
                            Materials = new[]{ "Atypical Disrupted Wake Echoes", "Anomalous FSD Telemetry", "Strange Wake Solutions", "Eccentric Hyperspace Trajectories", "Datamined Wake Exceptions" }
                        },
                        new Subcategory
                        {
                            Name = "Shield Data",
                            Materials = new[]{ "Distorted Shield Cycle Recordings", "Inconsistent Shield Soak Analysis", "Untypical Shield Scans", "Aberrant Shield Pattern Analysis", "Peculiar Shield Frequency Data" }
                        },
                        new Subcategory
                        {
                            Name = "Encryption Files",
                            Materials = new[]{ "Unusual Encrypted Files", "Tagged Encryption Codes", "Open Symmetric Keys", "Atypical Encryption Archives", "Adaptive Encryptors Capture" }
                        },
                        new Subcategory
                        {
                            Name = "Data Archives",
                            Materials = new[]{ "Anomalous Bulk Scan Data", "Unidentified Scan Archives", "Classified Scan Databanks", "Divergend Scan Data", "Classified Scan Fragment" }
                        },
                        new Subcategory
                        {
                            Name = "Encoded Firmware",
                            Materials = new[]{ "Specialised Legacy Firmware", "Modified Consumer Firmware", "Cracked Industrial Firmware", "Security Firmware Patch", "Modified Embedded Firmware" }
                        }
                    }
                };

            _AssertCategory(expected, Materials.Encoded);
            _AssertReflection(Materials.Encoded);
            _AssertFindById(Materials.Encoded);
        }

        [TestMethod]
        public void GettingMaterialByNullIdThrowsException()
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => Materials.FindById(null));
            Assert.AreEqual(new ArgumentNullException("id").Message, exception.Message);
        }

        [TestMethod]
        public void GettingMaterialByIdThatDoesNotExistThrowsException()
        {
            var invalidId = "does not exist";
            var exception = Assert.ThrowsException<ArgumentException>(() => Materials.FindById(invalidId));
            Assert.AreEqual(new ArgumentException($"Material with id '{invalidId}' does not exist.", "id").Message, exception.Message);
        }

        private static void _AssertCategory(Category expected, MaterialCategory actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Subcategories.Count, actual.Subcategories.Count);
            foreach (var pair in expected
                .Subcategories
                .Zip(
                    actual.Subcategories,
                    (expectedSubcategory, actualSubcategory) =>
                        new
                        {
                            ExpectedSubcategory = expectedSubcategory,
                            ActualSubcategory = actualSubcategory
                        }
                    )
                )
                _AssertSubcategory(pair.ExpectedSubcategory, actual, pair.ActualSubcategory);
        }

        private static void _AssertSubcategory(Subcategory expected, MaterialCategory expectedMaterialCategory, MaterialSubcategory actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreSame(expectedMaterialCategory, actual.Category);
            Assert.AreEqual(expected.Materials.Count, actual.Materials.Count);

            for (var index = 0; index < expected.Materials.Count; index++)
            {
                var grade = (MaterialGrade)(index + 1);
                Assert.AreEqual(expected.Materials[index], actual.Materials[index].Name);
                Assert.AreEqual(grade, actual.Materials[index].Grade);
                Assert.AreSame(actual, actual.Materials[index].Subcategory);
            }
        }

        private static void _AssertReflection(MaterialCategory expected)
        {
            var actual = typeof(Materials)
                .GetProperty(
                    expected.Name,
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.IgnoreCase
                )
                .GetValue(null);
            Assert.AreSame(expected, actual);

            foreach (var subcategory in expected.Subcategories)
                _AssertReflection(subcategory);
        }

        private static void _AssertReflection(MaterialSubcategory expected)
        {
            var actual = typeof(Materials)
                .GetProperty(
                    expected
                        .Name
                        .Replace(" Material ", "Materials")
                        .Replace(" ", string.Empty)
                        + (expected.Materials.Any(material => expected.Name == material.Name) ? "Category" : string.Empty),
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.IgnoreCase
                )
                .GetValue(null);
            Assert.AreSame(expected, actual);

            foreach (var material in expected.Materials)
                _AssertReflection(material);
        }

        private static void _AssertReflection(Material expected)
        {
            var actual = typeof(Materials)
                .GetProperty(
                    expected.Name.Replace(" ", string.Empty),
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.IgnoreCase
                )
                .GetValue(null);
            Assert.AreSame(expected, actual);
        }

        private static void _AssertFindById(MaterialCategory expected)
        {
            foreach (var expectedMaterial in expected.Subcategories.SelectMany(subcategory => subcategory.Materials))
            {
                var actualMaterial = Materials.FindById(expectedMaterial.Id.ToUpperInvariant());
                Assert.AreSame(expectedMaterial, actualMaterial);
            }
        }

        private sealed class Category
        {
            public string Name { get; set; }

            public IReadOnlyList<Subcategory> Subcategories { get; set; }
        }

        private sealed class Subcategory
        {
            public string Name { get; set; }

            public IReadOnlyList<string> Materials { get; set; }
        }
    }
}