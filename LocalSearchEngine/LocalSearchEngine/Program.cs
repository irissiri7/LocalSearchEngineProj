using System;
using ClassLibrary;

namespace LocalSearchEngine
{
    class Program
    {
        //The SearchEngine objekt is the class responsible for running the actual program and keeping track of the files we are working with.
        static void Main(string[] args)
        {
            SearchEngine engine = new SearchEngine();
            engine.Start();
        }
    }
}
