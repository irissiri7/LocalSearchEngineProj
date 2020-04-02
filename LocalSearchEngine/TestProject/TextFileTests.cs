using NUnit.Framework;
using ClassLibrary;
using System.IO;
using System;

namespace TestProject
{
    public class TextFileTests
    {

        [Test]
        public void GetWords_HappyDays()
        {
            // Arrange
            var file = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt"));

            // Assert
            Assert.IsTrue(file.Words.Contains("hi"));
            Assert.IsTrue(file.Words.Contains("my"));
            Assert.IsTrue(file.Words.Contains("name"));
            Assert.IsTrue(file.Words.Contains("is"));
            Assert.IsTrue(file.Words.Contains("baloo"));
            Assert.IsTrue(file.Words.Contains("and"));
            Assert.IsTrue(file.Words.Contains("i"));
            Assert.IsTrue(file.Words.Contains("live"));
            Assert.IsTrue(file.Words.Contains("in"));
            Assert.IsTrue(file.Words.Contains("the"));
            Assert.IsTrue(file.Words.Contains("djungle"));
            Assert.IsTrue(file.Words.Contains("yesterday"));
            Assert.IsTrue(file.Words.Contains("met"));
            Assert.IsTrue(file.Words.Contains("a"));
            Assert.IsTrue(file.Words.Contains("new"));
            Assert.IsTrue(file.Words.Contains("friend"));
            Assert.IsTrue(file.Words.Contains("his"));
            Assert.IsTrue(file.Words.Contains("is"));
            Assert.IsTrue(file.Words.Contains("mowgli"));
            Assert.IsFalse(file.Words.Contains("."));
            Assert.IsFalse(file.Words.Contains(","));
        }

        [Test]
        public void Search_HappyDays()
        {
            // Arrange
            var file = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt"));

            // Assert
            Assert.AreEqual(2, file.Search("i"));
        }

        [Test]
        public void Search_BadInput()
        {
            // Arrange
            var file = new TxtFile(Path.Combine(Environment.CurrentDirectory, @"ExampleFiles\ValidTxtFile.txt"));

            // Assert
            Assert.Throws<ArgumentException>(() => file.Search("asdas123123%"));
        }
    }
}