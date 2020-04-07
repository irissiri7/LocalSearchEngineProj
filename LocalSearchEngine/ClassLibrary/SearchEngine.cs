using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.IO;
[assembly: InternalsVisibleTo("TestProject")]

namespace ClassLibrary
{
    public class SearchEngine
    {
        //PROPERTIES
        public List<TxtFile> Files { get; private set; } = new List<TxtFile>();


        //METHODS
        //This methods starts the program and keeps it in a loop until the user activly chooses to 'Exit'.
        public void Start()
        {
            GiveInstructions();
            //Outer loop runs until user choose 'Exit'
            while (true)
            {
                var processingFiles = true;
                GiveOptions();

                //Inner loop runs until user chooses to restart program
                while (processingFiles)
                {
                    ProcessSelection(ref processingFiles);
                }
            }
        }

        //Giving initial instructions
        private void GiveInstructions()
        {
            Console.ResetColor();
            Console.WriteLine("Welcome to this tiny little search engine");
            Console.WriteLine("We can process files of .txt format");
            Console.WriteLine("You can search for words in a document or sort the document alphabetically");
        }

        //Giving the user options. This will basically represent the 'main menu' of the program.
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

        //This method is continously called in the program loop (see Start() method) until the user chooses to 'Exit' the program.
        private void ProcessSelection(ref bool processingFiles)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(">>");
            Console.ForegroundColor = ConsoleColor.White;
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    AskForFilePaths();
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

        //This method will ask user to submitt files
        private void AskForFilePaths()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Submit one or more filepath(s). Submit one filepath at a time, submit by pressing 'Enter'");
            Console.WriteLine("When you are done, press 'p' and enter to proceed");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(">>");
                Console.ForegroundColor = ConsoleColor.White;
                string input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "p":
                        if (CheckIfProceedIsPossible())
                        {
                            UpdateFilesSubmitted();
                            GiveOptions();
                            return;
                        }
                        break;
                    default:
                        TryAddFile(input, out string message);
                        if(message != null)
                        Console.WriteLine(message);
                        break;
                }

            }
        }

        //This method will try to add a new file to Files list, returning a 
        //bool for if the attenpt was successfull or not
        internal bool TryAddFile(string input, out string message)
        {
            bool result = false;
            if (FilePathVerifier.CheckIfValidFilepath(input, out message))
            {
                if (!CheckIfFileIsAlreadyInList(input))
                {
                    TxtFile txtFile = new TxtFile(input);
                    Files.Add(txtFile);
                    Console.WriteLine($"{Path.GetFileName(txtFile.FilePath)} with {txtFile.Words.Count} words was added");
                    result = true;
                }
                else
                {
                    message = $"{Path.GetFileName(input)} is already added. Can not add duplicates.";
                }
            }
            return result;
        }

        //Checks wheather a file is already added to the SearchEngine
        private bool CheckIfFileIsAlreadyInList(string filePath)
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

        //This method makes sure files are submitted before proceeding
        private bool CheckIfProceedIsPossible()
        {
            bool result = false;

            if (Files.Count > 0)
            {
                result = true;
            }
            else
            {
                Console.WriteLine("You haven't submitted any valid files yet and can't proceed");
            }

            return result;
        }

        //This method informs user how many valid files that are currently submitted to the SearchEngine
        private void UpdateFilesSubmitted()
        {
            var totalAmountOfWords = 0;
            foreach (TxtFile txtFile in Files)
            {
                totalAmountOfWords += txtFile.Words.Count;
            }
            Console.Clear();
            Console.WriteLine($"You have currently submitted {Files.Count} valid file(s)");
            Console.WriteLine($"You have submitted {totalAmountOfWords} words in total!");
        }

        //This method will restart the program, also removing previously submitted files and starting fresh
        private void ProcessRestartSelection(ref bool processingFiles)
        {
            processingFiles = false;

            //Wiping the Files list, starting fresh
            Files = new List<TxtFile>();
            Console.Clear();
            Console.WriteLine("Program restarted and all submitted documents were erased succesfully!");
        }

        //Processing Search option
        private void ProcessSearchSelection()
        {
            if (Files.Count == 0)
            {
                Console.WriteLine("Please add files before searching");
                return;
            }
            Console.WriteLine("Please Enter a Search Word");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(">>");
            Console.ForegroundColor = ConsoleColor.White;
            var input = Console.ReadLine();
            var hits = Search(input, out string filePath);
            Console.Clear();
            Console.Write($"Max Hits:{hits} for the word '{input}' in ");
            Console.Write("File: ");
            Console.WriteLine(filePath);
            GiveOptions();
        }

        public int Search(string search, out string filePath)
        {
            filePath = "";
            var maxHits = 0;
            if (Files.Count == 0) return -1;

            foreach (var file in Files)
            {
                var hits = file.Search(search);
                if (hits >= maxHits)
                {
                    maxHits = hits;
                    filePath = file.FilePath;
                }
            }
            return maxHits;
        }

        //Processing Sort selection
        private void ProcessSortSelection() //Sorts all submitted files
        {
            if (Files.Count > 0)
            {
                foreach (TxtFile files in Files) //TAR ALLA SUBMITTADE FILER.
                {
                    files.SortWords();
                    Console.WriteLine($"{files.FilePath} and {files.Words.Count} sorted!");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Want to save files?");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No (return to main menu)");
                Console.ForegroundColor = ConsoleColor.White;
                switch (Console.ReadKey().Key)                    //Vill du spara?
                {
                    case ConsoleKey.D1:
                        {
                            ProcessSaveAllFiles();
                        }
                        break;

                    case ConsoleKey.D2:
                        {
                            Console.Clear();
                            GiveOptions();
                            break;
                        }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You didn't submit any files");
                GiveOptions();
            }
        }
        private void ProcessSaveAllFiles()
        {

            Console.Clear();
            if (Files.Count > 0)
            {
                foreach (var files in Files)
                {
                    files.SaveSortedFile();
                    Console.WriteLine($"{files.FilePath} saved!");
                }
                GiveOptions();                                       

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Can't save since you had no submitted files");
                GiveOptions();
            }

        }


        
        //'Exit' program
        private void ProcessExitSelection()
        {
            Console.WriteLine("Bye bye");
            Environment.Exit(0);
        }
    }
}