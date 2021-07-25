using System;
using System.IO;
using EDMats.Data;
using EDMats.Storage;

namespace EDMats.ViewModels
{
    public class CommanderViewModel : ViewModel
    {
        private readonly IProfileStorageHandler _profileStorageHandler;
        private string _commanderName;
        private ProfilePicture _commanderProfilePicture;
        private string _name;
        private ProfilePicture _profilePicture;
        private string _journalsDirectoryPath;

        public CommanderViewModel()
            : this(App.Resolve<IProfileStorageHandler>())
        {
        }

        public CommanderViewModel(IProfileStorageHandler profileStorageHandler)
        {
            _profileStorageHandler = profileStorageHandler;

            SaveCommand = CreateCommand(_Save);
            ResetCommand = CreateCommand(_Reset);
        }

        public event EventHandler Saved;

        public event EventHandler Reset;

        public string CommanderName
        {
            get => _commanderName;
            private set
            {
                if (_commanderName != value)
                {
                    _commanderName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ProfilePicture CommanderPicture
        {
            get => _commanderProfilePicture;
            private set
            {
                if (_commanderProfilePicture != value)
                {
                    _commanderProfilePicture = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ProfilePicture Picture
        {
            get => _profilePicture;
            set
            {
                if (_profilePicture != value)
                {
                    _profilePicture = value;
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

        public Command SaveCommand { get; }

        public Command ResetCommand { get; }

        public void Load()
        {
            var profile = _profileStorageHandler.LoadProfile("default");

            CommanderName = Name = profile.Commander.Name ?? "Anonymous";

            ProfilePicture profilePicture;
            if (!Enum.TryParse(profile.Commander.Picture, out profilePicture))
                profilePicture = ProfilePicture.Sidewinder;
            CommanderPicture = Picture = profilePicture;

            JournalsDirectoryPath = profile.Commander.JournalsDirectoryPath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Saved Games", "Frontier Developments", "Elite Dangerous");
        }

        private void _Save()
        {
            var profile = _profileStorageHandler.LoadProfile("default");
            profile.Commander.Name = CommanderName = Name;
            profile.Commander.Picture = (CommanderPicture = Picture).ToString("G");
            profile.Commander.JournalsDirectoryPath = JournalsDirectoryPath;
            _profileStorageHandler.SaveProfile(profile);
            Saved?.Invoke(this, EventArgs.Empty);
        }

        private void _Reset()
        {
            Load();
            Reset?.Invoke(this, EventArgs.Empty);
        }
    }
}