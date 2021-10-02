using System;
using System.IO;
using EDMats.Data;
using EDMats.Storage.Implementations;
using Xunit;

namespace EDMats.Tests.Storage
{
    public class CommanderProfileStorageHandlerTests
    {
        [Fact]
        public void Load_WhenThereIsNoCommanderProfile_ReturnsDefaultInstance()
        {
            var commanderProfileStorageHandler = new CommanderProfileStorageHandler(new InMemoryStorageHandler());

            var commanderProfile = commanderProfileStorageHandler.Load();

            Assert.Equal("Anonymous", commanderProfile.CommanderName);
            Assert.Equal(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Saved Games", "Frontier Developments", "Elite Dangerous"), commanderProfile.JournalsDirectoryPath);
        }

        [Fact]
        public void Save_WhenCommanderProfileIsProvided_StoresIt()
        {
            var commanderProfileStorageHandler = new CommanderProfileStorageHandler(new InMemoryStorageHandler());

            commanderProfileStorageHandler.Save(new CommanderProfile
            {
                CommanderName = "commander name",
                JournalsDirectoryPath = "journals directory path"
            });

            var commanderProfile = commanderProfileStorageHandler.Load();
            Assert.Equal("commander name", commanderProfile.CommanderName);
            Assert.Equal("journals directory path", commanderProfile.JournalsDirectoryPath);
        }

        [Fact]
        public void Save_WhenCommanderProfileIsUpdated_StoresIt()
        {
            var commanderProfileStorageHandler = new CommanderProfileStorageHandler(new InMemoryStorageHandler());
            commanderProfileStorageHandler.Save(new CommanderProfile
            {
                CommanderName = "commander name",
                JournalsDirectoryPath = "journals directory path"
            });

            commanderProfileStorageHandler.Save(new CommanderProfile
            {
                CommanderName = "updated commander name",
                JournalsDirectoryPath = "updated journals directory path"
            });

            var commanderProfile = commanderProfileStorageHandler.Load();
            Assert.Equal("updated commander name", commanderProfile.CommanderName);
            Assert.Equal("updated journals directory path", commanderProfile.JournalsDirectoryPath);
        }
    }
}