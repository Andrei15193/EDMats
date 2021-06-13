using System;
using System.Linq;
using System.Reflection;
using EDMats.Data.Materials;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Data.Materials
{
    [TestClass]
    public class MaterialTests
    {
        [TestMethod]
        public void MaterialData()
        {
            var materialTypes = typeof(Material)
                .GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty)
                .Where(property => property.PropertyType == typeof(MaterialType))
                .Select(property => (MaterialType)property.GetValue(null));

            CollectionAssert.AreEquivalent(
                Material
                    .All
                    .ToList(),
                Material
                    .All
                    .OrderBy(material => material.Name, StringComparer.OrdinalIgnoreCase)
                    .ToList()
            );
            foreach (var materialType in materialTypes)
            {
                Assert.AreSame(materialType, typeof(Material).GetProperty(materialType.Name.Replace(" ", string.Empty)).GetValue(null));

                CollectionAssert.AreEquivalent(
                    materialType
                        .Categories
                        .ToList(),
                    materialType
                        .Categories
                        .OrderBy(materialCategory => materialCategory.Name, StringComparer.OrdinalIgnoreCase)
                        .ToList()
                );
                foreach (var materialCategory in materialType.Categories)
                {
                    Assert.AreSame(materialType, materialCategory.Type);
                    Assert.AreSame(
                        materialCategory,
                        (typeof(Material).GetProperty(materialCategory.Name.Replace(" ", string.Empty) + "Category") ?? typeof(Material).GetProperty(materialCategory.Name.Replace(" ", string.Empty)))
                            .GetValue(null)
                    );
                    CollectionAssert.AreEquivalent(
                        materialCategory
                            .Materials
                            .ToList(),
                        materialCategory
                            .Materials
                            .OrderBy(material => material.Name, StringComparer.OrdinalIgnoreCase)
                            .ToList()
                    );
                    foreach (var material in materialCategory.Materials)
                    {
                        Assert.AreSame(materialType, material.Type);
                        Assert.AreSame(materialCategory, material.Category);
                        Assert.AreSame(
                            material,
                            typeof(Material)
                                .GetProperty(string.Join(
                                    string.Empty,
                                    material.Name.Split(' ').Select(part => part[0] + part.Substring(1).ToLowerInvariant()))
                                )
                                .GetValue(null));
                        Assert.AreSame(material, Material.FindById(material.Id));
                        Assert.IsTrue(Enum.IsDefined(typeof(MaterialGrade), material.Grade));
                        switch (material.Grade)
                        {
                            case MaterialGrade.VeryCommon:
                                Assert.AreEqual(300, material.Capacity);
                                break;

                            case MaterialGrade.Common:
                                Assert.AreEqual(250, material.Capacity);
                                break;

                            case MaterialGrade.Standard:
                                Assert.AreEqual(200, material.Capacity);
                                break;

                            case MaterialGrade.Rare:
                                Assert.AreEqual(150, material.Capacity);
                                break;


                            case MaterialGrade.VeryRare:
                                Assert.AreEqual(100, material.Capacity);
                                break;
                        }
                    }
                }
            }
        }
    }
}