using System;
using System.IO;
using EDMats.Services;
using EDMats.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDMats.Tests.Services
{
    [TestClass]
    public class FileSystemServiceTests
    {
        public TestContext TestContext { get; set; }

        private IFileSystemService _FileSystemService { get; } = new FileSystemService();

        [TestMethod]
        public void OpenReadGetsATextReaderForTheProvidedFilePath()
        {
            var testFileContent = Guid.NewGuid().ToString();
            var testFileName = Guid.NewGuid().ToString();
            var testFilePath = Path.Combine(TestContext.TestDir, testFileName);
            using (var fileStream = new FileStream(testFilePath, FileMode.CreateNew))
            using (var textWriter = new StreamWriter(fileStream))
                textWriter.Write(testFileContent);

            string actualFileContent;
            using (var textReader = _FileSystemService.OpenRead(testFilePath))
                actualFileContent = textReader.ReadToEnd();

            Assert.AreEqual(testFileContent, actualFileContent);
        }

        [TestMethod]
        public void OpenWriteGetsATextWriterForTheProvidedFilePath()
        {
            var testFileContent = Guid.NewGuid().ToString();
            var testFileName = Guid.NewGuid().ToString();
            var testFilePath = Path.Combine(TestContext.TestDir, testFileName);

            using (var textWriter = _FileSystemService.OpenWrite(testFilePath))
                textWriter.Write(testFileContent);

            string actualFileContent;
            using (var fileStream = new FileStream(testFilePath, FileMode.Open))
            using (var textReader = new StreamReader(fileStream))
                actualFileContent = textReader.ReadToEnd();

            Assert.AreEqual(testFileContent, actualFileContent);
        }

        [TestMethod]
        public void OpenWriteOverwritersExistingFile()
        {
            var oldTestFileContent = Guid.NewGuid().ToString();
            var testFileName = Guid.NewGuid().ToString();
            var testFilePath = Path.Combine(TestContext.TestDir, testFileName);

            using (var textWriter = _FileSystemService.OpenWrite(testFilePath))
                textWriter.Write(oldTestFileContent);

            var testFileContent = Guid.NewGuid().ToString();
            using (var textWriter = _FileSystemService.OpenWrite(testFilePath))
                textWriter.Write(testFileContent);

            string actualFileContent;
            using (var fileStream = new FileStream(testFilePath, FileMode.Open))
            using (var textReader = new StreamReader(fileStream))
                actualFileContent = textReader.ReadToEnd();

            Assert.AreEqual(testFileContent, actualFileContent);
        }
    }
}