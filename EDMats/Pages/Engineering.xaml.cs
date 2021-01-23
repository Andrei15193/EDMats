using System;
using System.Windows.Controls;

namespace EDMats.Pages
{
    public partial class Engineering : UserControl
    {
        public Engineering()
            => InitializeComponent();

        private void _UnselectModule(object sender, EventArgs e)
            => ModulesListView.SelectedItem = null;
    }
}