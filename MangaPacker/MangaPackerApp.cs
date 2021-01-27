using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using MangaPacker.Images;

namespace MangaPacker
{
    public class MangaPackerApp
    {
        private readonly string _path;
        private ICollection<DirectoryInfo> VolumeDirectories { get; set; }
        private AdvertDetector _advertDetector;
        
        
        public MangaPackerApp(string path)
        {
            _path = path;
            VolumeDirectories = new List<DirectoryInfo>();
            _advertDetector = new AdvertDetector(Path.Join(_path, "adverts"));
        }

        public void Scan()
        {
            var searchPattern = "*";
            DirectoryInfo di = new DirectoryInfo(_path);
            DirectoryInfo[] directories =
                di.GetDirectories(searchPattern, SearchOption.TopDirectoryOnly);

            foreach (var dir in directories)
            {
                if (dir.Name == "adverts") continue;
                Console.WriteLine($"Found directory {dir}");
                
                // Determine if this is a valid folder for packing
                var dirName = dir.Name;
                var volume = Parser.ParseVolume(dirName);
                var series = Parser.ParseSeries(dirName);
                var chapter = Parser.ParseChapter(dirName);
                
                // TODO: Assume all in the same series for now, eventually add a Map to manage instances
                if (series != string.Empty && (volume != "0" || chapter != string.Empty))
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
                }
                else
                {
                    Console.WriteLine($"Could not extract enough information to process from {dirName}");
                    Console.WriteLine($"Series: {series}, Volume: {volume}, Chapter: {chapter}");
                }
            }

            foreach (var directory in VolumeDirectories)
            {
                var dest = Path.Join(_path, directory.Name + ".cbz");
                if (!File.Exists(dest))
                {
                    ZipFile.CreateFromDirectory(directory.ToString(), dest);    
                }
                // Delete temp directory
                //Directory.Delete(directory.ToString());
                
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
                if (_advertDetector.IsAdvert(fileInfo.FullName))
                {
                    Console.WriteLine($"{fileInfo.FullName} is an advert. Skipping!");
                    continue;
                }
                var filename = Path.GetFileNameWithoutExtension(fileInfo.ToString());
                var chapterString = mangaVolume.Chapters != String.Empty ? "ch. " + Parser.PadZeros(mangaVolume.Chapters) : "";
                
                var newName =
                    $"{mangaVolume.Series} - v{mangaVolume.Volume} - {chapterString} - pg. {filename}";
                var extension = Path.GetExtension(fileInfo.ToString());

                var fullPath = Path.Join(newDirectory.ToString(), newName + extension);
                if (!File.Exists(fullPath))
                {
                    fileInfo.CopyTo(fullPath);    
                }
            }
        }

        /// <summary>
        /// Create a new folder called Series - Volume 001. This pads volume numbers.
        /// </summary>
        /// <param name="baseDir">Directory where to create new folder</param>
        /// <param name="mangaVolume">Information about the manga which we derive folder name from</param>
        /// <returns>Folder DirectoryInfo that (now) exists in baseDir. If already existing, will return that.</returns>
        private DirectoryInfo CreateTempVolumeFolder(DirectoryInfo baseDir, MangaVolume mangaVolume)
        {
            var folderName = $"{mangaVolume.Series} - Volume {Parser.PadZeros(mangaVolume.Volume)}";
            if (mangaVolume.Volume == "0")
            {
                folderName += $" - Chapter {Parser.PadZeros(mangaVolume.Chapters)}";
            }
            
            return Directory.CreateDirectory(Path.Combine(baseDir.ToString(), folderName));
        }

        
        private FileInfo[] GetFilesForDirectory(DirectoryInfo directory)
        {
            // TODO: We need to handle nested folders for packing
            return directory.GetFiles("*", SearchOption.TopDirectoryOnly);
        }
        
    }
}