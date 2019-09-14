using System;
using FluxBase;

namespace EDMats.ActionsData
{
    public class DismissNotificationActionData
    {
        public DismissNotificationActionData(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}