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
            Assert.IsTrue(file.WordsUnsorted.Contains("hi"));
            Assert.IsTrue(file.WordsUnsorted.Contains("my"));
            Assert.IsTrue(file.WordsUnsorted.Contains("name"));
            Assert.IsTrue(file.WordsUnsorted.Contains("is"));
            Assert.IsTrue(file.WordsUnsorted.Contains("baloo"));
            Assert.IsTrue(file.WordsUnsorted.Contains("and"));
            Assert.IsTrue(file.WordsUnsorted.Contains("i"));
            Assert.IsTrue(file.WordsUnsorted.Contains("live"));
            Assert.IsTrue(file.WordsUnsorted.Contains("in"));
            Assert.IsTrue(file.WordsUnsorted.Contains("the"));
            Assert.IsTrue(file.WordsUnsorted.Contains("djungle"));
            Assert.IsTrue(file.WordsUnsorted.Contains("yesterday"));
            Assert.IsTrue(file.WordsUnsorted.Contains("met"));
            Assert.IsTrue(file.WordsUnsorted.Contains("a"));
            Assert.IsTrue(file.WordsUnsorted.Contains("new"));
            Assert.IsTrue(file.WordsUnsorted.Contains("friend"));
            Assert.IsTrue(file.WordsUnsorted.Contains("his"));
            Assert.IsTrue(file.WordsUnsorted.Contains("is"));
            Assert.IsTrue(file.WordsUnsorted.Contains("mowgli"));
            Assert.IsFalse(file.WordsUnsorted.Contains("."));
            Assert.IsFalse(file.WordsUnsorted.Contains(","));
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