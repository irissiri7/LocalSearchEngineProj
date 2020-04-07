using System;
using System.IO;

namespace ClassLibrary
{
    public static class FilePathVerifier
    {

        public static string Format { get; } = ".txt";
        // Should verify that files exist and is of the right format. It should also give a 'message'
        // depending on the result of the method, for example 'File added', 'File does not exist' or 'File not in right format...'
        public static bool CheckIfValidFilepath(string filePath, out string message)
        {
            message = null;
            if (!File.Exists(filePath))
            {
                message = $"{Path.GetFileName(filePath)} doesn't exist!";
                return false;
            }
            else if (Path.GetExtension(filePath) != Format)
            {
                message = $"Invalid format. Can only process {Format} files";
                return false;
            }

            return true;
        }
    }
}