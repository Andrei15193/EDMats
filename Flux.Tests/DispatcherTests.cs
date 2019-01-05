using System.Threading;
using Xunit;

namespace Flux.Tests
{
    public class DispatcherTests
    {
        private Dispatcher _Dispatcher { get; } = new MockDispatcher();

        [Fact]
        public void RegisteringToDispatcherInvokesCallback()
        {
            var invocationCount = 0;

            _Dispatcher.Register(actionData => Interlocked.Increment(ref invocationCount));
            _Dispatcher.Dispatch(null);

            Assert.Equal(1, invocationCount);
        }

        [Fact]
        public void RegisteringToDispatcherTwiceInvokesCallbackOnce()
        {
            var invocationCount = 0;

            void Callback(ActionData actionData) => Interlocked.Increment(ref invocationCount);

            _Dispatcher.Register(Callback);
            _Dispatcher.Register(Callback);
            _Dispatcher.Dispatch(null);

            Assert.Equal(1, invocationCount);
        }

        [Fact]
        public void DispatchingNullPassesActionDataEmpty()
        {
            ActionData actualActionData = null;

            _Dispatcher.Register(actionData => Interlocked.Exchange(ref actualActionData, actionData));
            _Dispatcher.Dispatch(null);

            Assert.Same(ActionData.Empty, actualActionData);
        }

        [Fact]
        public void DispatchPassesSameActionData()
        {
            var expectedActionData = new MockActionData();
            ActionData actualActionData = null;

            _Dispatcher.Register(actionData => Interlocked.Exchange(ref actualActionData, actionData));
            _Dispatcher.Dispatch(expectedActionData);

            Assert.Same(expectedActionData, actualActionData);
        }

        private sealed class MockDispatcher : Dispatcher
        {
        }

        private sealed class MockActionData : ActionData
        {
        }
    }
}