using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using EDMats.ViewModels;
using Microsoft.Win32;

namespace EDMats
{
    public partial class MainWindow : Window
    {
        public MainWindow()
            => InitializeComponent();

        private void _SectionSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Section.SelectedItem is null)
                Section.SelectedItem = Main;
        }

        private void _ToggleButtonChecked(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton)
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
                }
                else
                    toggleButton.IsChecked = false;
            }
        }

        private void _UnselectTab(object sender, EventArgs e)
            => Section.SelectedItem = null;

        private void _LoadCommander(object sender, RoutedEventArgs e)
            => ((CommanderViewModelNew)Resources["CommanderViewModel"]).Load();
    }
}