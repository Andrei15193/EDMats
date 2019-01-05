using System.Threading;
using Xunit;

namespace Flux.Tests
{
    public class StoreTests
    {
        private MockActions _Actions { get; } = new MockActions();

        [Fact]
        public void CreatingStoreRegistersToDispatcher()
        {
            var store = MockStore.Instance;
            var previousInvocationCount = store.InvocationCount;

            _Actions.Dispatch(null);

            Assert.Equal(1, store.InvocationCount - previousInvocationCount);
        }

        [Fact]
        public void StoreWithMoreSpecificActionDataGetsNotifiedWhenMatchesExactly()
        {
            var store = MockStore<SpecificActionData>.Instance;
            var previousInvocationCount = store.InvocationCount;

            _Actions.Dispatch(new SpecificActionData());

            Assert.Equal(1, store.InvocationCount - previousInvocationCount);
        }

        [Fact]
        public void StoreWithMoreSpecificActionDataDoesNotGetNotifiedWhenUsingBaseActionData()
        {
            var store = MockStore<SpecificActionData>.Instance;
            var previousInvocationCount = store.InvocationCount;

            _Actions.Dispatch(ActionData.Empty);

            Assert.Equal(0, store.InvocationCount - previousInvocationCount);
        }

        [Fact]
        public void StoreWithBaseActionDataGetsNotifiedWhenUsingMoreSpecificActionData()
        {
            var store = MockStore<ActionData>.Instance;
            var previousInvocationCount = store.InvocationCount;

            _Actions.Dispatch(new SpecificActionData());

            Assert.Equal(1, store.InvocationCount - previousInvocationCount);
        }

        [Fact]
        public void NonVoidReturningMethodsAreNotRegistered()
        {
            var store = MockStore<ActionData, int>.Instance;
            var previousInvocationCount = store.InvocationCount;

            _Actions.Dispatch(new SpecificActionData());

            Assert.Equal(0, store.InvocationCount - previousInvocationCount);
        }

        private sealed class MockActions : Actions
        {
            new public void Dispatch(ActionData actionData)
                => base.Dispatch(actionData);
        }

        private class MockStore<TActionData, TResult> : Store where TActionData : ActionData
        {
            public static MockStore<TActionData, TResult> Instance { get; } = new MockStore<TActionData, TResult>();

            private int _invocationCount = 0;

            public int InvocationCount
                => _invocationCount;

            protected MockStore()
            {
            }

            private TResult _Handle(ActionData actionData)
            {
                Interlocked.Increment(ref _invocationCount);
                return default(TResult);
            }
        }

        private class MockStore<TActionData> : Store where TActionData : ActionData
        {
            public static MockStore<TActionData> Instance { get; } = new MockStore<TActionData>();

            private int _invocationCount = 0;

            public int InvocationCount
                => _invocationCount;

            protected MockStore()
            {
            }

            private void _Handle(TActionData actionData)
                => Interlocked.Increment(ref _invocationCount);
        }

        private sealed class MockStore : MockStore<ActionData>
        {
            new public static MockStore Instance { get; } = new MockStore();

            private MockStore()
            {
            }
        }

        private class SpecificActionData : ActionData
        {
        }
    }
}