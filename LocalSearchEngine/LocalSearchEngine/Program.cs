using System;
using ClassLibrary;

namespace LocalSearchEngine
{
    class Program
    {
        //The SearchEngine object is the class responsible for running the actual program and keeping track of the files we are working with.
        static void Main(string[] args)
        {
            var engine = new SearchEngine();
            engine.Start();
        }
    }
}
