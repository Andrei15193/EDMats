namespace EDMats.ActionsData
{
    public class OpeningJournalFileActionData : NotificationActionData
    {
        public OpeningJournalFileActionData(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; }
    }
}