using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.IO;
using System.Linq;

[assembly: InternalsVisibleTo("TestProject")]

namespace ClassLibrary
{
    public class SearchEngine
    {
        public string Format { get; private set; }
        public List<TxtFile> Files { get; private set; } = new List<TxtFile>();
        public bool HasFiles { get => Files.Count > 0; }

        public SearchEngine(string desiredFormat)
        {
            Format = desiredFormat;
        }

        // This methods starts the program and keeps it in a loop until the user actively chooses to 'Exit'.
        public void Start()
        {
            GiveInstructions();
            // Outer loop runs until user choose 'Exit'
            while (true)
            {
                var processingFiles = true;
                GiveOptions();

                // Inner loop runs until user chooses to restart program
                while (processingFiles)
                {
                    ProcessSelection(ref processingFiles);
                }
            }
        }

        // Giving initial instructions
        private void GiveInstructions()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" __    __    ___  _         __   ___   ___ ___    ___ ");
            Console.WriteLine(@"|  T__T  T  /  _]| T       /  ] /   \ |   T   T  /  _]");
            Console.WriteLine(@"|  |  |  | /  [_ | |      /  / Y     Y| _   _ | /  [_ ");
            Console.WriteLine(@"|  |  |  |Y    _]| l___  /  /  |  O  ||  \_/  |Y    _]");
            Console.WriteLine(@"l  `  '  !|   [_ |     T/   \_ |     ||   |   ||   [_ ");
            Console.WriteLine(@" \      / |     T|     |\     |l     !|   |   ||     T");
            Console.WriteLine(@" \_ /\_ / l_____jl_____j \____j \___ / l___j___jl_____j");
            Console.ResetColor();
            Console.WriteLine($"We can process files of the following formats: {Format}");
            Console.WriteLine("You can search for words in a document or sort the document alphabetically");
        }

        // Giving the user options. This will basically represent the 'main menu' of the program.
        private void GiveOptions()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Choose an option:");
            Console.WriteLine("[1] Submit file(s)");
            Console.WriteLine("[2] Search for word");
            Console.WriteLine("[3] Sort document(s)");
            Console.WriteLine("[4] Restart");
            Console.WriteLine("[5] Exit");
        }

        // This method is continuously called in the program loop (see Start() method) until the user chooses to 'Exit' the program.
        private void ProcessSelection(ref bool processingFiles)
        {
            DisplayPrompt();
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    AskForFilePaths();
                    SubmitFilePaths();
                    break;
                case "2":
                    ProcessSearchSelection();
                    break;
                case "3":
                    ProcessSortSelection();
                    break;
                case "4":
                    ProcessRestartSelection(ref processingFiles);
                    break;
                case "5":
                    ProcessExitSelection();
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }

        // This method will ask user to submit files
        private void AskForFilePaths()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Submit one or more filepath(s). Submit one filepath at a time, submit by pressing 'Enter'");
            Console.WriteLine("When you are done, press 'p' to proceed");
        }

        // This method is responsible for running a loop where user can submit one or several files until user chooses to proceed.
        private void SubmitFilePaths()
        {
            while (true)
            {
                DisplayPrompt();
                string input = Console.ReadLine()?.ToLower();
                
                switch (input)
                {
                    case "p":
                        UpdateFilesSubmitted();
                        GiveOptions();
                        return;

                    default:
                        if (TryAddFile(input, out string message, out TxtFile file))
                            GiveSubmissionInformation(file);
                        else
                            Console.WriteLine(message);
                        break;
                }
            }
        }

        private void GiveSubmissionInformation(TxtFile file)
        {
            Console.WriteLine($"{Path.GetFileName(file.FilePath)} with {file.WordsUnsorted.Count} words was added");
        }

        // This method will try to add a new file to Files list
        internal bool TryAddFile(string input, out string message, out TxtFile file)
        {
            bool result = false;
            file = null;
            if (FilePathVerifier.CheckIfValidFilepath(input, Format, out message))
            {
                if (!CheckIfDuplicate(input))
                {
                    file = new TxtFile(input);
                    Files.Add(file);
                    result = true;
                }
                else
                {
                    message = $"{Path.GetFileName(input)} is already added. Can not add duplicates.";
                }
            }

            return result;
        }

        // Checks weather a file is already added to the SearchEngine
        internal bool CheckIfDuplicate(string filePath)
        {
            bool fileAlreadyInList = false;
            foreach (var f in Files)
            {
                if (f.FilePath == filePath)
                {
                    fileAlreadyInList = true;
                }
            }

            return fileAlreadyInList;
        }

        // This method informs user how many valid files and words that are currently submitted to the SearchEngine
        private void UpdateFilesSubmitted()
        {
            Console.Clear();
            Console.WriteLine($"You have currently submitted {Files.Count} valid file(s)");
            Console.WriteLine($"You have submitted {CountTotalWordsSubmitted()} words in total!");
        }

        private int CountTotalWordsSubmitted()
        {
            var totalAmountOfWords = 0;
            foreach (var txtFile in Files)
            {
                totalAmountOfWords += txtFile.WordsUnsorted.Count;
            }
            return totalAmountOfWords;
        }

        // This method will restart the program, also removing previously submitted files and starting fresh
        private void ProcessRestartSelection(ref bool processingFiles)
        {
            processingFiles = false;

            // Wiping the Files list, starting fresh
            Files = new List<TxtFile>();
            Console.Clear();
            Console.WriteLine("Program restarted and all submitted documents were erased successfully!");
        }

        // Processing Search option
        private void ProcessSearchSelection()
        {
            // If files are submitted, we proceed
            if (HasFiles)
            {
                string input = "";
                bool firstInput = true;

                while (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please Enter a" + (firstInput ? "" : "Valid") + " Search Word");
                    DisplayPrompt();
                    input = Console.ReadLine()?.ToLower();
                    firstInput = false;
                }

                var result = Search(input);
                Console.Clear();
                DisplaySearchResult(result);
                GiveOptions();
            }
            else
            {
                Console.WriteLine("Please add files before searching");
            }
        }

        private void DisplaySearchResult(List<KeyValuePair<string, int>> list)
        {
            Console.WriteLine("Search Result:");
            foreach(var item in list)
            {
                Console.WriteLine($"File: {item.Key}");
                Console.WriteLine($"Occurance: {item.Value} times");
                Console.WriteLine();
            }
        }

        // Testable Search Method
        public List<KeyValuePair<string, int>> Search(string search)
        {
            var result = new List<KeyValuePair<string, int>>();
      
            try
            {
                foreach (var file in Files)
                {
                    string filename = Path.GetFileName(file.FilePath);
                    var hits = file.Search(search);
                    var pair = new KeyValuePair<string, int>(filename, hits);
                    result.Add(pair);
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            result.Sort((x, y) => (y.Value.CompareTo(x.Value)));

            return result;

        }

        // Processing Sort selection
        private void ProcessSortSelection() // Sorts all submitted files
        {
            if (HasFiles)
            {
                SortAllFiles();
                GiveOptionToSaveFiles();
            }
            else
            {
                Console.WriteLine("Please add files before sorting");
            }
        }

        private void SortAllFiles()
        {
            foreach (var files in Files)
            {
                files.SortWords();
                Console.WriteLine($"{Path.GetFileName(files.FilePath)} sorted!");
            }
        }

        private void GiveOptionToSaveFiles()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Want to save files? [1] Yes");
            Console.WriteLine("Press any other key to return to main menu");
            Console.ForegroundColor = ConsoleColor.White;
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SaveAllFiles();
                    break;

                default:
                    Console.Clear();
                    GiveOptions();
                    break;
            }
        }

        // Save all files
        private void SaveAllFiles()
        {
            Console.Clear();
            if (HasFiles)
            {
                foreach (var files in Files)
                {
                    string path = files.SaveSortedFile();
                    Console.WriteLine($"{Path.GetFileName(path)} saved!");
                }

                GiveOptions();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Can't save, no files submitted");
                GiveOptions();
            }
        }

        // Exit program
        private void ProcessExitSelection()
        {
            Console.WriteLine("Bye bye");
            Console.ForegroundColor = ConsoleColor.Red;
            Environment.Exit(0);
        }

        private void DisplayPrompt()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(">> ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}