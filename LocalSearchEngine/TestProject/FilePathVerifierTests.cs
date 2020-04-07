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
            bool result = FilePathVerifier.CheckIfValidFilepath(fullPath, out string message);
            Assert.IsTrue(result);
            Assert.AreEqual(null, message);
        }

        [Test]
        public void CheckIfValidFilePath_FileDoesNotExist_ReturnsFalseAndCorrectMessage() //$"{Path.GetFileName(filePath)} doesn't exist!";
        {
            string dir = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(dir, @"ExampleFiles\IDoNotExistFile.txt");
            bool result = FilePathVerifier.CheckIfValidFilepath(fullPath, out string message);
            Assert.IsFalse(result);
            Assert.AreEqual("IDoNotExistFile.txt doesn't exist!", message);
        }

        [Test]
        public void CheckIfValidFilePath_FileIsWrongFormat_ReturnsFalseAndCorrectMessage() //"Invalid format. C:\\Users\\maxtauru\\Documents\\GitHub\\LocalSearchEngineProj\\LocalSearchEngine\\TestProject\\bin\\Debug\\netcoreapp3.1\\ExampleFiles\\NotValidTxtFile.xml doesn't have a valid format. The only valid filepaths are: .txt"
        {
            string dir = Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(dir, @"ExampleFiles\NotValidTxtFile.xml");
            bool result = FilePathVerifier.CheckIfValidFilepath(fullPath, out string message);
            Assert.False(result);
            Assert.AreEqual($"Invalid format. {fullPath} doesn't have a valid format. The only valid filepaths are: .txt", message);
        }

    }
}
