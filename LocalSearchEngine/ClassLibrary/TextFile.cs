using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    //This class will represent the .txt files the user wants to work with
    public class TxtFile
    {
        //PROPERTIES
        public string FilePath { get; private set; }

        //CONSTRUCTOR
        public TxtFile(string filePath)
        {
            FilePath = filePath;
        }

        //METHODS
        public string SortWords()
        {
            Console.WriteLine("Hej");
            return "Not implemented ";
        }

        public string Search(string word)
        {
            return "Not implemented";
        }

        public void Save(string text)
        {
            Console.WriteLine("Not implemented");
        }
    }

}
