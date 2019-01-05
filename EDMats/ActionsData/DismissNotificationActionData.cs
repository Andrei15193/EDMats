using System;
using Flux;

namespace EDMats.ActionsData
{
    public class DismissNotificationActionData : ActionData
    {
        public DismissNotificationActionData(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}