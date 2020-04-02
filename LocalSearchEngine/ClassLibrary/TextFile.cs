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
        public List<string> Words { get; set; } = new List<string>();

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

        public int Search(string searchWord)
        {
            var validateSearch = new Regex(@"^[a-zA-Z]+$");
            if (!validateSearch.IsMatch(searchWord))
            {
                throw new ArgumentException("Invalid Search");
            }
            return Words.Count(word => word == searchWord);
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
                string line;
                while ((line = sr.ReadLine()) != null)
                {

                    var split = line.Split(" ");

                    foreach (string s in split)
                    {
                        Words.Add(s.Trim(charsToAvoid).ToLower());
                    }
                }
            }
        }
    }
}
