using EDMats.Services;

namespace EDMats.ActionsData
{
    public class CommanderGoalsLoadedActionData : NotificationActionData
    {
        public CommanderGoalsLoadedActionData(CommanderGoalsData commanderGoals)
        {
            CommanderGoals = commanderGoals;
        }

        public CommanderGoalsData CommanderGoals { get; }
    }
}