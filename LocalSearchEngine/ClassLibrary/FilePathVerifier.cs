using System;
using System.IO;

namespace ClassLibrary
{
    public static class FilePathVerifier
    {

        public static string Format { get; private set; } = ".txt";
        //Should verify that files exist and is of the right format. It should also give a 'message'
        //depending on the result of the method, for example 'File added', 'File does not exist' or 'File not in right format...'
        public static bool CheckIfValidFilepath(string filePath, out string message)
        {
            bool isValid = true;
            message = "File added";
            if (!File.Exists(filePath))
            {
                isValid = false;
                message = $"{Path.GetFileName(filePath)} doesn't exist!";
            }
            else if (Path.GetExtension(filePath) != Format)
            {
                message = $"Invalid format. Can only process {Format} files";
                isValid = false;
            }
            return isValid;
        }
    }
}