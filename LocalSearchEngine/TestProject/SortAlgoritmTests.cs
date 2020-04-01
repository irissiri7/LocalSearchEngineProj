using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ClassLibrary;
using System.IO;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestHeap()
        {
            var expected = new List<string>();
            expected.Add("a");
            expected.Add("b");
            expected.Add("c");
            expected.Add("hej");

            var actual = new List<string>();
            actual.Add("hej");
            actual.Add("b");
            actual.Add("c");
            actual.Add("a");
            SortingAlgoritm.HeapSort<string>(actual);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Test_ValidTxtCount()
        {
            int count = 21;
            string dir = Directory.GetCurrentDirectory();
            var fullpath = Path.Combine(dir, @"ExampleFiles\ValidTxtFile.txt"); //Hi my name is Baloo and i live in the djungle. Yesterday I met a new friend, his name is Mowgli.
            var actual = new TxtFile(fullpath);
            Assert.AreEqual(count, actual.Words.Count);
        }
        [Test]
        public void Test_SortedTextFile()
        {
            string dir = Directory.GetCurrentDirectory();
            var fullpath = Path.Combine(dir, @"ExampleFiles\ValidTxtFile.txt"); //Hi my name is Baloo and i live in the djungle. Yesterday I met a new friend, his name is Mowgli.
            var expected = new List<string>();
            expected.Add("a");
            expected.Add("and");
            expected.Add("baloo");
            expected.Add("djungle");
            expected.Add("friend");
            expected.Add("hi");
            expected.Add("his");
            expected.Add("i");
            expected.Add("i");
            expected.Add("in");
            expected.Add("is");
            expected.Add("is");
            expected.Add("live");
            expected.Add("met");
            expected.Add("mowgli");
            expected.Add("my");
            expected.Add("name");
            expected.Add("name");
            expected.Add("new");
            expected.Add("the");
            expected.Add("yesterday");

            var actual = new TxtFile(fullpath);
            actual.SortWords();
            Assert.AreEqual(expected, actual.SortedTxtFile);
        }
        [Test]
        public void TestSaveASSorted()
        {
            string dir = Directory.GetCurrentDirectory();
            var fullpath = Path.Combine(dir, @"ExampleFiles\ValidTxtFile.txt");
            var result = new TxtFile(fullpath);
            result.SortWords();
            result.SaveSortedFile();
        }
    }
}