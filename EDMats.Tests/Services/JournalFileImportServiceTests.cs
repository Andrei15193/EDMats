﻿using System;
using System.Collections.Generic;
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

        [TestMethod]
        public async Task ImportJournalLatestUpdatesFromFile()
        {
            using (var textReader = new StringReader(string.Empty))
            {
                var fileName = Guid.NewGuid().ToString();
                var latestEntry = DateTime.UtcNow;
                _FileSystemService
                    .Setup(fileSystemService => fileSystemService.OpenRead(fileName))
                    .Returns(textReader);

                var journalUpdates = new List<JournalUpdate>();
                _JournalImportService
                    .Setup(journalImportService => journalImportService.ImportLatestJournalUpdatesAsync(textReader, latestEntry, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(journalUpdates);

                var actualJournalUpdates = await _JournalFileImportService.ImportLatestJournalUpdatesAsync(fileName, latestEntry);
                Assert.AreSame(journalUpdates, actualJournalUpdates);
            }
        }
    }
}