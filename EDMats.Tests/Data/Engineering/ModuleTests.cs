using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EDMats.Data.Engineering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Module = EDMats.Data.Engineering.Module;

namespace EDMats.Tests.Data.Engineering
{
    [TestClass]
    public class ModuleTests
    {
        [TestMethod]
        public void ModuleData()
        {
            var moduleTypes = typeof(Module)
                .GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty)
                .Where(property => property.PropertyType == typeof(ModuleType))
                .Select(property => (ModuleType)property.GetValue(null));

            CollectionAssert.AreEquivalent(
                Module
                    .All
                    .ToList(),
                Module
                    .All
                    .OrderBy(module => module.Name, StringComparer.OrdinalIgnoreCase)
                    .ToList()
            );
            var moduleTypeIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var moduleType in moduleTypes)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(moduleType.Id));
                Assert.IsTrue(moduleTypeIds.Add(moduleType.Id));
                Assert.AreSame(moduleType, typeof(Module).GetProperty(moduleType.Name.Replace(" ", string.Empty)).GetValue(null));

                CollectionAssert.AreEquivalent(
                    moduleType
                        .Modules
                        .ToList(),
                    moduleType
                        .Modules
                        .OrderBy(module => module.Name, StringComparer.OrdinalIgnoreCase)
                        .ToList()
                );
                foreach (var module in moduleType.Modules)
                {
                    Assert.AreSame(moduleType, module.Type);
                    Assert.AreSame(module, typeof(Module).GetProperty(module.Name.Replace("-", string.Empty).Replace(" ", string.Empty)).GetValue(null));
                    Assert.AreSame(module, Module.FindById(module.Id));

                    CollectionAssert.AreEquivalent(
                        module
                            .Blueprints
                            .ToList(),
                        module
                            .Blueprints
                            .OrderBy(blueprint => blueprint.Name, StringComparer.OrdinalIgnoreCase)
                            .ToList()
                    );
                    var blueprintIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var blueprint in module.Blueprints)
                    {
                        Assert.AreSame(module, blueprint.Module);
                        Assert.IsFalse(string.IsNullOrWhiteSpace(blueprint.Id));
                        Assert.IsTrue(blueprintIds.Add(blueprint.Id));
                        Assert.IsFalse(string.IsNullOrWhiteSpace(blueprint.Name));

                        CollectionAssert.AreEquivalent(
                            blueprint
                                .GradeRequirements
                                .ToList(),
                            blueprint
                                .GradeRequirements
                                .OrderBy(requirement => requirement.Grade)
                                .ToList()
                        );
                        foreach (var gradeRequirement in blueprint.GradeRequirements)
                        {
                            Assert.IsTrue(gradeRequirement.Requirements.All(requirement => requirement.Amount == 1));
                            CollectionAssert.AreEquivalent(
                                gradeRequirement
                                    .Requirements
                                    .ToList(),
                                gradeRequirement
                                    .Requirements
                                    .OrderBy(requirement => requirement.Material.Name, StringComparer.OrdinalIgnoreCase)
                                    .ToList()
                            );
                        }
                    }

                    CollectionAssert.AreEquivalent(
                        module
                            .ExperimentalEffects
                            .ToList(),
                        module
                            .ExperimentalEffects
                            .OrderBy(experimentalEffect => experimentalEffect.Name, StringComparer.OrdinalIgnoreCase)
                            .ToList()
                    );
                    var experimentalEffectsIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var experimentalEffect in module.ExperimentalEffects)
                    {
                        Assert.AreSame(module, experimentalEffect.Module);
                        Assert.IsFalse(string.IsNullOrWhiteSpace(experimentalEffect.Id));
                        Assert.IsTrue(experimentalEffectsIds.Add(experimentalEffect.Id));
                        Assert.IsFalse(string.IsNullOrWhiteSpace(experimentalEffect.Name));
                        Assert.IsTrue(experimentalEffect.Requirements.All(requirement => requirement.Amount >= 1));
                        CollectionAssert.AreEquivalent(
                            experimentalEffect
                                .Requirements
                                .ToList(),
                            experimentalEffect
                                .Requirements
                                .OrderByDescending(requirement => requirement.Amount)
                                .ThenBy(requirement => requirement.Material.Name, StringComparer.OrdinalIgnoreCase)
                                .ToList()
                        );
                    }
                }
            }
        }
    }
}