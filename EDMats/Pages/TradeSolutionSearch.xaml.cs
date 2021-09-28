﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace EDMats.Pages
{
    public partial class TradeSolutionSearch : UserControl
    {
        private readonly DispatcherTimer _tradeSolutionSearchTimer;

        public TradeSolutionSearch()
        {
            InitializeComponent();
            _tradeSolutionSearchTimer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, delegate { ViewModel.SearchTradeSolutionCommand.Execute(); }, Dispatcher)
            {
                IsEnabled = false
            };
        }

        private void _DeselectModule(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedModule = null;
            ((ToggleButton)sender).IsChecked = true;
        }

        private void _PreviewRepetitionInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            e.Handled = !(
                int.TryParse(textBox.Text.Substring(0, textBox.SelectionStart) + e.Text + textBox.Text.Substring(textBox.SelectionStart + textBox.SelectionLength), out var number)
                && 0 <= number && number <= 50
            );
        }

        private void _BeginTradeSolutionSearch(object sender, RoutedEventArgs e)
            => _tradeSolutionSearchTimer.IsEnabled = true;

        private void _EndTradeSolutionSearch(object sender, RoutedEventArgs e)
            => _tradeSolutionSearchTimer.IsEnabled = false;

        private void _SolutionSearchToggleButtonChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var toggleButton = (ToggleButton)sender;
            if (!toggleButton.IsEnabled)
                toggleButton.IsChecked = false;
        }
    }
}