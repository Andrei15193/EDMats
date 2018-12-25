namespace EDMats.ActionsData
{
    public class OpeningJournalFileActionData : ActionData
    {
        public OpeningJournalFileActionData(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; }
    }
}