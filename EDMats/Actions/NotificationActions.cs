using System;
using EDMats.ActionsData;
using FluxBase;

namespace EDMats.Actions
{
    public class NotificationActions
    {
        private readonly Dispatcher _dispatcher;

        public NotificationActions(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void DismissNotification(Guid id)
            => _dispatcher.Dispatch(new DismissNotificationActionData(id));

        public void DismissAllNotifications()
            => _dispatcher.Dispatch(new DismissAllNotificationsActionData());
    }
}