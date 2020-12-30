namespace EDMats.ViewModels
{
    public class NotificationViewModel : ViewModel
    {
        public NotificationViewModel(NotificationsViewModel notificationsViewModel, string text)
        {
            Text = text;
            RemoveCommand = CreateCommand(() => notificationsViewModel.RemoveNotification(this));
        }

        public string Text { get; }

        public Command RemoveCommand { get; }
    }
}