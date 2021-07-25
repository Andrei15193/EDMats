using System.IO;
using System.Windows;
using System.Windows.Controls;
using EDMats.ViewModels;
using Microsoft.Win32;

namespace EDMats.Pages
{
    public partial class Commander : UserControl
    {
        public static readonly DependencyProperty CommanderViewModelProperty = DependencyProperty.Register(
            nameof(CommanderViewModel),
            typeof(CommanderViewModel),
            typeof(Commander)
        );

        public Commander()
            => InitializeComponent();

        public CommanderViewModel CommanderViewModel
        {
            get => (CommanderViewModel)GetValue(CommanderViewModelProperty);
            set => SetValue(CommanderViewModelProperty, value);
        }

        private void _OpenDirecotryBrowser(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog()
            {
                InitialDirectory = CommanderViewModel.JournalsDirectoryPath,
                DefaultExt = ".log",
                Filter = "Log files (*.log)|*.log|All files (*.*)|*.*"
            };
            if (fileDialog.ShowDialog() ?? false)
                CommanderViewModel.JournalsDirectoryPath = Path.GetDirectoryName(fileDialog.FileName);
        }
    }
}