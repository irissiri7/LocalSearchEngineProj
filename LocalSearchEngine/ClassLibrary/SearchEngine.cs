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
        public List<TxtFile> Files { get; private set; } = new List<TxtFile>();

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
            Console.WriteLine($"We can process files of the following formats: {FilePathVerifier.Format}");
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(">> ");
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

        // This method will ask user to submit files
        private void AskForFilePaths()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(
                "Submit one or more filepath(s). Submit one filepath at a time, submit by pressing 'Enter'");
            Console.WriteLine("When you are done, press 'p' and enter to proceed");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(">> ");
                Console.ForegroundColor = ConsoleColor.White;
                string input = Console.ReadLine()?.ToLower();
                
                switch (input)
                {
                    case "p":
                        UpdateFilesSubmitted();
                        GiveOptions();
                        return;

                    default:
                        TryAddFile(input, out string message);
                        if (message != null)
                        {
                            Console.WriteLine(message);
                        }

                        break;
                }
            }
        }

        // This method will try to add a new file to Files list, returning a 
        // bool for if the attempt was successful or not
        internal bool TryAddFile(string input, out string message)
        {
            bool result = false;
            if (FilePathVerifier.CheckIfValidFilepath(input, out message))
            {
                if (!CheckIfFileIsAlreadyInList(input))
                {
                    var txtFile = new TxtFile(input);
                    Files.Add(txtFile);
                    Console.WriteLine(
                        $"{Path.GetFileName(txtFile.FilePath)} with {txtFile.WordsUnsorted.Count} words was added");
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
        internal bool CheckIfFileIsAlreadyInList(string filePath)
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

        // This method makes sure files are submitted before proceeding
        internal bool CheckIfProceedIsPossible()
        {
            if (Files.Count > 0)
            {
                return true;
            }

            Console.WriteLine("You haven't submitted any valid files yet and can't proceed");
            return false;
        }

        // This method informs user how many valid files that are currently submitted to the SearchEngine
        private void UpdateFilesSubmitted()
        {
            var totalAmountOfWords = 0;
            foreach (var txtFile in Files)
            {
                totalAmountOfWords += txtFile.WordsUnsorted.Count;
            }

            Console.Clear();
            Console.WriteLine($"You have currently submitted {Files.Count} valid file(s)");
            Console.WriteLine($"You have submitted {totalAmountOfWords} words in total!");
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
            if (Files.Count == 0)
            {
                Console.WriteLine("Please add files before searching");
                return;
            }

            string input = "";
            bool firstInput = true;

            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please Enter a" + (firstInput ? "" : "Valid") + " Search Word");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(">> ");
                Console.ForegroundColor = ConsoleColor.White;
                input = Console.ReadLine()?.ToLower();
                firstInput = false;
            }

            int hits = Search(input, out string filePath);
            Console.Clear();
            Console.Write($"{hits} hit(s) for the word '{input}' in ");
            Console.Write("File: ");
            Console.WriteLine(filePath);
            GiveOptions();
        }

        // Testable Search Method
        public int Search(string search, out string filePath)
        {
            filePath = "";
            var maxHits = 0;
            if (Files.Count == 0) return -1;

            try
            {
                foreach (var file in Files)
                {
                    var hits = file.Search(search);
                    if (hits < maxHits) continue;
                    maxHits = hits;
                    filePath = file.FilePath;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            return maxHits;
        }

        // Processing Sort selection
        private void ProcessSortSelection() // Sorts all submitted files
        {
            if (Files.Count > 0)
            {
                foreach (var files in Files)
                {
                    files.SortWords();
                    Console.WriteLine($"{Path.GetFileName(files.FilePath)} sorted!");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Want to save files?");
                Console.WriteLine("[1] Yes");
                Console.WriteLine("Press any other key to return to main menu");
                Console.ForegroundColor = ConsoleColor.White;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        ProcessSaveAllFiles();
                        break;

                    default:
                        Console.Clear();
                        GiveOptions();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please add files before sorting");
            }
        }

        // Save all files
        private void ProcessSaveAllFiles()
        {
            Console.Clear();
            if (Files.Count > 0)
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
    }
}