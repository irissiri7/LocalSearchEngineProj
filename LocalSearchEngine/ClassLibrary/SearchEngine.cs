﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("TestProject")]

namespace ClassLibrary
{
    public class SearchEngine
    {
        //PROPERTIES
        public List<TxtFile> Files { get; private set; } = new List<TxtFile>();
        private string Format { get; set; } = ".txt";


        //METHODS
        //This methods starts the program and keeps it in a loop until the user activly chooses to 'Exit'.
        public void Start()
        {
            GiveInstructions();
            //Outer loop runs until user choose 'Exit'
            while (true)
            {
                bool processingFiles = true;
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
            Console.WriteLine("Welcome to this tiny little search engine");
            Console.WriteLine("We can process files of .txt format");
            Console.WriteLine("You can search for words in a document or sort the document alphabetically");
        }

        //Giving the user options. This will basically represent the 'main menu' of the program.
        private void GiveOptions()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("[1] Submitt file(s)");
            Console.WriteLine("[2] Search for word");
            Console.WriteLine("[3] Sort document");
            Console.WriteLine("[4] Restart");
            Console.WriteLine("[5] Exit");
        }

        //This method is continously called in the program loop (see Start() method) until the user chooses to 'Exit' the program.
        private void ProcessSelection(ref bool processingFiles)
        {
            Console.Write(">>");
            string input = Console.ReadLine();
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
            Console.WriteLine("Submit one or more filepath(s). Submitt one filepath at a time, submitt by pressing 'Enter'");
            Console.WriteLine("When you are done, press 'p' to proceed");

            while (true)
            {
                Console.Write(">>");
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
            if (FilePathVerifier.CheckIfValidFilepath(input, Format, out message))
            {
                if (!CheckIfFileIsAlreadyInList(input))
                {
                    Files.Add(new TxtFile(input));
                    result = true;
                }
                else
                {
                    message = "File is already added. Can not add duplicates.";
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
            Console.WriteLine($"You have currently submitted {Files.Count} valid file(s)");
        }

        //This method will restart the program, also removing previously submitted files and starting fresh
        private void ProcessRestartSelection(ref bool processingFiles)
        {
            processingFiles = false;

            //Wiping the Files list, starting fresh
            Files = new List<TxtFile>();
            Console.WriteLine("Restarting the program");
        }

        //Processing Search option
        private void ProcessSearchSelection()
        {
            Console.WriteLine("Search not implemented");
        }

        //Processing Sort selection
        private void ProcessSortSelection()
        {
            Console.WriteLine("Sort not implemented");
        }

        //'Exit' program
        private void ProcessExitSelection()
        {
            Console.WriteLine("Bye bye");
            System.Environment.Exit(0);
        }
    }
}