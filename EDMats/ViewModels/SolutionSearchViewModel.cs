using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EDMats.Journals;
using EDMats.Models.Engineering;
using EDMats.Models.Materials;
using EDMats.Models.Trading;
using EDMats.Storage;

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
        private readonly CommanderProfileStorageHandler _commanderProfileStorageHandler;
        private readonly JournalReader _journalReader;

        public SolutionSearchViewModel()
            : this(App.Resolve<CommanderProfileStorageHandler>(), App.Resolve<JournalReader>())
        {
            _encodedMaterialsTradeSolution = new TradeSolution(Enumerable.Empty<TradeEntry>());
            _manufacturedMaterialsTradeSolution = new TradeSolution(new[] { new TradeEntry(new MaterialQuantity(Material.Iron, 3), new MaterialQuantity(Material.Manganese, 3)) });
        }

        public SolutionSearchViewModel(CommanderProfileStorageHandler commanderProfileStorageHandler, JournalReader journalReader)
            => (_commanderProfileStorageHandler, _journalReader) = (commanderProfileStorageHandler, journalReader);

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
                    BlueprintGradeRequirements = _selectedBlueprint?.GradeRequirements.Select(gradeRequirement => new BlueprintRequirementRepetitionsViewModel(gradeRequirement));
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
                }
            }
        }

        public void SearchTradeSolutions()
        {
            var commanderProfile = _commanderProfileStorageHandler.Load();
            var commanderInfo = _GetCommanderInfo(commanderProfile.JournalsDirectoryPath);
        }

        private CommanderInfo _GetCommanderInfo(string journalsDirectoryPath)
        {
            var journalsDirectory = new DirectoryInfo(journalsDirectoryPath);
            if (journalsDirectory.Exists)
            {
                var latestJournalFile = journalsDirectory
                    .EnumerateFiles("*.log", SearchOption.TopDirectoryOnly).OrderBy(journalFile => journalFile.LastWriteTimeUtc)
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
    }
}