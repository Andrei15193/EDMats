using System;
using System.Windows;
using System.Windows.Controls;
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
    }
}