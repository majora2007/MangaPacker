using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CoenM.ImageHash;
using CoenM.ImageHash.HashAlgorithms;

namespace MangaPacker.Images
{
    public class AdvertDetector
    {
        private IList<ulong> _advertHashes;
        private readonly double _percentSimiliar = 80.0;

        /// <summary>
        /// A utility to detect if a file is an advert.
        /// </summary>
        /// <param name="advertFolderPath">Full path folder of where advert samples are.</param>
        public AdvertDetector(string advertFolderPath)
        {
            Scan(advertFolderPath);
        }

        private void Scan(string folderPath)
        {
            _advertHashes = new List<ulong>();
            var files = !Directory.Exists(folderPath) ? Array.Empty<string>() : Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                _advertHashes.Add(CreateHash(file));
            }
        }

        private ulong CreateHash(string file)
        {
            var hashAlgorithm = new PerceptualHash();
            using var stream = File.OpenRead(file);
            return hashAlgorithm.Hash(stream);
        }
        

        /// <summary>
        /// Given a filepath to an image, will validate if it's a fan group advert or not. 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public bool IsAdvert(string filepath)
        {
            var fileHash = CreateHash(filepath);
            //return fileHash != string.Empty && _advertHashes.Contains(fileHash);
            return _advertHashes.Any(h => CompareHash.Similarity(fileHash, h) >= _percentSimiliar);
        }
    }
}