using System.Threading;
using Xunit;

namespace Flux.Tests
{
    public class ActionsTest
    {
        private MockActions _Actions { get; } = new MockActions();

        [Fact]
        public void ActionDispatchesData()
        {
            var store = MockStore.Instance;
            var previousInvocationCount = store.InvocationCount;

            _Actions.Dispatch(null);

            Assert.Equal(1, store.InvocationCount - previousInvocationCount);
        }

        private sealed class MockActions : Actions
        {
            new public void Dispatch(ActionData actionData)
                => base.Dispatch(actionData);
        }

        private sealed class MockStore : Store
        {
            public static MockStore Instance { get; } = new MockStore();

            private int _invocationCount = 0;

            public int InvocationCount
                => _invocationCount;

            private MockStore()
            {
            }

            private void _Handle(ActionData actionData)
                => Interlocked.Increment(ref _invocationCount);
        }
    }
}