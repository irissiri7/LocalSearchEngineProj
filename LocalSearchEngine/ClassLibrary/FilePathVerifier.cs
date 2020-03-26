using System;
using System.IO;

namespace ClassLibrary
{
    public static class FilePathVerifier
    {
        //Should verify that files exist AND is of the .txt format. It should also give a 'message'
        //depending on the result of the method, for example 'File added', 'File doesn't exist' or 'File not in right format' 
        public static bool CheckIfValidFilepath(string filePath, out string message)
        {
            message = "";
            return false;
        }

    }
}
