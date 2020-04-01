using System;
using System.IO;

namespace ClassLibrary
{
    public static class FilePathVerifier
    {
        //Should verify that files exist and is of the right format. It should also give a 'message'
        //depending on the result of the method, for example 'File added', 'File does not exist' or 'File not in right format...'
        public static bool CheckIfValidFilepath(string filePath, string desiredFormat, out string message)
        {
            bool isValid = true;
            message = "File added";
            if (!File.Exists(filePath))
            {
                isValid = false;
                message = "File does not exist";
            }
            else if (Path.GetExtension(filePath) != desiredFormat)
            {
                isValid = false;
                message = $"File not in right format. Can only process {desiredFormat} files";

            }

            return isValid;
        }

    }
}