using System;
using EDMats.ActionsData;

namespace EDMats.Actions
{
    public class NotificationActions : Flux.Actions
    {
        public void DismissNotification(Guid id)
            => Dispatch(new DismissNotificationActionData(id));

        public void DismissAllNotifications()
            => Dispatch(new DismissAllNotificationsActionData());
    }
}