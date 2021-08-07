using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using EDMats.ViewModels;

namespace EDMats.Pages
{
    public partial class MaterialsTradeSearch : UserControl
    {
        private readonly DispatcherTimer _tradeSolutionSearchTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1),
            IsEnabled = false
        };

        public MaterialsTradeSearch()
            => InitializeComponent();

        private void _Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (MaterialsTradeSearchViewModel)FindResource("MaterialsTradeSearchViewModel");

            viewModel.Load();
            _tradeSolutionSearchTimer.Tick += _SearchForTradeSolution;
            viewModel.PropertyChanged += _ViewModelPropertyChanged;
            if (viewModel.IsSessionActive)
                _SearchForTradeSolution();
        }

        private void _Unloaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (MaterialsTradeSearchViewModel)FindResource("MaterialsTradeSearchViewModel");

            viewModel.PropertyChanged -= _ViewModelPropertyChanged;
            _tradeSolutionSearchTimer.Stop();
            _tradeSolutionSearchTimer.Tick -= _SearchForTradeSolution;
        }

        private void _ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MaterialsTradeSearchViewModel.IsSessionActive))
            {
                var viewModel = (MaterialsTradeSearchViewModel)FindResource("MaterialsTradeSearchViewModel");

                if (viewModel.IsSessionActive)
                    _SearchForTradeSolution();
                else
                    _tradeSolutionSearchTimer.Stop();
            }
        }
        private void _ListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;
            if (e.AddedItems.Count > 0 && listView.SelectedItem != e.AddedItems[0])
                listView.SelectedItem = e.AddedItems[0];
        }

        private async void _SearchForTradeSolution(object sender = null, EventArgs e = null)
        {
            var viewModel = (MaterialsTradeSearchViewModel)FindResource("MaterialsTradeSearchViewModel");

            try
            {
                _tradeSolutionSearchTimer.Stop();
                await viewModel.SearchTradeSolution();
            }
            finally
            {
                _tradeSolutionSearchTimer.Start();
            }
        }

        private void _OpenTradeSolutionView(object sender, RoutedEventArgs e)
        {
            var viewModel = (MaterialsTradeSearchViewModel)FindResource("MaterialsTradeSearchViewModel");

            var tradeSolutionWindow = new TradeSolutionWindow
            {
                DataContext = viewModel.TradeSolution
            };
            tradeSolutionWindow.ShowDialog();
        }
    }
}