namespace EDMats.ActionsData
{
    public class SavingCommanderGoalsActionData : NotificationActionData
    {
        public SavingCommanderGoalsActionData(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }
    }
}