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
                new Type
                {
                    Name = "Raw",
                    Categories = new[]
                    {
                        new Category
                        {
                            Name = "Raw Material Category 1",
                            Materials = new[]{ "Carbon", "Vanadium", "Niobium", "Yttrium" }
                        },
                        new Category
                        {
                            Name = "Raw Material Category 2",
                            Materials = new[]{ "Phosphorus", "Chromium", "Molybdenum", "Technetium" }
                        },
                        new Category
                        {
                            Name = "Raw Material Category 3",
                            Materials = new[]{ "Sulphur", "Manganese", "Cadmium", "Ruthenium" }
                        },
                        new Category
                        {
                            Name = "Raw Material Category 4",
                            Materials = new[]{ "Iron", "Zinc", "Tin", "Selenium" }
                        },
                        new Category
                        {
                            Name = "Raw Material Category 5",
                            Materials = new[]{ "Nickel", "Germanium", "Tungsten", "Tellurium" }
                        },
                        new Category
                        {
                            Name = "Raw Material Category 6",
                            Materials = new[]{ "Rhenium", "Arsenic", "Mercury", "Polonium" }
                        },
                        new Category
                        {
                            Name  ="Raw Material Category 7",
                            Materials = new[]{ "Lead", "Zirconium", "Boron", "Antimony" }
                        }
                    }
                };

            _AssertMaterialType(expected, Materials.Raw);
            _AssertReflection(Materials.Raw);
            _AssertFindById(Materials.Raw);
        }

        [TestMethod]
        public void ManufacturedMaterials()
        {
            var expected =
                new Type
                {
                    Name = "Manufactured",
                    Categories = new[]
                    {
                        new Category
                        {
                            Name = "Chemical",
                            Materials = new[]{ "Chemical Storage Units", "Chemical Processors", "Chemical Distillery", "Chemical Manipulators", "Pharmaceutical Isolators" }
                        },
                        new Category
                        {
                            Name = "Thermic",
                            Materials = new[]{ "Tempered Alloys", "Heat Resistant Ceramics", "Precipitated Alloys", "Thermic Alloys", "Military Grade Alloys" }
                        },
                        new Category
                        {
                            Name = "Heat",
                            Materials = new[]{ "Heat Conduction Wiring", "Heat Dispersion Plate", "Heat Exchangers", "Heat Vanes", "Proto Heat Radiators" }
                        },
                        new Category
                        {
                            Name = "Conductive",
                            Materials = new[]{ "Basic Conductors", "Conductive Components", "Conductive Ceramics", "Conductive Polymers", "Biotech Conductors" }
                        },
                        new Category
                        {
                            Name = "Mechanical Components",
                            Materials = new[]{ "Mechanical Scrap", "Mechanical Equipment", "Mechanical Components", "Configurable Components", "Improvised Components" }
                        },
                        new Category
                        {
                            Name = "Capacitors",
                            Materials = new[]{ "Grid Resistors", "Hybrid Capacitors", "Electrochemical Arrays", "Polymer Capacitors", "Military Supercapacitors" }
                        },
                        new Category
                        {
                            Name = "Shielding",
                            Materials = new[]{ "Worn Shield Emitters", "Shield Emitters", "Shielding Sensors", "Compound Shielding", "Imperial Shielding" }
                        },
                        new Category
                        {
                            Name = "Composite",
                            Materials = new[]{ "Compact Composites", "Filament Composites", "High Density Composites", "Proprietary Composites", "Core Dynamics Composites" }
                        },
                        new Category
                        {
                            Name = "Crystals",
                            Materials = new[]{ "Crystal Shards", "Flawed Focus Crystals", "Focus Crystals", "Refined Focus Crystals", "Exquisite Focus Crystals" }
                        },
                        new Category
                        {
                            Name = "Alloys",
                            Materials = new[]{ "Salvaged Alloys", "Galvanising Alloys", "Phase Alloys", "Proto Light Alloys", "Proto Radiolic Alloys" }
                        }
                    }
                };

            _AssertMaterialType(expected, Materials.Manufactured);
            _AssertReflection(Materials.Manufactured);
            _AssertFindById(Materials.Manufactured);
        }

        [TestMethod]
        public void EncodedMaterials()
        {
            var expected =
                new Type
                {
                    Name = "Encoded",
                    Categories = new[]
                    {
                        new Category
                        {
                            Name = "Emission Data",
                            Materials = new[]{ "Exceptional Scrambled Emission Data", "Irregular Emission Data", "Unexpected Emission Data", "Decoded Emission Data", "Abnormal Compact Emissions Data" }
                        },
                        new Category
                        {
                            Name = "Wake Scans",
                            Materials = new[]{ "Atypical Disrupted Wake Echoes", "Anomalous FSD Telemetry", "Strange Wake Solutions", "Eccentric Hyperspace Trajectories", "Datamined Wake Exceptions" }
                        },
                        new Category
                        {
                            Name = "Shield Data",
                            Materials = new[]{ "Distorted Shield Cycle Recordings", "Inconsistent Shield Soak Analysis", "Untypical Shield Scans", "Aberrant Shield Pattern Analysis", "Peculiar Shield Frequency Data" }
                        },
                        new Category
                        {
                            Name = "Encryption Files",
                            Materials = new[]{ "Unusual Encrypted Files", "Tagged Encryption Codes", "Open Symmetric Keys", "Atypical Encryption Archives", "Adaptive Encryptors Capture" }
                        },
                        new Category
                        {
                            Name = "Data Archives",
                            Materials = new[]{ "Anomalous Bulk Scan Data", "Unidentified Scan Archives", "Classified Scan Databanks", "Divergend Scan Data", "Classified Scan Fragment" }
                        },
                        new Category
                        {
                            Name = "Encoded Firmware",
                            Materials = new[]{ "Specialised Legacy Firmware", "Modified Consumer Firmware", "Cracked Industrial Firmware", "Security Firmware Patch", "Modified Embedded Firmware" }
                        }
                    }
                };

            _AssertMaterialType(expected, Materials.Encoded);
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

        private static void _AssertMaterialType(Type expected, MaterialType actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Categories.Count, actual.Categories.Count);
            foreach (var pair in expected
                .Categories
                .Zip(
                    actual.Categories,
                    (expectedCategory, actualCategory) =>
                        new
                        {
                            ExpectedCategory = expectedCategory,
                            ActualCategory = actualCategory
                        }
                    )
                )
                _AssertCategory(pair.ExpectedCategory, actual, pair.ActualCategory);
        }

        private static void _AssertCategory(Category expected, MaterialType expectedMaterialCategory, MaterialCategory actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreSame(expectedMaterialCategory, actual.Type);
            Assert.AreEqual(expected.Materials.Count, actual.Materials.Count);

            for (var index = 0; index < expected.Materials.Count; index++)
            {
                var grade = (MaterialGrade)(index + 1);
                var maximumCapacity =  300 - 50 * index;
                Assert.AreEqual(expected.Materials[index], actual.Materials[index].Name);
                Assert.AreEqual(grade, actual.Materials[index].Grade);
                Assert.AreSame(actual, actual.Materials[index].Category);
                Assert.AreEqual(maximumCapacity, actual.Materials[index].MaximumCapacity);
                Assert.AreSame(actual.Type, actual.Materials[index].Type);
            }
        }

        private static void _AssertReflection(MaterialType expected)
        {
            var actual = typeof(Materials)
                .GetProperty(
                    expected.Name,
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.IgnoreCase
                )
                .GetValue(null);
            Assert.AreSame(expected, actual);

            foreach (var category in expected.Categories)
                _AssertReflection(category);
        }

        private static void _AssertReflection(MaterialCategory expected)
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

        private static void _AssertFindById(MaterialType expected)
        {
            foreach (var expectedMaterial in expected.Categories.SelectMany(category => category.Materials))
            {
                var actualMaterial = Materials.FindById(expectedMaterial.Id.ToUpperInvariant());
                Assert.AreSame(expectedMaterial, actualMaterial);
            }
        }

        private sealed class Type
        {
            public string Name { get; set; }

            public IReadOnlyList<Category> Categories { get; set; }
        }

        private sealed class Category
        {
            public string Name { get; set; }

            public IReadOnlyList<string> Materials { get; set; }
        }
    }
}