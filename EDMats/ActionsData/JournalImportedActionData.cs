using EDMats.Services;

namespace EDMats.ActionsData
{
    public class JournalImportedActionData : NotificationActionData
    {
        public JournalImportedActionData(JournalCommanderInformation commanderInformation)
        {
            CommanderInformation = commanderInformation;
        }

        public JournalCommanderInformation CommanderInformation { get; }
    }
}