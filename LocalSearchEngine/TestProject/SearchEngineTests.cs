using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ClassLibrary;
using System.IO;

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
            var file = Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt");
            string message;

            //Act
            var couldAddFile = sut.TryAddFile(file, out message);

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
            var file = Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt");
            string message;

            //Act
            sut.TryAddFile(file, out message);
            bool couldAddFile = sut.TryAddFile(file, out message);

            //Assert
            Assert.IsFalse(couldAddFile);
            Assert.AreEqual(1, sut.Files.Count);
            Assert.AreEqual("ValidTxtFile.txt is already added. Can not add duplicates.", message);
        }

        [Test]
        public void CheckIfFileIsAlreadyInList_FileIsNotInList_ReturnsFalse()
        {
            //Arrange
            var sut = new SearchEngine();
            
            //Act
            bool fileAlreadyInList = sut.CheckIfFileIsAlreadyInList(@"ExampleFiles\ValidTxtFile.txt");

            //Assert
            Assert.IsFalse(fileAlreadyInList);
        }

        [Test]
        public void CheckIfFileIsAlreadyInList_FileIsAlreadyInList_ReturnsTrue()
        {
            //Arrange
            var sut = new SearchEngine();

            //Act
            sut.TryAddFile(@"ExampleFiles\ValidTxtFile.txt", out string message);
            bool fileAlreadyInList = sut.CheckIfFileIsAlreadyInList(@"ExampleFiles\ValidTxtFile.txt");

            //Assert
            Assert.IsTrue(fileAlreadyInList);
        }

        [Test]
        public void CheckIfProceedIsPossible_FileAreAdded_ReturnsTrue()
        {
            //Arrange
            var sut = new SearchEngine();

            //Act
            sut.TryAddFile(@"ExampleFiles\ValidTxtFile.txt", out string message);
            bool proceedIsPossible = sut.CheckIfProceedIsPossible();

            //Assert
            Assert.IsTrue(proceedIsPossible);
        }

        [Test]
        public void CheckIfProceedIsPossible_FilesAreNotAdded_ReturnsFalse()
        {
            //Arrange
            var sut = new SearchEngine();

            //Act
            bool proceedIsPossible = sut.CheckIfProceedIsPossible();

            //Assert
            Assert.IsFalse(proceedIsPossible);
        }

        [Test]
        public void Search_OneFile()
        {
            // Arrange
            var sut = new SearchEngine();
            var file = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt"));

            // Act
            sut.Files.Add(file);
            var maxHits = sut.Search("his", out string filePath);

            // Assert
            Assert.AreEqual(file.FilePath, filePath);
            Assert.AreEqual(1, maxHits);
        }

        [Test]
        public void Search_MultipleFiles()
        {
            // Arrange
            var sut = new SearchEngine();
            var file1 = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt"));
            var file2 = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile2.txt"));
            var file3 = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile3.txt"));

            // Act
            sut.Files.Add(file1);
            sut.Files.Add(file2);
            sut.Files.Add(file3);
            var maxHits = sut.Search("his", out string filePath);

            // Assert
            Assert.AreEqual(file2.FilePath, filePath);
            Assert.AreEqual(6, maxHits);
        }

        [Test]
        public void Search_NoFiles()
        {
            // Arrange
            var sut = new SearchEngine();

            // Act
            var maxHits = sut.Search("his", out string filePath);

            // Assert
            Assert.AreEqual(-1, maxHits);
        }
    }
}
