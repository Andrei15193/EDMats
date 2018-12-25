using System;
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
            var invocationCount = 0;

            var store = new MockStore(actionData => Interlocked.Increment(ref invocationCount));
            ActionSet.Dispatch(null);

            Assert.AreEqual(1, invocationCount);
        }

        private sealed class MockActionSet : ActionSet
        {
            new public void Dispatch(ActionData actionData)
                => base.Dispatch(actionData);
        }

        private sealed class MockStore : Store
        {
            private readonly Action<ActionData> _handler;

            public MockStore(Action<ActionData> handler)
            {
                _handler = handler;
            }

            protected override void Handle(ActionData actionData)
                => _handler?.Invoke(actionData);
        }
    }
}