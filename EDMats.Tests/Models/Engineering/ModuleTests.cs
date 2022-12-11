using System;
using System.Collections.Generic;
using System.Linq;
using EDMats.Models.Engineering;
using Xunit;
using BindingFlags = System.Reflection.BindingFlags;

namespace EDMats.Tests.Models.Engineering
{
    public class ModuleTests
    {
        [Fact]
        public void ModuleData()
        {
            var moduleTypes = typeof(Module)
                .GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty)
                .Where(property => property.PropertyType == typeof(ModuleType))
                .Select(property => (ModuleType)property.GetValue(null));

            Assert.Equal(Module.All, Module.All.OrderBy(module => module.Name, StringComparer.OrdinalIgnoreCase));
            var moduleTypeIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var moduleType in moduleTypes)
            {
                Assert.False(string.IsNullOrWhiteSpace(moduleType.Id));
                Assert.True(moduleTypeIds.Add(moduleType.Id));
                Assert.Same(moduleType, typeof(Module).GetProperty(moduleType.Name.Replace(" ", string.Empty)).GetValue(null));

                Assert.Equal(moduleType.Modules, moduleType.Modules.OrderBy(module => module.Name, StringComparer.OrdinalIgnoreCase));
                foreach (var module in moduleType.Modules)
                {
                    Assert.Same(moduleType, module.Type);
                    Assert.Same(module, typeof(Module).GetProperty(module.Name.Replace("-", string.Empty).Replace(" ", string.Empty)).GetValue(null));
                    Assert.Same(module, Module.FindById(module.Id));

                    Assert.Equal(module.Blueprints, module.Blueprints.OrderBy(blueprint => blueprint.Name, StringComparer.OrdinalIgnoreCase));
                    var blueprintIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var blueprint in module.Blueprints)
                    {
                        Assert.Same(module, blueprint.Module);
                        Assert.False(string.IsNullOrWhiteSpace(blueprint.Id));
                        Assert.True(blueprintIds.Add(blueprint.Id));
                        Assert.False(string.IsNullOrWhiteSpace(blueprint.Name));

                        Assert.Equal(blueprint.GradeRequirements, blueprint.GradeRequirements.OrderBy(requirement => requirement.Grade));
                        foreach (var gradeRequirement in blueprint.GradeRequirements)
                        {
                            Assert.True(blueprint.IsPreEngineered || gradeRequirement.Requirements.All(requirement => requirement.Amount == 1));
                            Assert.Equal(gradeRequirement.Requirements, gradeRequirement.Requirements.OrderBy(requirement => requirement.Material.Name, StringComparer.OrdinalIgnoreCase).ToList());
                        }
                    }

                    Assert.Equal(module.ExperimentalEffects, module.ExperimentalEffects.OrderBy(experimentalEffect => experimentalEffect.Name, StringComparer.OrdinalIgnoreCase));
                    var experimentalEffectsIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var experimentalEffect in module.ExperimentalEffects)
                    {
                        Assert.Same(module, experimentalEffect.Module);
                        Assert.False(string.IsNullOrWhiteSpace(experimentalEffect.Id));
                        Assert.True(experimentalEffectsIds.Add(experimentalEffect.Id));
                        Assert.False(string.IsNullOrWhiteSpace(experimentalEffect.Name));
                        Assert.True(experimentalEffect.Requirements.All(requirement => requirement.Amount >= 1));
                        Assert.Equal(experimentalEffect.Requirements, experimentalEffect.Requirements.OrderByDescending(requirement => requirement.Amount));
                    }
                }
            }
        }
    }
}