namespace EDMats
{
    public abstract class ActionSet
    {
        protected virtual void Dispatch(ActionData actionData)
            => Dispatcher.Instance.Dispatch(actionData);
    }
}