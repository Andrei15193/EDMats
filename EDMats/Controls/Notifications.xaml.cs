using System.Windows.Controls;
using EDMats.Actions;
using EDMats.Stores;
using Unity;

namespace EDMats.Controls
{
    public partial class Notifications : ItemsControl
    {
        public Notifications()
        {
            InitializeComponent();
            App.EnsureDependencies(this);
        }

        [Dependency]
        public NotificationActions NotificationActions { get; set; }

        private void _DismissNotification(object sender, System.Windows.RoutedEventArgs e)
        {
            var notification = (Notification)((Button)sender).DataContext;
            NotificationActions.DismissNotification(notification.Id);
        }
    }
}