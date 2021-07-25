using System;
using System.Windows;
using System.Windows.Controls;
using EDMats.ViewModels;

namespace EDMats
{
    public partial class MainWindow : Window
    {
        public MainWindow()
            => InitializeComponent();

        private void _SectionSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Section.SelectedItem is null)
                Section.SelectedItem = MaterialsTradeSearch;
        }

        private void _UnselectTab(object sender, EventArgs e)
            => Section.SelectedItem = null;

        private void _LoadCommander(object sender, RoutedEventArgs e)
            => ((CommanderViewModel)Resources["CommanderViewModel"]).Load();
    }
}