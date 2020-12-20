using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MangaPacker
{
    public class MangaVolume
    {
        public string Volume { get; init; }
        public string Series { get; init; }
        public string Chapters { get; init; }
        public IEnumerable<FileInfo> Files { get; init; }
    }
}