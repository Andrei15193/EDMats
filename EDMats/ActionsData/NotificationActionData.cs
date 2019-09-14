using System;
using FluxBase;

namespace EDMats.ActionsData
{
    public class NotificationActionData
    {
        private readonly Guid _id = Guid.NewGuid();
        private string _text;

        protected NotificationActionData()
        {
        }

        public NotificationActionData(string text)
        {
            NotificationText = text;
        }

        public Guid NotificationId
            => _text == null ? default(Guid) : _id;

        public string NotificationText
        {
            get
            {
                return _text;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _text = null;
                else
                    _text = value;
            }
        }
    }
}