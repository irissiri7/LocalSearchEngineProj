using NUnit.Framework;
using ClassLibrary;
using System.IO;
using System;

namespace TestProject
{
    public class TextFileTests
    {
        //REGULAR
        [Test]
        public void CreatingNewTxtObj_GivenRegularTxtFile_ReadsInTheWordsCorrectly()
        {
            // Arrange/Act
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
        public void CreatingNewTxtObj_GivenMessyTxtFile_ReadsInTheWordsCorrectly()
        {
            // Arrange/Act
             var file = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\MessyTxtFile.txt"));

            // Assert
            Assert.IsTrue(file.WordsUnsorted.Contains("all"));
            Assert.IsTrue(file.WordsUnsorted.Contains("an"));
            Assert.IsTrue(file.WordsUnsorted.Contains("and"));
            Assert.IsTrue(file.WordsUnsorted.Contains("awefull"));
            Assert.IsTrue(file.WordsUnsorted.Contains("be"));
            Assert.IsTrue(file.WordsUnsorted.Contains("crap"));
            Assert.IsTrue(file.WordsUnsorted.Contains("crappy"));
            Assert.IsTrue(file.WordsUnsorted.Contains("gonna"));
            Assert.IsTrue(file.WordsUnsorted.Contains("is"));
            Assert.IsTrue(file.WordsUnsorted.Contains("lots"));
            Assert.IsTrue(file.WordsUnsorted.Contains("mohahahaha"));
            Assert.IsTrue(file.WordsUnsorted.Contains("of"));
            Assert.IsTrue(file.WordsUnsorted.Contains("over"));
            Assert.IsTrue(file.WordsUnsorted.Contains("place"));
            Assert.IsTrue(file.WordsUnsorted.Contains("spaces"));
            Assert.IsTrue(file.WordsUnsorted.Contains("tabs"));
            Assert.IsTrue(file.WordsUnsorted.Contains("text"));
            Assert.IsTrue(file.WordsUnsorted.Contains("the"));
            Assert.IsTrue(file.WordsUnsorted.Contains("this"));
            Assert.IsTrue(file.WordsUnsorted.Contains("with"));


        }

        [Test]
        public void CreatingNewTxtObj_GivenRegularTxtFile_ReadsInTheCorrectNumOfWords()
        {
            //Arrange
            var fullpath = Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt");
            int numWords = 21;
            
            //Act
            var sut = new TxtFile(fullpath);
            
            //Assert
            Assert.AreEqual(numWords, sut.WordsUnsorted.Count);
        }

        [Test]
        public void SortWords__SameNumberOfWordsInSortedWordsAndUnsortedWords()
        {
            //Arrange
            var fullpath = Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt");
            var sut = new TxtFile(fullpath);

            //Act
            sut.SortWords();

            //Assert
            Assert.AreEqual(sut.WordsSorted.Count, sut.WordsUnsorted.Count);
        }

        [Test]
        public void SortWords_SortedWordsAndUnsortedWordsContainTheSameWords()
        {
            //Arrange
            var fullpath = Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt");
            var sut = new TxtFile(fullpath);

            //Act
            sut.SortWords();

            //Assert
            foreach(var word in sut.WordsSorted)
            {
                Assert.IsTrue(sut.WordsUnsorted.Contains(word));
            }
        }

        [Test]
        public void Search_WordIsInFile_ReturnsRightOccurance()
        {
            // Arrange
            var file = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt"));

            // Assert
            Assert.AreEqual(2, file.Search("i"));
        }

        [Test]
        public void Search_WordIsNotInFile_ReturnsRightOccurance()
        {
            // Arrange
            var file = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt"));

            // Assert
            Assert.AreEqual(0, file.Search("kuckeliku"));
        }

        [Test]
        public void SaveSortedFile_DoesSaveFile()
        {
            var fullpath = Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt");
            var result = new TxtFile(fullpath);
            result.SortWords();
            result.SaveSortedFile();
            bool fileExist = File.Exists(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile_SortedWords.txt"));

            Assert.IsTrue(fileExist);
        }


        //INTEGRATION 
        //Making sure Search and SortWords do not impact eachother.
        [Test]
        public void SearchAndSort_SearchSortThenSearchAgain_SearchResultIsTheSame()
        {
            // Arrange
            var file = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt"));
            int searchOne = file.Search("baloo");
            file.SortWords();
            int searchTwo = file.Search("baloo");

            // Assert
            Assert.AreEqual(searchOne, searchTwo);
        }

    }
}