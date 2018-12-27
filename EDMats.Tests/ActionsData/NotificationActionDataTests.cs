using System;
using EDMats.ActionsData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.ActionsData
{
    [TestClass]
    public class NotificationActionDataTests
    {
        [TestMethod]
        public void CreatingANotificationActionDataSetsTheIdAutomatically()
        {
            var notificationText = "test";
            var notificationActionData = new NotificationActionData(notificationText);

            Assert.AreNotEqual(default(Guid), notificationActionData.NotificationId);
            Assert.AreSame(notificationText, notificationActionData.NotificationText);
        }

        [TestMethod]
        public void CreatingANotificationWithNullTextDoesNotInitializeRelatedProperties()
        {
            var notificationActionData = new NotificationActionData(null);

            Assert.AreEqual(default(Guid), notificationActionData.NotificationId);
            Assert.IsNull(notificationActionData.NotificationText);
        }

        [TestMethod]
        public void CreatingANotificationWithEmptyTextDoesNotInitializeRelatedProperties()
        {
            var notificationActionData = new NotificationActionData(string.Empty);

            Assert.AreEqual(default(Guid), notificationActionData.NotificationId);
            Assert.IsNull(notificationActionData.NotificationText);
        }

        [TestMethod]
        public void CreatingANotificationWithWhiteSpaceTextDoesNotInitializeRelatedProperties()
        {
            var notificationActionData = new NotificationActionData(" \t\r\n");

            Assert.AreEqual(default(Guid), notificationActionData.NotificationId);
            Assert.IsNull(notificationActionData.NotificationText);
        }

        [TestMethod]
        public void CreatingANotificationWithoutMessageDoesNotInitializeRelatedProperties()
        {
            var notificationActionData = new TestNotificationActionData();

            Assert.AreEqual(default(Guid), notificationActionData.NotificationId);
            Assert.IsNull(notificationActionData.NotificationText);
        }

        [TestMethod]
        public void SettingTheNotificationTextSetsTheIdAutomatically()
        {
            var notificationText = "test";
            var notificationActionData = new TestNotificationActionData
            {
                NotificationText = notificationText
            };

            Assert.AreNotEqual(default(Guid), notificationActionData.NotificationId);
            Assert.AreSame(notificationText, notificationActionData.NotificationText);
        }

        [TestMethod]
        public void ResettingTheNotificationTextDoesNotChangeTheId()
        {
            var notificationText = "test";
            var updatedNotificationText = "updated " + notificationText;
            var notificationActionData = new TestNotificationActionData
            {
                NotificationText = notificationText
            };

            var notificationId = notificationActionData.NotificationId;
            notificationActionData.NotificationText = updatedNotificationText;

            Assert.AreEqual(notificationId, notificationActionData.NotificationId);
            Assert.AreSame(updatedNotificationText, notificationActionData.NotificationText);
        }

        [TestMethod]
        public void ResettingTheNotificationTextToNullSetsTheIdToDefault()
        {
            var notificationText = "test";
            var notificationActionData = new TestNotificationActionData
            {
                NotificationText = notificationText
            };

            notificationActionData.NotificationText = null;

            Assert.AreEqual(default(Guid), notificationActionData.NotificationId);
            Assert.IsNull(notificationActionData.NotificationText);
        }

        [TestMethod]
        public void ResettingTheNotificationTextToEmptySetsTheIdToDefault()
        {
            var notificationText = "test";
            var notificationActionData = new TestNotificationActionData
            {
                NotificationText = notificationText
            };

            notificationActionData.NotificationText = string.Empty;

            Assert.AreEqual(default(Guid), notificationActionData.NotificationId);
            Assert.IsNull(notificationActionData.NotificationText);
        }

        [TestMethod]
        public void ResettingTheNotificationTextToWhiteSpaceSetsTheIdToDefault()
        {
            var notificationText = "test";
            var notificationActionData = new TestNotificationActionData
            {
                NotificationText = notificationText
            };

            notificationActionData.NotificationText = " \t\r\n";

            Assert.AreEqual(default(Guid), notificationActionData.NotificationId);
            Assert.IsNull(notificationActionData.NotificationText);
        }

        [TestMethod]
        public void ResettingTheNotificationTextToNullThenUpdatingToAnActualStringDoesNotResetTheInitialId()
        {
            var notificationText = "test";
            var updatedNotificationText = "updated" + notificationText;
            var notificationActionData = new TestNotificationActionData
            {
                NotificationText = notificationText
            };
            var notificationId = notificationActionData.NotificationId;

            notificationActionData.NotificationText = null;
            Assert.AreEqual(default(Guid), notificationActionData.NotificationId);

            notificationActionData.NotificationText = updatedNotificationText;

            Assert.AreEqual(notificationId, notificationActionData.NotificationId);
            Assert.AreSame(updatedNotificationText, notificationActionData.NotificationText);
        }

        private sealed class TestNotificationActionData : NotificationActionData
        {
        }
    }
}