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
    public class JournalFileImportServiceTests
    {
        private IJournalFileImportService _JournalFileImportService { get; set; }

        private Mock<IJournalImportService> _JournalImportService { get; set; }

        private Mock<IFileSystemService> _FileSystemService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            _JournalImportService = new Mock<IJournalImportService>();
            _FileSystemService = new Mock<IFileSystemService>();
            _JournalFileImportService = new JournalFileImportService(_JournalImportService.Object, _FileSystemService.Object);
        }

        [TestMethod]
        public async Task ImportJournalFromFile()
        {
            using (var textReader = new StringReader(string.Empty))
            {
                var fileName = Guid.NewGuid().ToString();
                _FileSystemService
                    .Setup(fileSystemService => fileSystemService.OpenRead(fileName))
                    .Returns(textReader);

                var journalCommanderInformation = new JournalCommanderInformation();
                _JournalImportService
                    .Setup(journalImportService => journalImportService.ImportJournalAsync(textReader, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(journalCommanderInformation);

                var actualJournalCommanderInformation = await _JournalFileImportService.ImportAsync(fileName);
                Assert.AreSame(journalCommanderInformation, actualJournalCommanderInformation);
            }
        }
    }
}