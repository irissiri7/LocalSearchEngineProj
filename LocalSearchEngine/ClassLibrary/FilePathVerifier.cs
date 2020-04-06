using System;
using System.IO;

namespace ClassLibrary
{
    public static class FilePathVerifier
    {

        private static string Format { get; set; } = ".txt";
        //Should verify that files exist and is of the right format. It should also give a 'message'
        //depending on the result of the method, for example 'File added', 'File does not exist' or 'File not in right format...'
        public static bool CheckIfValidFilepath(string filePath, out string message)
        {
            bool isValid = true;
            message = null;
            if (!File.Exists(filePath))
            {
                isValid = false;
                message = $"{Path.GetFileName(filePath)} doesn't exist!";
            }
            else if (Path.GetExtension(filePath) != Format)
            {
                message = $"Invalid format. {filePath} doesn't have a valid format. The only valid filepaths are: {Format}";
                isValid = false;
            }
            return isValid;
        }
    }
}