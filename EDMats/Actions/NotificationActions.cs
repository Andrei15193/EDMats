using System;
using EDMats.ActionsData;

namespace EDMats.Actions
{
    public class NotificationActions : ActionSet
    {
        public void DismissNotification(Guid id)
            => Dispatch(new DismissNotificationActionData(id));
    }
}