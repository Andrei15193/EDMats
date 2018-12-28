using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EDMats.Services;
using EDMats.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class GoalsFileStorageServiceTests
    {
        private Mock<IGoalsStorageService> _GoalsStorageService { get; set; }

        private Mock<IFileSystemService> _FileSystemService { get; set; }

        private IGoalsFileStorageService _GoalsFileStorageService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            _GoalsStorageService = new Mock<IGoalsStorageService>();
            _FileSystemService = new Mock<IFileSystemService>();

            _GoalsFileStorageService = new GoalsFileStorageService(_GoalsStorageService.Object, _FileSystemService.Object);
        }

        [TestMethod]
        public async Task ReadCommanderGoals()
        {
            var commanderGoals = new CommanderGoalsData();
            CommanderGoalsData actualCommanderGoals;
            using (var stringReader = new StringReader(string.Empty))
            {
                var fileName = Guid.NewGuid().ToString();
                _FileSystemService
                    .Setup(fileSystemService => fileSystemService.OpenRead(fileName))
                    .Returns(stringReader);
                _GoalsStorageService
                    .Setup(goalsStorageService => goalsStorageService.ReadGoalsAsync(stringReader, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(commanderGoals);

                actualCommanderGoals = await _GoalsFileStorageService.ReadGoalsAsync(fileName);
            }

            Assert.AreSame(commanderGoals, actualCommanderGoals);
        }

        [TestMethod]
        public async Task WriteCommanderGoals()
        {
            using (var stringWriter = new StringWriter())
            {
                var fileName = Guid.NewGuid().ToString();
                var commanderGoals = new CommanderGoalsData();
                _FileSystemService
                    .Setup(fileSystemService => fileSystemService.OpenWrite(fileName))
                    .Returns(stringWriter);
                _GoalsStorageService
                    .Setup(goalsStorageService => goalsStorageService.WriteGoalsAsync(stringWriter, commanderGoals, It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

                await _GoalsFileStorageService.WriteGoalsAsync(fileName, commanderGoals);
                _GoalsStorageService
                    .Verify(
                        goalsStorageService => goalsStorageService.WriteGoalsAsync(stringWriter, commanderGoals, It.IsAny<CancellationToken>()),
                        Times.Once()
                    );
            }

        }
    }
}