using System;
using System.Windows;
using System.Windows.Controls;

namespace EDMats
{
    public partial class MainWindow : Window
    {
        public MainWindow()
            => InitializeComponent();

        private void _SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                ((TabControl)sender).SelectedIndex = 0;
        }

        private void _SelectSearchTab(object sender, EventArgs e)
            => TabControl.SelectedIndex = 0;
    }
}