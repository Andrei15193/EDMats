using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EDMats.Data.Engineering;
using EDMats.ViewModels;

namespace EDMats.Pages
{
    public partial class ModuleEngineering : UserControl
    {
        public ModuleEngineering()
            => InitializeComponent();

        public event EventHandler Saved;

        public event EventHandler Cancelled;

        private void _ModuleEngineeringSaved(object sender, EventArgs e)
            => Saved?.Invoke(this, EventArgs.Empty);

        private void _CancelButtonClick(object sender, RoutedEventArgs e)
            => Cancelled?.Invoke(this, EventArgs.Empty);

        private void _DataContextChanges(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Module module)
            {
                var viewModel = (ModuleEngineeringViewModel)Resources["ViewModel"];
                viewModel.LoadModule(module);
            }
        }

        private void _PreviewRepetitionInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            e.Handled = !_IsValidRepetitionNumber();

            bool _IsValidRepetitionNumber()
                => int.TryParse(textBox.Text + e.Text, out var number) && 0 <= number && number <= 50;
        }
    }
}