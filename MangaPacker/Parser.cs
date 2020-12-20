﻿using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MangaPacker
{
    public static class Parser
    {
        //?: is a non-capturing group in C#, else anything in () will be a group
        private static readonly Regex[] MangaVolumeRegex = new[]
        {
            // Historys Strongest Disciple Kenichi_v11_c90-98.zip
            new Regex(
                
                @"(?<Series>.*)(\b|_)v(?<Volume>\d+)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),
            // Killing Bites Vol. 0001 Ch. 0001 - Galactica Scanlations (gb)
            new Regex(
                @"(vol. ?)(?<Volume>0*[1-9]+)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),
            // Dance in the Vampire Bund v16-17
            new Regex(
                
                @"(?<Series>.*)(\b|_)v(?<Volume>\d+-?\d+)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),
            new Regex(
                
                @"(?:v)(?<Volume>0*[1-9]+)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),
            
        };

        private static readonly Regex[] MangaSeriesRegex = new[]
        {
            // Gokukoku no Brynhildr - c001-008 (v01) [TrinityBAKumA], Black Bullet - v4 c17 [batoto]
            new Regex(
                
                @"(?<Series>.*)( - )(?:v|vo|c)\d",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),
            // Historys Strongest Disciple Kenichi_v11_c90-98.zip, Killing Bites Vol. 0001 Ch. 0001 - Galactica Scanlations (gb)
            new Regex(
                
                @"(?<Series>.*)(\b|_)v",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),
            
            // Black Bullet
            new Regex(
                
                @"(?<Series>.*)(\b|_)(v|vo|c)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),
            
            // [BAA]_Darker_than_Black_c1 (This is very greedy, make sure it's always last)
            new Regex(
                @"(?<Series>.*)(\b|_)(c)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),
            
            
        };

        private static readonly Regex[] ReleaseGroupRegex = new[]
        {
            // [TrinityBAKumA Finella&anon], [BAA]_, [SlowManga&OverloadScans], [batoto]
            new Regex(@"(?:\[(?<subgroup>(?!\s).+?(?<!\s))\](?:_|-|\s|\.)?)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),
            // (Shadowcat-Empire), 
            // new Regex(@"(?:\[(?<subgroup>(?!\s).+?(?<!\s))\](?:_|-|\s|\.)?)",
            //     RegexOptions.IgnoreCase | RegexOptions.Compiled),
        };

        private static readonly Regex[] MangaChapterRegex = new[]
        {
            new Regex(

                @"(c|ch)(\.? ?)(?<Chapter>\d+-?\d*)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled),
        };
        
        public static string ParseSeries(string filename)
        {
            foreach (var regex in MangaSeriesRegex)
            {
                var matches = regex.Matches(filename);
                foreach (Match match in matches)
                {
                    if (match.Groups["Volume"] != Match.Empty)
                    {
                        return CleanTitle(match.Groups["Series"].Value);    
                    }
                    
                }
            }

            Console.WriteLine("Unable to parse {0}", filename);
            return "";
        }

        public static string ParseVolume(string filename)
        {
            foreach (var regex in MangaVolumeRegex)
            {
                var matches = regex.Matches(filename);
                foreach (Match match in matches)
                {
                    if (match.Groups["Volume"] != Match.Empty)
                    {
                        var value = match.Groups["Volume"].Value;
                        
                        // I think we should put this logic elsewhere
                        // if (value.Contains("-"))
                        // {
                        //     var tokens = value.Split("-");
                        //     var from = Int32.Parse(tokens[0]);
                        //     var to = Int32.Parse(tokens[1]);
                        //     var delta = to - from;
                        // }
                        
                        return RemoveLeadingZeroes(match.Groups["Volume"].Value);    
                    }
                    
                }
            }

            Console.WriteLine("Unable to parse {0}", filename);
            return "";
        }

        public static string ParseChapter(string filename)
        {
            foreach (var regex in MangaChapterRegex)
            {
                var matches = regex.Matches(filename);
                foreach (Match match in matches)
                {
                    if (match.Groups["Chapter"] != Match.Empty)
                    {
                        var value = match.Groups["Chapter"].Value;

                        
                        if (value.Contains("-"))
                        {
                            var tokens = value.Split("-");
                            var from = RemoveLeadingZeroes(tokens[0]);
                            var to = RemoveLeadingZeroes(tokens[1]);
                            return $"{from}-{to}";
                        }

                        return RemoveLeadingZeroes(match.Groups["Chapter"].Value);
                    }

                }
            }

            return "";
        }

        /// <summary>
        /// Translates _ -> spaces, trims front and back of string, removes release groups
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string CleanTitle(string title)
        {
            foreach (var regex in ReleaseGroupRegex)
            {
                var matches = regex.Matches(title);
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        title = title.Replace(match.Value, "");
                    }
                }
            }

            title = title.Replace("_", " ");
            return title.Trim();
        }


        /// <summary>
        /// Pads the start of a number string with 0's so ordering works fine if there are over 100 items.
        /// Handles ranges (ie 4-8) -> (004-008).
        /// </summary>
        /// <param name="number"></param>
        /// <returns>A zero padded number</returns>
        public static string PadZeros(string number)
        {
            if (number.Contains("-"))
            {
                var tokens = number.Split("-");
                return $"{PerformPadding(tokens[0])}-{PerformPadding(tokens[1])}";
            }

            return PerformPadding(number);
        }

        private static string PerformPadding(string number)
        {
            var num = Int32.Parse(number);
            return num switch
            {
                < 10 => "00" + num,
                < 100 => "0" + num,
                _ => number
            };
        }
        
        public static string RemoveLeadingZeroes(string title)
        {
            return title.TrimStart(new Char[] { '0' });
        }
    }
}