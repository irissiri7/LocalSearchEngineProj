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
        private List<string> wordList = new List<string>();
        //PROPERTIES
        public string FilePath { get; private set; }
        public List<String> Words
        {
            get { return wordList; }
            set { wordList = value; }
        }

        //CONSTRUCTOR
        public TxtFile(string filePath)
        {
            FilePath = filePath;
            GetWords();
        }

        //METHODS
        private static List<string> SortWords(TxtFile txtFile) //returns a TxtFile sorted
        {
            List<string> sortedList = new List<string>();
            foreach (string s in txtFile.Words)
            {
                sortedList.Add(s);
            }
            sortedList.Sort();                                   //Implementera egen metod

            return sortedList;
        }
        private void GetWords()
        {

            char[] charsToAvoid = { '?', '!', ' ', ',', '.', ':', ';', '\t' };
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string words = sr.ReadLine();
                string[] split = words.Split(charsToAvoid);
                foreach (string s in split)
                {
                    wordList.Add(s);
                }
            }

        }
        public void WriteAllWords_ToConsole()
        {
            foreach (string s in this.wordList)
            {
                Console.WriteLine(s);
            }
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
