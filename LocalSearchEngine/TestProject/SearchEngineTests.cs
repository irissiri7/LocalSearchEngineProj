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
            var sut = new SearchEngine(".txt");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt");

            //Act
            var couldAddFile = sut.TryAddFile(filePath, out string message, out TxtFile file);

            //Assert
            Assert.IsTrue(couldAddFile);
            Assert.AreEqual(1, sut.Files.Count);
            Assert.AreEqual(null, message);

        }

        [Test]
        public void TryAddFile_GivenDuplicateFile_DoesNotAddFile()
        {
            //Arrange
            var sut = new SearchEngine(".txt");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt");

            //Act
            sut.TryAddFile(filePath, out string message, out TxtFile file);
            bool couldAddFile = sut.TryAddFile(filePath, out  message, out file);

            //Assert
            Assert.IsFalse(couldAddFile);
            Assert.AreEqual(1, sut.Files.Count);
            Assert.AreEqual("ValidTxtFile.txt is already added. Can not add duplicates.", message);
        }

        [Test]
        public void CheckIfFileIsAlreadyInList_FileIsNotInList_ReturnsFalse()
        {
            //Arrange
            var sut = new SearchEngine(".txt");
            
            //Act
            bool fileAlreadyInList = sut.CheckIfDuplicate(@"ExampleFiles\ValidTxtFile.txt");

            //Assert
            Assert.IsFalse(fileAlreadyInList);
        }

        [Test]
        public void CheckIfFileIsAlreadyInList_FileIsAlreadyInList_ReturnsTrue()
        {
            //Arrange
            var sut = new SearchEngine(".txt");

            //Act
            sut.TryAddFile(@"ExampleFiles\ValidTxtFile.txt", out string message, out TxtFile file);
            bool fileAlreadyInList = sut.CheckIfDuplicate(@"ExampleFiles\ValidTxtFile.txt");

            //Assert
            Assert.IsTrue(fileAlreadyInList);
        }

        [Test]
        public void Search_OneFile()
        {
            // Arrange
            var sut = new SearchEngine(".txt");
            var file = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt"));

            // Act
            sut.Files.Add(file);
            var result = sut.Search("his");

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(Path.GetFileName(file.FilePath), result[0].Key);
            Assert.AreEqual(1, result[0].Value);
        }

        [Test]
        public void Search_MultipleFiles()
        {
            // Arrange
            var sut = new SearchEngine(".txt");
            var file1 = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt"));
            var file2 = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile2.txt"));
            var file3 = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile3.txt"));

            // Act
            sut.Files.Add(file1);
            sut.Files.Add(file2);
            sut.Files.Add(file3);
            var result = sut.Search("his");

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(Path.GetFileName(file2.FilePath), result[0].Key);
            Assert.AreEqual(6, result[0].Value);
        }

    }
}
