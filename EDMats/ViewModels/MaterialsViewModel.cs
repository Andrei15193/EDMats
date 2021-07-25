using System;
using System.Collections.ObjectModel;
using System.Linq;
using EDMats.Data.Engineering;
using EDMats.Data.Materials;
using EDMats.Storage;

namespace EDMats.ViewModels
{
    public class MaterialsViewModel : ViewModel
    {
        private readonly IProfileStorageHandler _profileStorageHandler;
        private readonly ObservableCollection<MaterialQuantity> _materialRequirements;

        public MaterialsViewModel()
            : this(App.Resolve<IProfileStorageHandler>())
        {
        }

        public MaterialsViewModel(IProfileStorageHandler profileStorageHandler)
        {
            _profileStorageHandler = profileStorageHandler;
            _materialRequirements = new ObservableCollection<MaterialQuantity>();
            MaterialRequirements = new ReadOnlyObservableCollection<MaterialQuantity>(_materialRequirements);
        }

        public ReadOnlyObservableCollection<MaterialQuantity> MaterialRequirements { get; }

        public void Load()
        {
            var materialRequirements = _profileStorageHandler
                .LoadProfile("default")
                .Modules
                .SelectMany(module =>
                {
                    var matchedModule = Module.FindById(module.Key);
                    var blueprintGradeRequirements = matchedModule.Blueprints.SingleOrDefault(blueprint => blueprint.Id == module.Value.Blueprint?.Id)?.GradeRequirements ?? Enumerable.Empty<BlueprintGradeRequirements>();
                    var experimentalEffectRequirements = matchedModule.ExperimentalEffects.SingleOrDefault(experimentalEffect => experimentalEffect.Id == module.Value.ExperimentalEffect?.Id)?.Requirements ?? Enumerable.Empty<MaterialQuantity>();

                    return blueprintGradeRequirements
                        .SelectMany(gradeRequirement => Enumerable.Repeat(gradeRequirement.Requirements, _GetRepetitionsFor(gradeRequirement.Grade, module.Value.Blueprint) ?? 0))
                        .Concat(Enumerable.Repeat(experimentalEffectRequirements, module.Value.ExperimentalEffect?.Repetitions ?? 0))
                        .SelectMany(Enumerable.AsEnumerable);
                })
                .GroupBy(materialQuantity => materialQuantity.Material, materialQuantity => materialQuantity.Amount)
                .Select(grouping => new MaterialQuantity(grouping.Key, grouping.Sum()))
                .OrderBy(materialRequirement => materialRequirement.Material.Grade)
                .ThenBy(materialRequirement => materialRequirement.Material.Name, StringComparer.OrdinalIgnoreCase);

            _materialRequirements.Clear();
            foreach (var materialRequirement in materialRequirements)
                _materialRequirements.Add(materialRequirement);
        }

        private static int? _GetRepetitionsFor(BlueprintGrade blueprintGrade, StorageBlueprint storageBlueprint)
        {
            switch (blueprintGrade)
            {
                case BlueprintGrade.Grade1:
                    return storageBlueprint?.Grade1?.Repetitions;

                case BlueprintGrade.Grade2:
                    return storageBlueprint?.Grade2?.Repetitions;

                case BlueprintGrade.Grade3:
                    return storageBlueprint?.Grade3?.Repetitions;

                case BlueprintGrade.Grade4:
                    return storageBlueprint?.Grade4?.Repetitions;

                case BlueprintGrade.Grade5:
                    return storageBlueprint?.Grade5?.Repetitions;

                default:
                    return null;
            }
        }
    }
}