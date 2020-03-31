using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ClassLibrary;


namespace TestProject
{
    public class FilePathVerifierTests
    {
        [Test]
        public void CheckIfValidFilePath_HappyDays_ReturnsTrueAndCorrectMessage()
        {
            bool result = FilePathVerifier.CheckIfValidFilepath(@"C:\Users\91lydnil\source\repos\LocalSearchEngineProj\LocalSearchEngine\TestProject\ExampleFiles\ValidTxtFile.txt", ".txt", out string message);
            Assert.IsTrue(result);
            Assert.AreEqual("File added", message);
        }

        [Test]
        public void CheckIfValidFilePath_FileDoesNotExist_ReturnsFalseAndCorrectMessage()
        {
            bool result = FilePathVerifier.CheckIfValidFilepath(@"C:\Users\91lydnil\source\repos\LocalSearchEngineProj\LocalSearchEngine\TestProject\ExampleFiles\IDoNotExistFile.txt", ".txt", out string message);
            Assert.IsFalse(result);
            Assert.AreEqual("File does not exist", message);
        }

        [Test]
        public void CheckIfValidFilePath_FileIsWrongFormat_ReturnsFalseAndCorrectMessage()
        {
            bool result = FilePathVerifier.CheckIfValidFilepath(@"C:\Users\91lydnil\source\repos\LocalSearchEngineProj\LocalSearchEngine\TestProject\ExampleFiles\NotValidTxtFile.xml", ".txt", out string message);
            Assert.False(result);
            Assert.AreEqual("File not in right format. Can only process .txt files", message);
        }
    }
}
