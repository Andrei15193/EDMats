namespace Flux
{
    public abstract class Actions
    {
        protected virtual void Dispatch(ActionData actionData)
            => Dispatcher.Instance.Dispatch(actionData);
    }
}