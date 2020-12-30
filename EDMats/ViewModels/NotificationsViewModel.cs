using System.Collections.ObjectModel;

namespace EDMats.ViewModels
{
    public class NotificationsViewModel : ViewModel
    {
        private readonly ObservableCollection<NotificationViewModel> _notifications;

        public NotificationsViewModel()
        {
            _notifications = new ObservableCollection<NotificationViewModel>();
            Notifications = new ReadOnlyObservableCollection<NotificationViewModel>(_notifications);
            ClearNotificationsCommand = CreateCommand(_notifications.Clear);
        }

        public ReadOnlyObservableCollection<NotificationViewModel> Notifications { get; }

        public void AddNotification(string notification)
            => _notifications.Insert(0, new NotificationViewModel(this, notification));

        public void RemoveNotification(NotificationViewModel notification)
            => _notifications.Remove(notification);

        public Command ClearNotificationsCommand { get; }
    }
}