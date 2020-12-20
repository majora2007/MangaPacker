using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace MangaPacker
{
    public class MangaPackerApp
    {
        private ICollection<DirectoryInfo> VolumeDirectories { get; set; }
        
        
        public MangaPackerApp()
        {
            VolumeDirectories = new List<DirectoryInfo>();
        }

        public void Scan(string path)
        {
            var searchPattern = "*";
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] directories =
                di.GetDirectories(searchPattern, SearchOption.TopDirectoryOnly);

            foreach (var dir in directories)
            {
                Console.WriteLine($"Found directory {dir}");
                
                // Determine if this is a valid folder for packing
                var dirName = dir.Name;
                var volume = Parser.ParseVolume(dirName);
                var series = Parser.ParseSeries(dirName);
                var chapter = Parser.ParseChapter(dirName);
                
                // TODO: Assume all in the same series for now, eventually add a Map to manage instances
                if (volume.Length > 0 && series != String.Empty)
                {
                    MangaVolume mangaVolume = new MangaVolume
                    {
                        Volume = volume,
                        Series = series,
                        Chapters = chapter,
                        Files = GetFilesForDirectory(dir)
                    };

                    DirectoryInfo newDirectory = CreateTempVolumeFolder(di, mangaVolume);
                    if (!VolumeDirectories.Contains(newDirectory))
                    {
                        VolumeDirectories.Add(newDirectory);
                    }
                    Console.WriteLine($"New folder created: {newDirectory}");
                    CopyFilesOver(newDirectory, mangaVolume);
                    var dest = Path.Join(path, newDirectory.Name + ".cbz");
                    if (!File.Exists(dest))
                    {
                        ZipFile.CreateFromDirectory(newDirectory.ToString(), dest);    
                    }
                    // Delete temp directory
                    Directory.Delete(newDirectory.ToString());
                    

                }
                else
                {
                    Console.WriteLine("Could not extract volume");
                }
                
                
            }
        }

        /// <summary>
        /// Copies over files within the mangaVolume over and renames them as:
        /// Series - v{volumeNum} - ch. {chapterNum} - pg. {pageNumber} where pagenumber is the individual image file
        /// </summary>
        /// <param name="newDirectory"></param>
        /// <param name="mangaVolume"></param>
        private void CopyFilesOver(DirectoryInfo newDirectory, MangaVolume mangaVolume)
        {
            foreach (var fileInfo in mangaVolume.Files)
            {
                var filename = Path.GetFileNameWithoutExtension(fileInfo.ToString());
                var chapterString = mangaVolume.Chapters != String.Empty ? "ch. " + Parser.PadZeros(mangaVolume.Chapters) : "";
                
                var newName =
                    $"{mangaVolume.Series} - v{mangaVolume.Volume} - {chapterString} - pg. {filename}";
                var extension = Path.GetExtension(fileInfo.ToString());

                if (!File.Exists(newName + extension))
                {
                    fileInfo.CopyTo(Path.Join(newDirectory.ToString(), newName + extension));    
                }
            }
        }

        /// <summary>
        /// Create a new folder called Series - Volume #001. This pads volume numbers.
        /// </summary>
        /// <param name="baseDir">Directory where to create new folder</param>
        /// <param name="mangaVolume">Information about the manga which we derive folder name from</param>
        /// <returns>Folder DirectoryInfo that (now) exists in baseDir. If already existing, will return that.</returns>
        private DirectoryInfo CreateTempVolumeFolder(DirectoryInfo baseDir, MangaVolume mangaVolume)
        {
            var folderName = $"{mangaVolume.Series} - Volume #{Parser.PadZeros(mangaVolume.Volume)}";
            return Directory.CreateDirectory(Path.Combine(baseDir.ToString(), folderName));
        }

        
        private FileInfo[] GetFilesForDirectory(DirectoryInfo directory)
        {
            return directory.GetFiles("*", SearchOption.TopDirectoryOnly);
        }

        private bool HasImages(DirectoryInfo directory)
        {
            return false;
        }
    }
}