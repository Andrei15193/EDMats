using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EDMats.Services;
using EDMats.ViewModels;
using Microsoft.Win32;

namespace EDMats
{
    public partial class MainWindow : Window
    {
        private Task _autoUpdateTask = Task.CompletedTask;
        private readonly TimeSpan _autoUpdateDelay = TimeSpan.FromSeconds(5);

        public MainWindow()
        {
            InitializeComponent();
            Loaded += delegate
            {
                CommanderViewModel.PropertyChanged += _CommanderViewModelPropertyChanged;
            };
            Unloaded += delegate
            {
                CommanderViewModel.PropertyChanged += _CommanderViewModelPropertyChanged;
            };
            _UpdateStatusPanel();
        }

        public NotificationsViewModel NotificationsViewModel { get; } = App.Resolve<NotificationsViewModel>();

        public CommanderViewModel CommanderViewModel { get; } = App.Resolve<CommanderViewModel>();

        private async void _LoadJournalFileButtonClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Log Files (*.log)|*.log|All Files (*.*)|*.*"
            };

            var logsDirectory = new DirectoryInfo($@"{Environment.GetEnvironmentVariable("userprofile")}\Saved Games\Frontier Developments\Elite Dangerous");
            if (logsDirectory.Exists)
                openFileDialog.InitialDirectory = logsDirectory.FullName;

            if (openFileDialog.ShowDialog(this) ?? false)
            {
                CommanderViewModel.JournalFilePath = openFileDialog.FileName;
                await CommanderViewModel.LoadJournalAsync();
                await CommanderViewModel.SearchForTradeSolutionAsync();
            }

            if (_autoUpdateTask.IsCompleted)
                _autoUpdateTask = _AutoUpdateJournalEntries();
        }

        private void _AutoUpdateCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            if (_autoUpdateTask.IsCompleted)
                _autoUpdateTask = _AutoUpdateJournalEntries();
        }

        private async Task _AutoUpdateJournalEntries()
        {
            if (!string.IsNullOrWhiteSpace(CommanderViewModel.JournalFilePath))
            {
                await Task.Delay(_autoUpdateDelay);
                while (_AutoUpdateCheckBox.IsChecked ?? false)
                {
                    if (await CommanderViewModel.RefreshJournalAsync())
                        await CommanderViewModel.SearchForTradeSolutionAsync();
                    await Task.Delay(_autoUpdateDelay);
                }
            }
        }

        private async void _MaterialGoalTextChanged(object sender, TextChangedEventArgs e)
        {
            var materialGoalTextBox = (TextBox)sender;
            var bindingExpression = materialGoalTextBox.GetBindingExpression(TextBox.TextProperty);
            if (!_IsPositiveInteger(materialGoalTextBox.Text))
                Validation.MarkInvalid(bindingExpression, new ValidationError(new MockValidationRule(), bindingExpression));
            else
            {
                Validation.ClearInvalid(bindingExpression);
                var storedMaterial = (StoredMaterial)materialGoalTextBox.DataContext;
                var amountGoal = int.Parse(materialGoalTextBox.Text);
                storedMaterial.Amount = amountGoal;
                await CommanderViewModel.SearchForTradeSolutionAsync();
            }
        }

        private static bool _IsPositiveInteger(string value)
            => !string.IsNullOrWhiteSpace(value)
                && int.TryParse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.CurrentCulture, out var number);

        private sealed class MockValidationRule : ValidationRule
        {
            public override ValidationResult Validate(object value, CultureInfo cultureInfo)
                => throw new NotImplementedException();
        }

        private async void _LoadCommanderGoalsButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var fileDialog = new OpenFileDialog
                {
                    Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
                };
                if (fileDialog.ShowDialog(this) ?? false)
                {
                    //await GoalActions.LoadCommanderGoalsAsync(fileDialog.FileName);
                    await CommanderViewModel.SearchForTradeSolutionAsync();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void _SaveCommanderGoalsButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var fileDialog = new SaveFileDialog
                {
                    Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
                };
                if (fileDialog.ShowDialog(this) ?? false)
                {
                    var menuItem = (MenuItem)sender;

                    var commanderGoals = new CommanderGoalsData
                    {
                        Materials = CommanderViewModel
                            .MaterialGoals
                            .Select(
                                materialGoal => new MaterialGoalData
                                {
                                    MaterialId = materialGoal.Id,
                                    Name = materialGoal.Name,
                                    Amount = materialGoal.Amount
                                }
                            )
                            .ToList()
                    };
                    //await GoalActions.SaveCommanderGoalsAsync(fileDialog.FileName, commanderGoals);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void _ViewTradeSolutionButtonClick(object sender, RoutedEventArgs e)
            => new TradeSolutionWindow { DataContext = CommanderViewModel.TradeSolution }.ShowDialog();

        private void _CommanderViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(CommanderViewModel.SearchStatus), StringComparison.OrdinalIgnoreCase))
                _UpdateStatusPanel();
        }

        private void _UpdateStatusPanel()
        {
            switch (CommanderViewModel.SearchStatus)
            {
                case TradeSolutionSearchStatus.Idle:
                    _StatusTextBlock.Text = "Idle";
                    _ViewTradeSolutionButton.IsEnabled = false;
                    break;

                case TradeSolutionSearchStatus.Searching:
                    _StatusTextBlock.Text = "Searching";
                    _ViewTradeSolutionButton.IsEnabled = false;
                    break;

                case TradeSolutionSearchStatus.SearchSucceeded:
                    _StatusTextBlock.Text = "Search succeeded";
                    _ViewTradeSolutionButton.IsEnabled = true;
                    break;

                case TradeSolutionSearchStatus.SearchFailed:
                    _StatusTextBlock.Text = "Search failed, not enough materials";
                    _ViewTradeSolutionButton.IsEnabled = false;
                    break;
            }
        }
    }
}