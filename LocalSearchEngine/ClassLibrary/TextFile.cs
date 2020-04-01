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
        private List<string> Words { get; set; } = new List<string>();

        //CONSTRUCTOR
        public TxtFile(string filePath)
        {
            FilePath = filePath;
            GetWords();
        }

        //METHODS
        public string SortWords()
        {
            return "Not implemented";
        }

        public string Search(string word)
        {
            return "Not implemented";
        }

        public void Save(string text)
        {
            Console.WriteLine("Not implemented");
        }

        private void GetWords()
        {
            char[] charsToAvoid = { '?', '!', ' ', ',', '.', ':', ';', '\t' };
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string words = sr.ReadLine();
                string[] split = words.Split(" ");
                
                foreach (string s in split)
                {
                    Words.Add(s.Trim(charsToAvoid).ToLower());
                }
            }
        }
    }
}
