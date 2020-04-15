using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    // This class will represent the .txt files the user wants to work with
    public class TxtFile
    {
        
        public string FilePath { get; private set; }
        public List<string> WordsSorted { get; private set; } = new List<string>();
        public List<string> WordsUnsorted { get; private set; } = new List<string>();
        
        public TxtFile(string filePath)
        {
            FilePath = filePath;
            GetWords();
        }
        
        // Sort method
        public void SortWords()
        {
            foreach (var word in WordsUnsorted)
            {
                WordsSorted.Add(word);
            }
            SortingAlgorithm.HeapSort<string>(WordsSorted);
        }

        // Search Method
        public int Search(string searchWord)
        {
            return WordsUnsorted.Count(word => word == searchWord);
        }

        // Saves the file as a <Filepath>_SortedWords.txt
        public string SaveSortedFile() 
        {
            string directory = Path.GetDirectoryName(FilePath);
            string fileName = Path.GetFileNameWithoutExtension(FilePath);
            string extension = Path.GetExtension(FilePath);

            string newPath = Path.Combine(directory, string.Concat(fileName, "_SortedWords", extension));

            File.WriteAllLines(newPath, WordsSorted);
            return newPath;
        }

        private void GetWords()
        {
            char[] charsToAvoid = { '?', '!', ' ', ',', '.', ':', ';', '/', '-', '"', '(', ')', '{', '}', '[', ']', '#', '*', '%', '+', '`', '=', '&' };
            string[] text = File.ReadAllLines(FilePath);
            foreach (string line in text)
            {
                string tabsRemoved = line.Replace("\t", " ");
                var temp = tabsRemoved.Split(" ");
                foreach (string t in temp)
                {
                    string cleanedUp = t.Trim(charsToAvoid).ToLower();
                    if (cleanedUp != "")
                        WordsUnsorted.Add(cleanedUp);
                }
            }
        }

    }
}
