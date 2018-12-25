using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests
{
    [TestClass]
    public class DispatcherTests
    {
        private Dispatcher _Dispatcher { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            _Dispatcher = new TestDispatcher();
        }

        [TestMethod]
        public void RegisteringToDispatcherInvokesCallback()
        {
            var invocationCount = 0;

            _Dispatcher.Register(actionData => Interlocked.Increment(ref invocationCount));
            _Dispatcher.Dispatch(null);

            Assert.AreEqual(1, invocationCount);
        }

        [TestMethod]
        public void RegisteringToDispatcherTwiceInvokesCallbackOnce()
        {
            var invocationCount = 0;

            void Callback(ActionData actionData) => Interlocked.Increment(ref invocationCount);

            _Dispatcher.Register(Callback);
            _Dispatcher.Register(Callback);
            _Dispatcher.Dispatch(null);

            Assert.AreEqual(1, invocationCount);
        }

        [TestMethod]
        public void DispatchingNullPassesActionDataEmpty()
        {
            ActionData actualActionData = null;

            _Dispatcher.Register(actionData => Interlocked.Exchange(ref actualActionData, actionData));
            _Dispatcher.Dispatch(null);

            Assert.AreSame(ActionData.Empty, actualActionData);
        }

        [TestMethod]
        public void DispatchPassesSameActionData()
        {
            var expectedActionData = new TestActionData();
            ActionData actualActionData = null;

            _Dispatcher.Register(actionData => Interlocked.Exchange(ref actualActionData, actionData));
            _Dispatcher.Dispatch(expectedActionData);

            Assert.AreSame(expectedActionData, actualActionData);
        }

        private sealed class TestDispatcher : Dispatcher
        {
        }

        private sealed class TestActionData :ActionData
        {
        }
    }
}