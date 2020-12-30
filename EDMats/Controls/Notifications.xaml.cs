using System.Windows.Controls;
using EDMats.ViewModels;

namespace EDMats.Controls
{
    public partial class Notifications : ItemsControl
    {
        public Notifications()
            => InitializeComponent();

        public NotificationsViewModel NotificationsViewModel { get; } = App.Resolve<NotificationsViewModel>();
    }
}