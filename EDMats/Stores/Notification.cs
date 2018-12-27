using System;

namespace EDMats.Stores
{
    public class Notification
    {
        public Notification(Guid id, string text)
        {
            Id = id;
            Text = text;
        }

        public Guid Id { get; }

        public string Text { get; }
    }
}