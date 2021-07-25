using System.Windows;
using System.Windows.Controls;
using EDMats.ViewModels;

namespace EDMats.Pages
{
    public partial class Materials : UserControl
    {
        public Materials()
            => InitializeComponent();

        private void _Loaded(object sender, RoutedEventArgs e)
            => ((MaterialsViewModel)FindResource("ViewModel")).Load();
    }
}