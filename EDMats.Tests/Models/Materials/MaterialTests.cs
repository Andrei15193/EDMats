using System;
using System.Linq;
using System.Reflection;
using EDMats.Models.Materials;
using Xunit;

namespace EDMats.Tests.Models.Materials
{
    public class MaterialTests
    {
        [Fact]
        public void MaterialData()
        {
            var materialTypes = typeof(Material)
                .GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty)
                .Where(property => property.PropertyType == typeof(MaterialType))
                .Select(property => (MaterialType)property.GetValue(null));

            Assert.Equal(Material.All, Material.All.OrderBy(material => material.Name, StringComparer.OrdinalIgnoreCase));
            foreach (var materialType in materialTypes)
            {
                Assert.Same(materialType, typeof(Material).GetProperty(materialType.Name.Replace(" ", string.Empty)).GetValue(null));

                Assert.Equal(materialType.Categories, materialType.Categories.OrderBy(materialCategory => materialCategory.Name, StringComparer.OrdinalIgnoreCase));
                foreach (var materialCategory in materialType.Categories)
                {
                    Assert.Same(materialType, materialCategory.Type);
                    Assert.Same(materialCategory, (typeof(Material).GetProperty(materialCategory.Name.Replace(" ", string.Empty) + "Category") ?? typeof(Material).GetProperty(materialCategory.Name.Replace(" ", string.Empty))).GetValue(null));
                    Assert.Equal(materialCategory.Materials, materialCategory.Materials.OrderBy(material => material.Name, StringComparer.OrdinalIgnoreCase));
                    foreach (var material in materialCategory.Materials)
                    {
                        Assert.Same(materialType, material.Type);
                        Assert.Same(materialCategory, material.Category);
                        Assert.Same(
                            material,
                            typeof(Material)
                                .GetProperty(string.Join(
                                    string.Empty,
                                    material.Name.Split(' ').Select(part => part[0] + part.Substring(1).ToLowerInvariant()))
                                )
                                .GetValue(null));
                        Assert.Same(material, Material.FindById(material.Id));
                        Assert.True(Enum.IsDefined(typeof(MaterialGrade), material.Grade));
                        switch (material.Grade)
                        {
                            case MaterialGrade.VeryCommon:
                                Assert.Equal(300, material.Capacity);
                                break;

                            case MaterialGrade.Common:
                                Assert.Equal(250, material.Capacity);
                                break;

                            case MaterialGrade.Standard:
                                Assert.Equal(200, material.Capacity);
                                break;

                            case MaterialGrade.Rare:
                                Assert.Equal(150, material.Capacity);
                                break;


                            case MaterialGrade.VeryRare:
                                Assert.Equal(100, material.Capacity);
                                break;
                        }
                    }
                }
            }
        }
    }
}