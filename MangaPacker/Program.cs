using System;
using System.IO;

namespace MangaPacker
{
    class Program
    {
        static void Main(string[] args)
        {
            var scanPath = @"C:\Users\Joe\Desktop\mangadex-scraper";
            
            MangaPackerApp app = new MangaPackerApp();
            app.Scan(scanPath);
            

            //DirectoryInfo di = new DirectoryInfo(scanPath);
            
            //var docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // string[] myFiles = Directory.GetFiles(scanPath);
            // Console.WriteLine("Files:");
            //
            // foreach (var myFile in myFiles)
            // {
            //     Console.WriteLine(myFile);
            // }
            //
            // string[] entries = Directory.GetFileSystemEntries(scanPath, "w*");
            // Console.WriteLine("Entries:");
            //
            // foreach (var entry in entries)
            // {
            //     Console.WriteLine(entry);
            // }
            //
            // string[] myDirs = Directory.GetDirectories(scanPath);
            // Console.WriteLine("Directories:");
            //
            // foreach (var myDir in myDirs)
            // {
            //     Console.WriteLine(myDir);
            // }


        }
    }
}