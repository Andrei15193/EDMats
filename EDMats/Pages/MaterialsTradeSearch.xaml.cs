using System.Windows;
using System.Windows.Controls;
using EDMats.ViewModels;

namespace EDMats.Pages
{
    public partial class MaterialsTradeSearch : UserControl
    {
        public MaterialsTradeSearch()
            => InitializeComponent();

        private void _Loaded(object sender, RoutedEventArgs e)
            => ((MaterialsTradeSearchViewModel)FindResource("MaterialsTradeSearchViewModel")).Load();

        private void _ListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;
            if (e.AddedItems.Count > 0 && listView.SelectedItem != e.AddedItems[0])
                listView.SelectedItem = e.AddedItems[0];
        }
    }
}