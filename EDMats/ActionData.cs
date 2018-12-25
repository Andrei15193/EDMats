namespace EDMats
{
    public class ActionData
    {
        public static ActionData Empty { get; } = new ActionData();

        protected ActionData()
        {
        }

        public ActionState State { get; set; } = ActionState.Completed;
    }
}