namespace EDMats
{
    public abstract class ActionSet
    {
        protected void Dispatch(ActionData actionData)
            => Dispatcher.Instance.Dispatch(actionData);
    }
}