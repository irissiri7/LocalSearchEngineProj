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
        public List<string> SortedTxtFile { get; private set; } = new List<string>();
        public List<String> Words { get; private set; } = new List<String>();

        //CONSTRUCTOR
        public TxtFile(string filePath)
        {
            FilePath = filePath;
            GetWords();
        }

        //METHODS
        public void SortWords()
        {
            foreach (string s in this.Words)
            {
                SortedTxtFile.Add(s);
            }
            SortingAlgoritm.HeapSort<string>(SortedTxtFile);         //Samma sak fast min metod

        }

        public int Search(string searchWord)
        {
            var validateSearch = new Regex(@"[a-zA-ZåäöÅÄÖ']+"); //@"^[a-zA-Z]+$"
            if (!validateSearch.IsMatch(searchWord))
            {
                throw new ArgumentException("Invalid Search");
            }
            return Words.Count(word => word == searchWord);
        }

        public void SaveSortedFile() //Saves the file as a {Filepath}_SortedWords.txt
        {
            string directory = Path.GetDirectoryName(FilePath);
            string fileName = Path.GetFileNameWithoutExtension(FilePath);
            string extension = Path.GetExtension(FilePath);

            string newPath = Path.Combine(directory, string.Concat(fileName, "_SortedWords", extension));

            File.WriteAllLines(newPath, SortedTxtFile);
        }

        private void GetWords()
        {
            char[] charsToAvoid = { '?', '!', ' ', ',', '.', ':', ';', '\t', '\r', '\n', '/' };
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string words = sr.ReadToEnd();
                words = words.Replace("\r\n", " ");
                var split = words.Split(" ");
                

                foreach (string s in split)
                {
                    Words.Add(s.Trim(charsToAvoid).ToLower());
                }
            }
        }

        //Not used anywhere, remove??
        public void WriteAllWords_ToConsole()
        {
            foreach (string s in Words)
            {
                Console.WriteLine(s);
            }
        }

    }
}
