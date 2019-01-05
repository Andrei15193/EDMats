using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Flux
{
    public class Dispatcher
    {
        internal static Dispatcher Instance { get; } = new Dispatcher();

        private const int _availableState = 0;
        private const int _invokingState = 1;

        private int _state = _availableState;
        private readonly ISet<Action<ActionData>> _subscribers = new HashSet<Action<ActionData>>();

        protected Dispatcher()
        {
        }

        public void Register(Action<ActionData> callback)
            => _subscribers.Add(callback);

        public void Dispatch(ActionData actionData)
        {
            if (Interlocked.CompareExchange(ref _state, _invokingState, _availableState) != _availableState)
                throw new InvalidOperationException("Cannot dispatch message while there is a message currently dispatching.");

            try
            {
                var actualActionData = actionData ?? ActionData.Empty;
                foreach (var subscriber in _subscribers)
                    subscriber(actualActionData);
            }
            catch (TargetInvocationException targetInvocationException) when (targetInvocationException.InnerException != null)
            {
                throw targetInvocationException.InnerException;
            }
            finally
            {
                Interlocked.Exchange(ref _state, _availableState);
            }
        }
    }
}