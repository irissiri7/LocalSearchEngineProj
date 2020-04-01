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

        public int Search(string searchWword)
        {

            // temporary load file until proper load function is in place
            var str = "";
            using (var file = new StreamReader(FilePath))
            {
                var line = "";
                while ((line = file.ReadLine()) != null)
                {
                    str += line;
                }
            }
            var count = 0;
            var arr = file.ToLower().Split(" ");
            foreach (string word in arr)
            {
                if (word == searchWword)
                {
                    count++;
                }
            }

            return count;
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
