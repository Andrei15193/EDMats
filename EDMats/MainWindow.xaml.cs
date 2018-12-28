using System;
using System.Globalization;
using System.Linq;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        [Dependency]
        public JournalImportActions SettingsActions { get; set; }

        [Dependency]
        public GoalActions GoalActions { get; set; }

        private async void _BrowseJournalFile(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Log Files (*.log)|*.log|All Files (*.*)|*.*"
                };
                if (openFileDialog.ShowDialog(this) ?? false)
                    await SettingsActions.LoadJournalFileAsync(openFileDialog.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void _FilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var filterTextBox = (TextBox)sender;
            SettingsActions.FilterMaterials(filterTextBox.Text);
        }

        private void _MaterialGoalTextChanged(object sender, TextChangedEventArgs e)
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

        private async void _LoadCommanderGoals(object sender, RoutedEventArgs e)
        {
            try
            {
                var fileDialog = new OpenFileDialog
                {
                    Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
                };
                if (fileDialog.ShowDialog(this) ?? false)
                    await GoalActions.LoadCommanderGoalsAsync(fileDialog.FileName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void _SaveCommanderGoals(object sender, RoutedEventArgs e)
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
    }
}