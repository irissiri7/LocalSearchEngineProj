using NUnit.Framework;
using ClassLibrary;
using System.IO;
using System;
using System.Reflection;

namespace TestProject
{
    public class TextFileTests
    {

        [Test]
        public void Search_HappyDays()
        {
            var file = new TxtFile(Path.Combine(Directory.GetCurrentDirectory(), @"ExampleFiles\ValidTxtFile.txt"));
            Assert.AreEqual(2, file.Search("hej"));
        }

        //[Test]
        //public void Search_BadInput()
        //{
        //    var file = new TxtFile(Path.Combine(Environment.CurrentDirectory, @"\ExampleFiles\ValidTxtFile"));
        //    Assert.AreEqual(2, file.Search)
        //}
    }
}