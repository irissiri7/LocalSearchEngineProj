using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    //This class will represent the .txt files the user wants to work with
    public class TxtFile //C:/Users/maxtauru/Desktop/Sample.txt
    {

        //PROPERTIES
        public string FilePath { get; private set; }
        public List<string> SortedTxtFile { get; private set; } = new List<string>();
        public List<String> Words
        {
            get { return wordList; }
            set { wordList = value; }
        }
        private List<string> wordList = new List<string>();
        //CONSTRUCTOR
        public TxtFile(string filePath)
        {
            FilePath = filePath;
            GetWords();
        }

        //METHODS
        public void SortWords() //returns a TxtFile sorted or returns all words as a list
        {
            foreach (string s in this.Words)
            {
                SortedTxtFile.Add(s);
            }
            SortingAlgoritm.HeapSort<string>(SortedTxtFile);         //Samma sak fast min metod
            /*sortedList.Sort();       */                         //Implementera egen metod

        }
        public void SaveSortedFile () //Saves the file as a {Filepath}_SortedWords.txt
        {
            string directory = Path.GetDirectoryName(FilePath);
            string fileName = Path.GetFileNameWithoutExtension(FilePath);
            string extension = Path.GetExtension(FilePath);

            string newPath = Path.Combine(directory, string.Concat(fileName, "_SortedWords", extension));

            File.WriteAllLines(newPath, SortedTxtFile);
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
