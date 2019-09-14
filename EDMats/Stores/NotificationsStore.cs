using System;
using System.Collections.ObjectModel;
using EDMats.ActionsData;
using FluxBase;

namespace EDMats.Stores
{
    public class NotificationsStore : Store
    {
        private readonly ObservableCollection<Notification> _notifications;

        public NotificationsStore()
        {
            _notifications = new ObservableCollection<Notification>();
            Notifications = new ReadOnlyObservableCollection<Notification>(_notifications);
        }

        public ReadOnlyObservableCollection<Notification> Notifications { get; }

        public void Handle(NotificationActionData notificationActionData)
        {
            if (notificationActionData.NotificationId != default(Guid))
                _notifications.Insert(0, new Notification(notificationActionData.NotificationId, notificationActionData.NotificationText));
        }

        public void Handle(DismissNotificationActionData dismissNotificationActionData)
        {
            var index = _notifications.Count - 1;
            while (index >= 0 && _notifications[index].Id != dismissNotificationActionData.Id)
                index--;
            if (index >= 0)
                _notifications.RemoveAt(index);
        }

        public void Handle(DismissAllNotificationsActionData dismissAllNotificationsActionData)
        {
            _notifications.Clear();
        }
    }
}