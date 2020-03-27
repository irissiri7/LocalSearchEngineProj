using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class SearchEngine
    {
        //PROPERTIES
        private List<TxtFile> Files { get; set; } = new List<TxtFile>();


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
        
        //This method will ask user to submit file path(s).
        private void AskForFilePaths()
        {
            Console.WriteLine("Not implemented");
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
            Environment.Exit(0);
        }
    }
}
