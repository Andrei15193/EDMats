using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using EDMats.ViewModels;
using Microsoft.Win32;

namespace EDMats.Pages
{
    public partial class Commander : UserControl
    {
        public Commander()
            => InitializeComponent();

        public event EventHandler Saved;

        public event EventHandler Cancelled;

        private void _CancelButtonClicked(object sender, RoutedEventArgs e)
            => Cancelled?.Invoke(this, EventArgs.Empty);

        private void _ViewModelSaved(object sender, EventArgs e)
            => Saved?.Invoke(this, e);

        private void _LoadCommanderInfo(object sender, RoutedEventArgs e)
            => ((CommanderViewModel)DataContext).Load();

        private void _OpenJournalsFileBrowser(object sender, RoutedEventArgs e)
        {
            var commanderViewModel = (CommanderViewModel)DataContext;
            var fileDialog = new OpenFileDialog
            {
                InitialDirectory = commanderViewModel.JournalsDirectoryPath,
                DefaultExt = ".log",
                Filter = "Log files (*.log)|*.log|All files (*.*)|*.*"
            };
            if (fileDialog.ShowDialog() ?? false)
                commanderViewModel.JournalsDirectoryPath = Path.GetDirectoryName(fileDialog.FileName);
        }
    }
}