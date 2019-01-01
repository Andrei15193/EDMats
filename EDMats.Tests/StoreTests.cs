using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests
{
    [TestClass]
    public class StoreTests
    {
        private static MockActionSet ActionSet { get; } = new MockActionSet();

        [TestMethod]
        public void CreatingStoreRegistersToDispatcher()
        {
            var store = new MockStore();

            ActionSet.Dispatch(null);

            Assert.AreEqual(1, store.InvocationCount);
        }

        [TestMethod]
        public void StoreWithMoreSpecificActionDataGetsNotifiedWhenMatchesExactly()
        {
            var store = new MockStore<SpecificActionData>();

            ActionSet.Dispatch(new SpecificActionData());

            Assert.AreEqual(1, store.InvocationCount);
        }

        [TestMethod]
        public void StoreWithMoreSpecificActionDataDoesNotGetNotifiedWhenUsingBaseActionData()
        {
            var store = new MockStore<SpecificActionData>();

            ActionSet.Dispatch(ActionData.Empty);

            Assert.AreEqual(0, store.InvocationCount);
        }

        [TestMethod]
        public void StoreWithBaseActionDataGetsNotifiedWhenUsingMoreSpecificActionData()
        {
            var store = new MockStore<ActionData>();

            ActionSet.Dispatch(new SpecificActionData());

            Assert.AreEqual(1, store.InvocationCount);
        }

        [TestMethod]
        public void NonVoidReturningMethodsAreNotRegistered()
        {
            var store = new MockStore<ActionData, int>();

            ActionSet.Dispatch(new SpecificActionData());

            Assert.AreEqual(0, store.InvocationCount);
        }

        private sealed class MockActionSet : ActionSet
        {
            new public void Dispatch(ActionData actionData)
                => base.Dispatch(actionData);
        }

        private class MockStore<TActionData, TResult> : Store where TActionData : ActionData
        {
            private int _invocationCount = 0;

            public int InvocationCount
                => _invocationCount;

            public MockStore()
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
            private int _invocationCount = 0;

            public int InvocationCount
                => _invocationCount;

            public MockStore()
            {
            }

            private void _Handle(TActionData actionData)
                => Interlocked.Increment(ref _invocationCount);
        }

        private sealed class MockStore : MockStore<ActionData>
        {
        }

        private class SpecificActionData : ActionData
        {
        }
    }
}