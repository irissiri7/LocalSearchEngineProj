using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ClassLibrary;

namespace TestProject
{
    [TestFixture]
    class SearchEngineTests
    {
        [Test]
        public void TryAddFile_HappyDays_AddsFile()
        {
            //Arrange
            var sut = new SearchEngine();
            string filePath = @"C:\Users\91lydnil\source\repos\LocalSearchEngineProj\LocalSearchEngine\TestProject\ExampleFiles\ValidTxtFile.txt";
            string message;

            //Act
            bool couldAddFile = sut.TryAddFile(filePath, out message);

            //Assert
            Assert.IsTrue(couldAddFile);
            Assert.AreEqual(1, sut.Files.Count);
            Assert.AreEqual("File added", message);

        }

        [Test]
        public void TryAddFile_GivenDuplicateFile_DoesNotAddFile()
        {
            //Arrange
            var sut = new SearchEngine();
            string filePath = @"C:\Users\91lydnil\source\repos\LocalSearchEngineProj\LocalSearchEngine\TestProject\ExampleFiles\ValidTxtFile.txt";
            string message;

            //Act
            sut.TryAddFile(filePath, out message);
            bool couldAddFile = sut.TryAddFile(filePath, out message);

            //Assert
            Assert.IsFalse(couldAddFile);
            Assert.AreEqual(1, sut.Files.Count);
            Assert.AreEqual("File is already added. Can not add duplicates.", message);
        }
    }
}
