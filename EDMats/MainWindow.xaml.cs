using System;
using System.Windows;
using System.Windows.Controls;
using EDMats.Actions;
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
        public SettingsActions SettingsActions { get; set; }

        private async void _BrowseJournalFile(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
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
            SettingsActions.FilterMaterials(_FilterTextBox.Text);
        }
    }
}