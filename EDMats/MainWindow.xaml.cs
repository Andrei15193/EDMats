using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using EDMats.Actions;
using EDMats.Services;
using EDMats.Stores;
using Microsoft.Win32;
using Unity.Attributes;

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
                App.GoalsStore.PropertyChanged += _GoalsStorePropertyChanged;
            };
            Unloaded += delegate
            {
                App.GoalsStore.PropertyChanged += _GoalsStorePropertyChanged;
            };
            _UpdateStatusPanel();
        }

        [Dependency]
        public JournalImportActions SettingsActions { get; set; }

        [Dependency]
        public GoalActions GoalActions { get; set; }

        private async void _LoadJournalFileButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Log Files (*.log)|*.log|All Files (*.*)|*.*"
                };
                if (openFileDialog.ShowDialog(this) ?? false)
                {
                    await SettingsActions.LoadJournalFileAsync(openFileDialog.FileName);
                    await _SearchForTradeSolutionAsync();
                }

                if (_autoUpdateTask.IsCompleted)
                    _autoUpdateTask = _AutoUpdateJournalEntries();
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void _AutoUpdateCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            if (_autoUpdateTask.IsCompleted)
                _autoUpdateTask = _AutoUpdateJournalEntries();
        }

        private async Task _AutoUpdateJournalEntries()
        {
            if (!string.IsNullOrWhiteSpace(App.CommanderInfoStore.JournalFilePath))
            {
                await Task.Delay(_autoUpdateDelay);
                while (_AutoUpdateCheckBox.IsChecked ?? false)
                {
                    await SettingsActions.LoadJournalFileAsync(App.CommanderInfoStore.JournalFilePath, App.CommanderInfoStore.LatestUpdate);
                    await _SearchForTradeSolutionAsync();
                    await Task.Delay(_autoUpdateDelay);
                }
            }
        }

        private CancellationTokenSource _cancellationTokenSource = null;

        private void _FilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var filterTextBox = (TextBox)sender;
            SettingsActions.FilterMaterials(filterTextBox.Text);
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
                GoalActions.UpdateMaterialAmountGoal(storedMaterial.Id, amountGoal);
                await _SearchForTradeSolutionAsync();
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
                    await GoalActions.LoadCommanderGoalsAsync(fileDialog.FileName);
                    await _SearchForTradeSolutionAsync();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void _SaveCommanderGoalsButtonClick(object sender, RoutedEventArgs e)
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
                    var goalsStore = (GoalsStore)menuItem.DataContext;

                    var commanderGoals = new CommanderGoalsData
                    {
                        Materials = goalsStore
                            .MaterialsGoal
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
                    await GoalActions.SaveCommanderGoalsAsync(fileDialog.FileName, commanderGoals);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void _ViewTradeSolutionButtonClick(object sender, RoutedEventArgs e)
            => new TradeSolutionWindow().ShowDialog();

        private async Task _SearchForTradeSolutionAsync()
        {
            _cancellationTokenSource?.Cancel();
            using (var cancellationTokenSource = new CancellationTokenSource())
                try
                {
                    _cancellationTokenSource = cancellationTokenSource;
                    await GoalActions.TryFindGoalTradeSolution(App.GoalsStore.MaterialsGoal, App.CommanderInfoStore.StoredMaterials, cancellationTokenSource.Token);
                }
                catch (OperationCanceledException operationCanceledException) when (operationCanceledException.CancellationToken == _cancellationTokenSource.Token)
                {
                }

            _cancellationTokenSource = null;
        }

        private void _GoalsStorePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(GoalsStore.SearchStatus), StringComparison.OrdinalIgnoreCase))
                _UpdateStatusPanel();
        }

        private void _UpdateStatusPanel()
        {
            switch (App.GoalsStore.SearchStatus)
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