using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace EDMats.Pages
{
    public partial class About : UserControl
    {
        public About()
            => InitializeComponent();

        private void _OpenHyperlink(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}