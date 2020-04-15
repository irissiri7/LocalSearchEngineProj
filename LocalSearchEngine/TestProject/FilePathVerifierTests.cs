using NUnit.Framework;
using ClassLibrary;
using System.IO;

namespace TestProject
{
    [TestFixture]
    public class FilePathVerifierTests
    {
        [Test]
        public void CheckIfValidFilePath_HappyDays_ReturnsTrueAndCorrectMessage()
        {
            string dir = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(dir, @"ExampleFiles\ValidTxtFile.txt");
            bool result = FilePathVerifier.CheckIfValidFilepath(fullPath, ".txt", out string message);
            Assert.IsTrue(result);
            Assert.AreEqual(null, message);
        }

        [Test]
        public void CheckIfValidFilePath_FileDoesNotExist_ReturnsFalseAndCorrectMessage()
        {
            string dir = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(dir, @"ExampleFiles\IDoNotExistFile.txt");
            bool result = FilePathVerifier.CheckIfValidFilepath(fullPath, ".txt", out string message);
            Assert.IsFalse(result);
            Assert.AreEqual("IDoNotExistFile.txt doesn't exist!", message);
        }

        [Test]
        public void CheckIfValidFilePath_FileIsWrongFormat_ReturnsFalseAndCorrectMessage()
        {
            string dir = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(dir, @"ExampleFiles\NotValidTxtFile.xml");
            bool result = FilePathVerifier.CheckIfValidFilepath(fullPath, ".txt", out string message);
            Assert.False(result);
            Assert.AreEqual($"Invalid format. Can only process .txt files", message);
        }

    }
}
