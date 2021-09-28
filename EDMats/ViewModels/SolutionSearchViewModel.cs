using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EDMats.Journals;
using EDMats.Models.Engineering;
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
        private object _rawMaterialsTradeSolution;
        private object _encodedMaterialsTradeSolution;
        private object _manufacturedMaterialsTradeSolution;
        private readonly CommanderProfileStorageHandler _commanderProfileStorageHandler;
        private readonly JournalReader _journalReader;

        public SolutionSearchViewModel()
            : this(App.Resolve<CommanderProfileStorageHandler>(), App.Resolve<JournalReader>())
        {
        }

        public SolutionSearchViewModel(CommanderProfileStorageHandler commanderProfileStorageHandler, JournalReader journalReader)
        {
            _commanderProfileStorageHandler = commanderProfileStorageHandler;
            _journalReader = journalReader;
            SearchTradeSolutionCommand = CreateCommand(
                () => (RawMaterialsTradeSolution is null || EncodedMaterialsTradeSolution is null || ManufacturedMaterialsTradeSolution is null)
                      && (SelectedBlueprint is object || SelectedExperimentalEffect is object),
                _SearchSolution,
                new[] { nameof(RawMaterialsTradeSolution), nameof(EncodedMaterialsTradeSolution), nameof(ManufacturedMaterialsTradeSolution), nameof(SelectedBlueprint), nameof(SelectedExperimentalEffect) }
            );
        }

        public Command SearchTradeSolutionCommand { get; }

        public object RawMaterialsTradeSolution
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

        public object EncodedMaterialsTradeSolution
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

        public object ManufacturedMaterialsTradeSolution
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

        private void _SearchSolution()
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
                    using (var fileStream = new FileStream(journalsDirectoryPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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