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
                => int.TryParse(textBox.Text.Substring(0, textBox.SelectionStart) + e.Text + textBox.Text.Substring(textBox.SelectionStart + textBox.SelectionLength), out var number) && 0 <= number && number <= 50;
        }

        private void _ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0)
                ((ScrollViewer)sender).ScrollToVerticalOffset(e.VerticalOffset - e.VerticalChange);
        }

        private void _ListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;
            if (e.AddedItems.Count > 0 && listView.SelectedItem != e.AddedItems[0])
                listView.SelectedItem = e.AddedItems[0];
        }
    }
}