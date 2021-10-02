using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using EDMats.Journals;
using EDMats.Models.Engineering;
using EDMats.Models.Materials;
using EDMats.Storage;
using EDMats.Trading;

namespace EDMats.ViewModels
{
    public class SolutionSearchViewModel : ViewModel
    {
        private Module _selectedModule;
        private Blueprint _selectedBlueprint;
        private IEnumerable<BlueprintRequirementRepetitionsViewModel> _blueprintGradeRequirements = Enumerable.Empty<BlueprintRequirementRepetitionsViewModel>();
        private ExperimentalEffect _selectedExperimentalEffect;
        private int _experimentalEffectRepetitions;
        private TradeSolution _rawMaterialsTradeSolution;
        private TradeSolution _encodedMaterialsTradeSolution;
        private TradeSolution _manufacturedMaterialsTradeSolution;
        private readonly ICommanderProfileStorageHandler _commanderProfileStorageHandler;
        private readonly JournalReader _journalReader;
        private readonly ITradeSolutionService _tradeSolutionService;

        public SolutionSearchViewModel()
            : this(App.Resolve<ICommanderProfileStorageHandler>(), App.Resolve<JournalReader>(), App.Resolve<ITradeSolutionService>())
        {
        }

        public SolutionSearchViewModel(ICommanderProfileStorageHandler commanderProfileStorageHandler, JournalReader journalReader, ITradeSolutionService tradeSolutionService)
            => (_commanderProfileStorageHandler, _journalReader, _tradeSolutionService) = (commanderProfileStorageHandler, journalReader, tradeSolutionService);

        public TradeSolution RawMaterialsTradeSolution
        {
            get => _rawMaterialsTradeSolution;
            private set
            {
                if (_rawMaterialsTradeSolution != value)
                {
                    _rawMaterialsTradeSolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TradeSolution EncodedMaterialsTradeSolution
        {
            get => _encodedMaterialsTradeSolution;
            private set
            {
                if (_encodedMaterialsTradeSolution != value)
                {
                    _encodedMaterialsTradeSolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TradeSolution ManufacturedMaterialsTradeSolution
        {
            get => _manufacturedMaterialsTradeSolution;
            private set
            {
                if (_manufacturedMaterialsTradeSolution != value)
                {
                    _manufacturedMaterialsTradeSolution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Module SelectedModule
        {
            get => _selectedModule;
            set
            {
                if (_selectedModule != value)
                {
                    _selectedModule = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Blueprint SelectedBlueprint
        {
            get => _selectedBlueprint;
            set
            {
                if (_selectedBlueprint != value)
                {
                    _selectedBlueprint = value;
                    NotifyPropertyChanged();
                    foreach (var blueprintGradeRequirement in BlueprintGradeRequirements)
                        blueprintGradeRequirement.PropertyChanged -= _BlueprintGradeRequirementChanged;
                    BlueprintGradeRequirements = _selectedBlueprint?.GradeRequirements.Select(gradeRequirement => new BlueprintRequirementRepetitionsViewModel(gradeRequirement)).ToList();
                    foreach (var blueprintGradeRequirement in BlueprintGradeRequirements)
                        blueprintGradeRequirement.PropertyChanged += _BlueprintGradeRequirementChanged;
                    _ResetTradeSolutions();
                }
            }
        }

        public IEnumerable<BlueprintRequirementRepetitionsViewModel> BlueprintGradeRequirements
        {
            get => _blueprintGradeRequirements;
            private set
            {
                if (_blueprintGradeRequirements != value)
                {
                    _blueprintGradeRequirements = value ?? Enumerable.Empty<BlueprintRequirementRepetitionsViewModel>();
                    NotifyPropertyChanged();
                }
            }
        }

        public ExperimentalEffect SelectedExperimentalEffect
        {
            get => _selectedExperimentalEffect;
            set
            {
                if (_selectedExperimentalEffect != value)
                {
                    _selectedExperimentalEffect = value;
                    NotifyPropertyChanged();
                    ExperimentalEffectRepetitions = 8;
                    _ResetTradeSolutions();
                }
            }
        }

        public int ExperimentalEffectRepetitions
        {
            get => _experimentalEffectRepetitions;
            set
            {
                if (_experimentalEffectRepetitions != value)
                {
                    _experimentalEffectRepetitions = value;
                    NotifyPropertyChanged();
                    _ResetTradeSolutions();
                }
            }
        }

        public void SearchTradeSolutions()
        {
            if (SelectedBlueprint is object || SelectedExperimentalEffect is object)
            {
                var commanderProfile = _commanderProfileStorageHandler.Load();
                var commanderInfo = _GetCommanderInfo(commanderProfile.JournalsDirectoryPath);
                var allowedTrades = AllowedTrade.All;

                var commanderMaterialsByType = commanderInfo.Materials.ToLookup(materialQuantity => materialQuantity.Material.Type);
                var requiredMaterialsByType = (
                    from materialQuantity in (
                            from gradeRequirement in BlueprintGradeRequirements ?? Enumerable.Empty<BlueprintRequirementRepetitionsViewModel>()
                            from requiredMaterialQuantity in gradeRequirement.GradeRequirements.Requirements
                            select new MaterialQuantity(requiredMaterialQuantity.Material, gradeRequirement.Repetitions * requiredMaterialQuantity.Amount)
                        ).Concat(
                            from requiredMaterialQuantity in SelectedExperimentalEffect?.Requirements ?? Enumerable.Empty<MaterialQuantity>()
                            select new MaterialQuantity(requiredMaterialQuantity.Material, ExperimentalEffectRepetitions * requiredMaterialQuantity.Amount)
                        )
                    group materialQuantity.Amount by materialQuantity.Material into amountsByMaterial
                    select new MaterialQuantity(amountsByMaterial.Key, amountsByMaterial.Sum())
                ).ToLookup(materialQuantity => materialQuantity.Material.Type);

                if (RawMaterialsTradeSolution is null)
                    RawMaterialsTradeSolution = _TryFindTradeSoluton(Material.Raw);
                if (EncodedMaterialsTradeSolution is null)
                    EncodedMaterialsTradeSolution = _TryFindTradeSoluton(Material.Encoded);
                if (ManufacturedMaterialsTradeSolution is null)
                    ManufacturedMaterialsTradeSolution = _TryFindTradeSoluton(Material.Manufactured);

                TradeSolution _TryFindTradeSoluton(MaterialType materialType)
                {
                    if (!requiredMaterialsByType.Contains(materialType))
                        return new TradeSolution(Enumerable.Empty<TradeEntry>());
                    else if (commanderMaterialsByType.Contains(materialType))
                        return _tradeSolutionService.TryFindSolution(requiredMaterialsByType[materialType], commanderMaterialsByType[materialType], allowedTrades);
                    else
                        return null;
                }
            }
        }


        private CommanderInfo _GetCommanderInfo(string journalsDirectoryPath)
        {
            var journalsDirectory = new DirectoryInfo(journalsDirectoryPath);
            if (journalsDirectory.Exists)
            {
                var latestJournalFile = journalsDirectory
                    .EnumerateFiles("*.log", SearchOption.TopDirectoryOnly).OrderByDescending(journalFile => journalFile.LastWriteTimeUtc)
                    .FirstOrDefault();
                if (latestJournalFile is object)
                    using (var fileStream = new FileStream(latestJournalFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                        return _journalReader.Read(streamReader);
                else
                    return new CommanderInfo();
            }
            else
                return new CommanderInfo();
        }

        private void _BlueprintGradeRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BlueprintRequirementRepetitionsViewModel.Repetitions))
                _ResetTradeSolutions();
        }

        private void _ResetTradeSolutions()
        {
            RawMaterialsTradeSolution = null;
            EncodedMaterialsTradeSolution = null;
            ManufacturedMaterialsTradeSolution = null;
        }
    }
}