using EDMats.Services;

namespace EDMats.ActionsData
{
    public class JournalImportedActionData
    {
        public JournalImportedActionData(JournalCommanderInformation commanderInformation)
        {
            CommanderInformation = commanderInformation;
        }

        public JournalCommanderInformation CommanderInformation { get; }
    }
}