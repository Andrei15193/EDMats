using System;
using System.Windows.Input;
using EDMats.Data;
using EDMats.Storage;

namespace EDMats.ViewModels
{
    public class CommanderViewModel : ViewModel
    {
        private string _commanderName;
        private string _journalsDirectoryPath;
        private readonly ICommanderProfileStorageHandler _commanderProfileStorageHandler;

        public CommanderViewModel()
            : this(App.Resolve<ICommanderProfileStorageHandler>())
        {
        }

        public CommanderViewModel(ICommanderProfileStorageHandler commanderProfileStorageHandler)
        {
            _commanderProfileStorageHandler = commanderProfileStorageHandler;
            SaveCommand = CreateCommand(_Save);
        }

        public event EventHandler Saved;

        public string CommanderName
        {
            get => _commanderName;
            set
            {
                if (_commanderName != value)
                {
                    _commanderName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string JournalsDirectoryPath
        {
            get => _journalsDirectoryPath;
            set
            {
                if (_journalsDirectoryPath != value)
                {
                    _journalsDirectoryPath = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; }

        public void Load()
        {
            var commanderProfile = _commanderProfileStorageHandler.Load();

            CommanderName = commanderProfile.CommanderName;
            JournalsDirectoryPath = commanderProfile.JournalsDirectoryPath;
        }

        private void _Save()
        {
            _commanderProfileStorageHandler.Save(new CommanderProfile
            {
                CommanderName = CommanderName,
                JournalsDirectoryPath = JournalsDirectoryPath
            });
            Saved?.Invoke(this, EventArgs.Empty);
        }
    }
}