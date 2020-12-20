using System;
using System.Globalization;
using System.IO;

namespace MangaPacker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You must pass directory to scan");
            }
            else
            {
                MangaPackerApp app = new MangaPackerApp();
                app.Scan(args[0]);
            }
        }
    }
}