namespace EDMats.ActionsData
{
    public class OpeningJournalFileActionData
    {
        public OpeningJournalFileActionData(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath { get; }
    }
}