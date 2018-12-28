namespace EDMats.ActionsData
{
    public class LoadingCommanderGoalsActionData : NotificationActionData
    {
        public LoadingCommanderGoalsActionData(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }
    }
}